using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public enum CombatState {BUSY, SELECTOR, MOVEMENT, ACTION, ATTACKS, ENEMYSELECTOR, DIALOGUE, COMBAT, ENEMYTURN}

public class CombatCtrl : MonoBehaviour
{
    public Character1Ctrl character1Ctrl;
    public CharacterUnit characterUnit;
    public CharacterUnit enemyUnit;
    public Camera worldCamera;
    public CombatState combatState;
    [SerializeField] public Tilemap tileMap;
    public Vector3 ptpos; //player tile position
    public Vector3 etpos; //enemy, delete after alpha
    public GameObject player1;
    Vector2 worldPoint;
    RaycastHit2D hit;
    RaycastHit2D chosenCharacter;
    public ActionUI actionUI;
    public int movementconstraint;
    public List<CharacterUnit> Enemylist;
    public GameObject combatCutscene;
    public GameObject viewPlayerHud;
    public GameObject viewEnemyHud;
    public BattleHud playerHud;
    public BattleHud enemyHud;
    public Animator characterAnimator;
    public Animator enemyAnimator;
    private void Start()
    {
        actionUI.EnableActionSelector(false);
        actionUI.EnableAttackSelector(false);
        actionUI.EnableUseSelector(false);
        actionUI.EnableInitiateAttackSelector(false);
        viewPlayerHud.SetActive(false);
        viewEnemyHud.SetActive(false);
        combatState = CombatState.SELECTOR;
        ptpos = new Vector3Int(-6, 0, 0);
        movementconstraint = 4;
        AudioManager.Instance.ChangeMusic1(AudioManager.SoundType.Music_Battle_Rain);
        AudioManager.Instance.ChangeMusic2(AudioManager.SoundType.Music_Battle_Thunder);
        AudioManager.Instance.MuteMusic2();
        characterUnit.Setup();
        foreach(CharacterUnit enemy in Enemylist)
        {
            enemy.Setup();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(combatState == CombatState.SELECTOR)
        {
            actionUI.EnableActionSelector(false);
            actionUI.EnableAttackSelector(false);
            actionUI.EnableUseSelector(false);
            actionUI.EnableInitiateAttackSelector(false);
            viewPlayerHud.SetActive(false);
            viewEnemyHud.SetActive(false);
            if(Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(worldPoint, Vector2.down);

                if (hit.collider != null)
                {
                    chosenCharacter = hit;
                    Debug.Log(chosenCharacter.collider.name);
                    combatState = CombatState.MOVEMENT;
                }
                
            }
        }

        if(combatState == CombatState.MOVEMENT)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(worldPoint, Vector2.down);
                Debug.Log(hit);

                Vector3Int tpos = tileMap.WorldToCell(worldPoint);
                Debug.Log(tpos);
                var tile = tileMap.GetTile(tpos);
                Debug.Log(tile);

                if(tpos != ptpos)
                {
                    if(Math.Abs(tpos.x - ptpos.x) <= movementconstraint && Math.Abs(tpos.y - ptpos.y) <= movementconstraint)
                    {
                        ptpos = (Vector3)tpos * 1.25f;
                        ptpos = new Vector3(ptpos.x + .65f, ptpos.y + .62f, ptpos.z);
                        player1.transform.position = ptpos;
                        Debug.Log(ptpos);
                        ptpos = tpos;
                        //characterAnimator.Play("Walk");
                        combatState = CombatState.ACTION;
                    }
                }
                
            }
            
        }
        if(combatState == CombatState.ACTION)
        {
            actionUI.EnableActionSelector(true);
        }

        if(combatState == CombatState.ATTACKS)
        {
            actionUI.NewButton(characterUnit.Character.Moves);
        }

        if(combatState == CombatState.ENEMYSELECTOR)
        {
            playerHud.SetHUD(characterUnit);
            viewPlayerHud.SetActive(true);
            if(Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(worldPoint, Vector2.down);
                Vector3Int epos = tileMap.WorldToCell(worldPoint);
                if(Math.Abs(epos.x - ptpos.x) <= actionUI.CurrentMoveRange() && Math.Abs(epos.y - ptpos.y) <= actionUI.CurrentMoveRange())
                {   
                    Debug.Log(hit.collider.name);
                    /*Vector3Int tpos = tileMap.WorldToCell(worldPoint);
                    Debug.Log(tpos);
                    var tile = tileMap.GetTile(tpos);
                    Debug.Log(tile);*/
                    //enemyUnit.Setup();
                    if(hit.collider.TryGetComponent<CharacterUnit>(out CharacterUnit unit))
                    {
                        enemyUnit = unit;
                        enemyHud.SetHUD(enemyUnit);
                        viewEnemyHud.SetActive(true);
                        actionUI.EnableInitiateAttackSelector(true);
                    }
                    else
                    {
                        Debug.Log("This doesnt work");
                    }
                }
            }
        }

