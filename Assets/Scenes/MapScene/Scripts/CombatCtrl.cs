using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CombatState {SELECTOR, MOVEMENT, ACTION, ATTACKS, ENEMYSELECTOR, COMBAT, ENEMYTURN}

public class CombatCtrl : MonoBehaviour
{
    public Character1Ctrl character1Ctrl;
    public CharacterUnit characterUnit;
    public Camera worldCamera;
    public CombatState combatState;
    [SerializeField] public Tilemap tileMap;
    public Vector3 ptpos; //player tile position
    public Vector3 etpos; //enemy
    public GameObject player1;
    Vector2 worldPoint;
    RaycastHit2D hit;
    RaycastHit2D chosenCharacter;
    public ActionUI actionUI;
    public int movementconstraint;
    public List<Enemy> Enemylist;
    private void Start()
    {
        actionUI.EnableActionSelector(false);
        actionUI.EnableAttackSelector(false);
        combatState = CombatState.SELECTOR;
        ptpos = new Vector3Int(-6, 0, 0);
        movementconstraint = 4;
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

        }

        if(combatState == CombatState.ENEMYTURN)
        {
            foreach(Enemy enemy in Enemylist)
            {
                int r = UnityEngine.Random.Range(0, enemy.Moves.Count);
                Vector3 arrpoint = enemy.closestplayer + (new Vector3(enemy.Moves[r].Range, 0, 0));
                //if(Math.Abs(arrpoint.x - enemy.Getpos().x) <= enemy.moveconstraint && Math.Abs(arrpoint.y - enemy.Getpos().y) <= enemy.moveconstraint)
                //{ uncomment after alpha
                    enemy.UpdatePos(arrpoint);
                //}
                
                
            }
            combatState = CombatState.SELECTOR;
        }
    }
    
    IEnumerator waitasecond()
    {
        yield return new WaitForSeconds(1);
    }
}
