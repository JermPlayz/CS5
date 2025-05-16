using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class button : MonoBehaviour
{
     public void MoveScene(int sceneID)
    {
        //SceneManager.LoadScene()
        SceneManager.LoadScene(sceneID);
        gameObject.SetActive(false); // Hides the button entirely
    }
}
