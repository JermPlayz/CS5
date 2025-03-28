using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {MOVEMENT, ACTION, COMBAT}

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
    }
}
