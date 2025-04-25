using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public enum CombatState {SELECTOR, MOVEMENT, ACTION, ATTACKS, ENEMYSELECTOR, COMBAT, ENEMYTURN}

public class CombatCtrl : MonoBehaviour
{
    public Character1Ctrl character1Ctrl;
    public CharacterUnit characterUnit;
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
    public List<Enemy> Enemylist;
    public GameObject combatCutscene;

    private void Start()
    {
        actionUI.EnableActionSelector(false);
        actionUI.EnableAttackSelector(false);
        combatState = CombatState.SELECTOR;
        ptpos = new Vector3Int(-6, 0, 0);
        movementconstraint = 4;
        AudioManager.Instance.ChangeMusic1(AudioManager.SoundType.Music_Battle_Rain);
        AudioManager.Instance.ChangeMusic2(AudioManager.SoundType.Music_Battle_Thunder);
        AudioManager.Instance.MuteMusic2();
    }

    // Update is called once per frame
    private void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(combatState == CombatState.SELECTOR)
        {
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
            characterUnit.Setup();
            actionUI.NewButton(characterUnit.Character.Moves);

        }

        if(combatState == CombatState.ENEMYSELECTOR)
        {
            if(Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(worldPoint, Vector2.down);
                Debug.Log(hit.collider.name);

                /*Vector3Int tpos = tileMap.WorldToCell(worldPoint);
                Debug.Log(tpos);
                var tile = tileMap.GetTile(tpos);
                Debug.Log(tile);*/
                combatState = CombatState.COMBAT;
            }
        }
        if(combatState == CombatState.COMBAT)
        {
            var move = characterUnit.Character.Moves[actionUI.CurrentMove()];
            EnableCombatCutscene(true);
            // StartCoroutine(waitasecond());
            // EnableCombatCutscene(false);

        }

        if(combatState == CombatState.ENEMYTURN)
        {
            foreach(Enemy enemy in Enemylist)
            {
                int r = UnityEngine.Random.Range(0, enemy.Moves.Count);
                Vector3 arrpoint = enemy.closestplayer.transform.position + (new Vector3(enemy.Moves[r].Base.Range, 0, 0));
                //if(Math.Abs(arrpoint.x - enemy.Getpos().x) <= enemy.moveconstraint && Math.Abs(arrpoint.y - enemy.Getpos().y) <= enemy.moveconstraint)
                //{ uncomment after alpha
                    enemy.UpdatePos(arrpoint);
                    Enemyattacks(enemy.Moves[r], enemy.closestplayer);
                //}
                
            }
            combatState = CombatState.SELECTOR;
        }
    }
    public void EnableCombatCutscene(bool enabled)
    {
        combatCutscene.SetActive(enabled);
        if(enabled == true)
        {
            AudioManager.Instance.MuteMusic1();
            AudioManager.Instance.UnmuteMusic2();
        }
        else
        {
            AudioManager.Instance.MuteMusic2();
            AudioManager.Instance.UnmuteMusic1();
        }
    }
    IEnumerator waitasecond()
    {
        yield return new WaitForSeconds(1);
    }

    public void Enemyattacks(Move move, GameObject player)
    {

    }
}
