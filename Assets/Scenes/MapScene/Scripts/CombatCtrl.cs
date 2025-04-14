using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CombatState {SELECTOR, MOVEMENT, ACTION, ATTACKS, COMBAT}

public class CombatCtrl : MonoBehaviour
{
    public Character1Ctrl character1Ctrl;
    public Camera worldCamera;
    public CombatState combatState;
    [SerializeField] public Tilemap tileMap;
    Vector2 worldPoint;
    RaycastHit2D hit;
    RaycastHit2D chosenCharacter;
    public ActionUI actionUI;
    private void Start()
    {
        actionUI.EnableActionSelector(false);
        combatState = CombatState.SELECTOR;
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
            StartCoroutine(waitasecond());
            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(worldPoint, Vector2.down);
                Debug.Log(hit);

                //if (hit.collider != null)
                //{
                    var tpos = tileMap.WorldToCell(worldPoint);
                    Debug.Log(tpos);
                    var tile = tileMap.GetTile(tpos);
                    Debug.Log(tile);
                    combatState = CombatState.ACTION;
                //}
                
            }
            
        }
        if(combatState == CombatState.ACTION)
        {
            actionUI.EnableActionSelector(true);
        }

        if(combatState == CombatState.ATTACKS)
        {
            Debug.Log(chosenCharacter.collider.name);
        }
    }
    
    IEnumerator waitasecond()
    {
        yield return new WaitForSeconds(1);
    }
}
