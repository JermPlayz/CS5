using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState {SELECTOR, MOVEMENT, ACTION, COMBAT}

public class CombatCtrl : MonoBehaviour
{
    public Character1Ctrl character1Ctrl;
    public Camera worldCamera;
    public CombatState combatState;
    Vector2 worldPoint;
    RaycastHit2D hit;
    RaycastHit2D chosenCharacter;
    [SerializeField] TileMap tileMap;
    private void Start()
    {
        combatState = CombatState.SELECTOR;
    }

    // Update is called once per frame
    private void Update()
    {
        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(combatState == CombatState.SELECTOR)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(worldPoint, Vector2.down);

                if (hit.collider != null)
                {
                    chosenCharacter = hit;
                    combatState = CombatState.MOVEMENT;
                }
                
            }
        }

        if(combatState == CombatState.MOVEMENT)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(worldPoint, Vector2.down);

                if (hit.collider != null)
                {
                    var tpos = tileMap.WorldToCell(hit.point);
                    var tile = tileMap.GetTile(tpos);
                    combatState = CombatState.ACTION;
                }
                
            }
        }

        if(combatState == CombatState.ACTION)
        {
            
        }
    }
}
