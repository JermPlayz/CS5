using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {SELECTOR, MOVEMENT, ACTION, COMBAT}

public class GameCtrl : MonoBehaviour
{
    public CharacterCtrl characterCtrl;
    public Camera worldCamera;
    public GameState gameState;
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(characterCtrl.character == 1)
        {
            
        }

        worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