        if(combatState == CombatState.DIALOGUE)
        {
            enemyUnit.DialogueTrigger.TriggerDialogue();
            combatState = CombatState.BUSY;
        }
        
        if(combatState == CombatState.COMBAT)
        {
            StartCoroutine(PlayerAttack());
            combatState = CombatState.BUSY;
        }

        if(combatState == CombatState.ENEMYTURN)
        {
            foreach(CharacterUnit enemy in Enemylist)
            {
                int r = UnityEngine.Random.Range(0, enemy.Character.Moves.Count);
                Vector3 arrpoint = characterUnit.transform.position + (new Vector3(enemy.Character.Moves[r].Base.Range, 0, 0));// change to find closest player
                Debug.Log("arrpoint assigned");
                Debug.Log(arrpoint);
                //if(Math.Abs(arrpoint.x - enemy.Getpos().x) <= enemy.moveconstraint && Math.Abs(arrpoint.y - enemy.Getpos().y) <= enemy.moveconstraint)
                //{ uncomment after alpha
                    enemy.transform.position = arrpoint;
                    Debug.Log("position updated");
                    Debug.Log(enemy.transform.position);
                    Enemyattacks(enemy.Character.Moves[r], characterUnit); //change to find closest player
                //}
                
            }
            combatState = CombatState.SELECTOR;
        }
    }
    IEnumerator EnableCombatCutscene(bool enabled)
    {
        //combatCutscene.SetActive(enabled);
        if(enabled == true)
        {
            AudioManager.Instance.MuteMusic1();
            AudioManager.Instance.UnmuteMusic2();
            yield return new WaitForSeconds(.25f);
            //characterAnimator.Play("Attack");
            yield return new WaitForSeconds(.575f);
            //characterAnimator.Play("Idle");
        }
        else
        {
            AudioManager.Instance.MuteMusic2();
            AudioManager.Instance.UnmuteMusic1();
        }
    }
    bool CheckIfMoveHits(Move move, Character source, Character target)
    {
        float moveAccuracy = move.Base.Accuracy;

        int accuracy = source.StatBoosts[Stat.Accuracy];
        int evasion = source.StatBoosts[Stat.Evasion];

        var boostValues = new float[] {1f, 4f / 3f, 5f / 3f, 2f, 7f / 3f, 8f / 3f, 3f};

        if(accuracy > 0)
            moveAccuracy *= boostValues[accuracy];
        else
            moveAccuracy /= boostValues[-accuracy];
        
        if(evasion > 0)
            moveAccuracy /= boostValues[evasion];
        else
            moveAccuracy *= boostValues[-evasion];

        return UnityEngine.Random.Range(1, 101) <= moveAccuracy;
    }

    IEnumerator PlayerAttack()
    {
        StartCoroutine(EnableCombatCutscene(true));
        var move = characterUnit.Character.Moves[actionUI.CurrentMove()];
        yield return new WaitForSeconds(.5f);

        // if(CheckIfMoveHits(move, characterUnit.Character, enemyUnit.Character))
        // {
            Debug.Log(enemyUnit.Character.HP);
            bool isDead = enemyUnit.Character.TakeDamage(move, characterUnit.Character);
            Debug.Log(enemyUnit.Character.HP);
            enemyHud.UpdateHP();
            // if(isDead)
            // {
            //     //enemyAnimator.Play("Dead");
            // }
            // else
            // {
            //     enemyAnimator.Play("Hurt");
            // }
        // }
        // else
        // {
        //     enemyAnimator.Play("Shield");
        // }

        yield return new WaitForSeconds(1f);
        //enemyAnimator.Play("Stay Dead");

        StartCoroutine(EnableCombatCutscene(false));
        //add loop through characters
        combatState = CombatState.ENEMYTURN;
    }
    IEnumerator waitasecond()
    {
        yield return new WaitForSeconds(1f);
    }

    public void Enemyattacks(Move move, CharacterUnit player)
    {

    }
}
