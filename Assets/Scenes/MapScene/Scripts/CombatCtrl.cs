using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public enum CombatState {BUSY, SELECTOR, MOVEMENT, ACTION, ATTACKS, ENEMYSELECTOR, DIALOGUE, COMBAT, ENEMYTURN, WIN, LOST}

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
    public Vector3 prevSpot;
    public GameObject endBattleObject;
    public Text endBattleText;
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
                        prevSpot = player1.transform.position;
                        player1.transform.position = ptpos;
                        Debug.Log(ptpos);
                        ptpos = tpos;
                        //characterAnimator.Play("Walk");
                        combatState = CombatState.ACTION;
                    }
                }
                
            }
            if(Input.GetKeyDown("escape"))
            {
                combatState = CombatState.SELECTOR;
            }
        }
        if(combatState == CombatState.ACTION)
        {
            actionUI.EnableActionSelector(true);
            if(Input.GetKeyDown("escape"))
            {
                actionUI.EnableActionSelector(false);
                player1.transform.position = prevSpot;
                ptpos = prevSpot;
                combatState = CombatState.SELECTOR;
            }
        }

        if(combatState == CombatState.ATTACKS)
        {
            actionUI.NewButton(characterUnit.Character.Moves);
            if(Input.GetKeyDown("escape"))
            {
                combatState = CombatState.ACTION;
            }
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
                        if(!unit.isPlayerUnit)
                        {
                            enemyUnit = unit;
                            enemyHud.SetHUD(enemyUnit);
                            viewEnemyHud.SetActive(true);
                            actionUI.EnableInitiateAttackSelector(true);
                        }
                    }
                    else
                    {
                        Debug.Log("This doesnt work");
                    }
                }
            }
            if(Input.GetKeyDown("escape"))
            {
                viewPlayerHud.SetActive(false);
                viewEnemyHud.SetActive(false);
                combatState = CombatState.ACTION;
            }
        }

        // if(combatState == CombatState.DIALOGUE)
        // {
        //     if(enemyUnit.hasDialogue == true)
        //     {

        //     }
        //     combatState = CombatState.BUSY;
        // }
        
        if(combatState == CombatState.COMBAT)
        {
            StartCoroutine(PlayerAttack());
            combatState = CombatState.BUSY;
        }

        if(combatState == CombatState.ENEMYTURN)
        {
            StartCoroutine(Enemyattack());
            combatState = CombatState.BUSY;
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

        if(CheckIfMoveHits(move, characterUnit.Character, enemyUnit.Character))
        {
            Debug.Log(enemyUnit.Character.HP);
            bool isDead = enemyUnit.Character.TakeDamage(move, characterUnit.Character);
            Debug.Log(enemyUnit.Character.HP);
            enemyHud.UpdateHP();
            if(isDead)
            {
                Enemylist.Remove(enemyUnit);
                Destroy(enemyUnit);
                enemyUnit.GetComponent<SpriteRenderer>().enabled = false;
                enemyUnit.GetComponent<CircleCollider2D>().enabled = false;
                int expYield = enemyUnit.Character.Base.ExpYield;
                int enemyLevel = enemyUnit.Character.Level;

                int expGain = Mathf.FloorToInt((expYield * enemyLevel) / 7);
                characterUnit.Character.Exp += expGain;
                bool levelUp = characterUnit.Character.CheckForLevelUp();
                if(levelUp)
                {
                yield return new WaitForSeconds(1f);
                characterUnit.UpdateLevel();
                playerHud.SetLevel();
                endBattleObject.SetActive(true);
                endBattleText.text = "Level Up";
                yield return new WaitForSeconds(1f);
                endBattleObject.SetActive(false);
                var newMove = characterUnit.Character.GetLearnableMoveAtCurrentLevel();

                if(newMove != null)
                {
                    if(characterUnit.Character.Moves.Count < 4)
                    {
                        characterUnit.Character.LearnMove(newMove);
                        actionUI.NewButton(characterUnit.Character.Moves);
                    }
                    else
                    {
                        //Forget a move
                    }
                }
            }
                if(Enemylist.Count == 0)
                {
                    viewPlayerHud.SetActive(false);
                    viewEnemyHud.SetActive(false);
                    StopAllCoroutines();
                    combatState = CombatState.WIN;
                    StartCoroutine(EndBattle());
                }
                //enemyAnimator.Play("Dead");
            }
            // else
            // {
            //     //enemyAnimator.Play("Hurt");
            // }
        }
        else
        {
            endBattleObject.SetActive(true);
            endBattleText.text = "You Missed";
            //enemyAnimator.Play("Shield");
        }

        yield return new WaitForSeconds(1f);
        //enemyAnimator.Play("Stay Dead");

        StartCoroutine(EnableCombatCutscene(false));
        //add loop through characters
        viewPlayerHud.SetActive(false);
        viewEnemyHud.SetActive(false);
        endBattleObject.SetActive(false);
        combatState = CombatState.ENEMYTURN;
    }
    IEnumerator waitasecond()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Enemyattacks(Move move, CharacterUnit player)
    {
        StartCoroutine(EnableCombatCutscene(true));
        viewPlayerHud.SetActive(true);
        enemyHud.SetHUD(enemyUnit);
        viewEnemyHud.SetActive(true);
        Debug.Log(player.Character.HP);
        yield return new WaitForSeconds(1f);
        if(CheckIfMoveHits(move, enemyUnit.Character, player.Character))
        {
            bool isDead = player.Character.TakeDamage(move, enemyUnit.Character);
            Debug.Log(player.Character.HP);
            playerHud.UpdateHP();
        if(isDead)
        {
            Destroy(player);
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<CircleCollider2D>().enabled = false;
            yield return new WaitForSeconds(1f);
            viewPlayerHud.SetActive(false);
            viewEnemyHud.SetActive(false);
            StopAllCoroutines();
            combatState = CombatState.LOST;
            StartCoroutine(EndBattle());
            //enemyAnimator.Play("Dead");
        }
        }
        else
        {
            endBattleObject.SetActive(true);
            endBattleText.text = "You Dodged";
            yield return new WaitForSeconds(1f);
            endBattleObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        viewPlayerHud.SetActive(false);
        viewEnemyHud.SetActive(false);
        StartCoroutine(EnableCombatCutscene(false));
    }

    IEnumerator Enemyattack()
    {
        foreach(CharacterUnit enemy in Enemylist)
        {
        int r = UnityEngine.Random.Range(0, enemy.Character.Moves.Count);
        var move = enemy.Character.Moves[r];
        enemyUnit = enemy;
        Vector3 arrpoint;
        if(enemy.Name == "Enemy1")
        {
            arrpoint = characterUnit.transform.position + (new Vector3(move.Base.Range, 0, 0));// change to find player
        }else if(enemy.Name == "Enemy2")
        {
            arrpoint = characterUnit.transform.position + (new Vector3(0, move.Base.Range, 0));
        }else{
            arrpoint = characterUnit.transform.position + (new Vector3(0, -(move.Base.Range), 0));
        }
        Debug.Log(enemy._base);
        yield return new WaitForSeconds(1f);
        //loop:
        Debug.Log("arrpoint assigned");
        if(Math.Abs(arrpoint.x - enemy.transform.position.x) <= enemy.moveconstraint && Math.Abs(arrpoint.y - enemy.transform.position.y) <= enemy.moveconstraint)
        {
            //hit = null;
            var checkenemy = Physics2D.Raycast(arrpoint, Vector2.down);
            //Debug.Log($"2:hit is equal to {checkenemy.collider.name}");
            //if(Math.Abs(arrpoint.x - hit.transform.position.x) <= 1f && Math.Abs(arrpoint.y - hit.transform.position.y) <= 1f)
            if(checkenemy.collider == null)
            {
                Debug.Log(checkenemy.collider);
                enemy.transform.position = arrpoint;
                Debug.Log("position updated to");
                Debug.Log(enemy.transform.position);
                yield return new WaitForSeconds(1f);
                StartCoroutine(Enemyattacks(move, characterUnit)); //change to find player
                yield return new WaitForSeconds(2f);
            }else{
                Debug.Log("in first else");
                Debug.Log(checkenemy.collider);
                arrpoint += new Vector3(1, 0, 0);
                enemy.transform.position = arrpoint;
                //goto loop;
                yield return new WaitForSeconds(1f);
                StartCoroutine(Enemyattacks(move, characterUnit));
                yield return new WaitForSeconds(2f);
            }
        }else{
            //move max of moveconstraint
            Debug.Log("in else");
            Debug.Log(Math.Abs(arrpoint.x - enemy.transform.position.x));
            Debug.Log(enemy.moveconstraint);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
    }
    hit = new RaycastHit2D();
    combatState = CombatState.SELECTOR;
    }

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(1f);
        if(combatState == CombatState.WIN)
        {
            endBattleObject.SetActive(true);
            endBattleText.text = "You Won";
            yield return new WaitForSeconds(1f);
        }
        else
        {
            endBattleObject.SetActive(true);
            endBattleText.text = "You Lost";
            yield return new WaitForSeconds(1f);
        }
    }
}
