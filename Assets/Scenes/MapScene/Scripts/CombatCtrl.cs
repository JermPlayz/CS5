using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CombatState {SELECTOR, MOVEMENT, ACTION, ATTACKS, COMBAT}

public class CombatCtrl : MonoBehaviour
{
    public Character1Ctrl character1Ctrl;
    public CharacterUnit characterUnit;
    public Camera worldCamera;
    public CombatState combatState;
    [SerializeField] public Tilemap tileMap;
    public Vector3 ptpos;
    public GameObject player1;
    Vector2 worldPoint;
    RaycastHit2D hit;
    RaycastHit2D chosenCharacter;
    public ActionUI actionUI;
    private void Start()
    {
        actionUI.EnableActionSelector(false);
        actionUI.EnableAttackSelector(false);
        combatState = CombatState.SELECTOR;
        ptpos = new Vector3Int(-6, 0, 0);
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
                    ptpos = (Vector3)tpos * 1.25f;
                    ptpos = new Vector3(ptpos.x + .65f, ptpos.y + .62f, ptpos.z);
                    player1.transform.position = ptpos;
                    Debug.Log(ptpos);
                    combatState = CombatState.ACTION;
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
    }
    
    IEnumerator waitasecond()
    {
        yield return new WaitForSeconds(1);
    }
}
