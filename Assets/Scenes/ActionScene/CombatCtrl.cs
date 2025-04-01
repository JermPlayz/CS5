using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState {SELECTOR, MOVEMENT, ACTION, COMBAT}

public class CombatCtrl : MonoBehaviour
{
    public CharacterCtrl characterCtrl;
    public Camera worldCamera;
    public CombatState combatState;
    Vector2 worldPoint;
    RaycastHit2D hit;
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
                    Debug.Log("click on " + hit.collider.name);
                    Debug.Log(hit.point);
                } 
            }
        }        
    }
}
