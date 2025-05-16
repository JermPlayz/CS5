using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MayneMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
       public void MoveScene(int sceneID)
    {
        gameObject.SetActive(false); // Hides the button entirely
        //SceneManager.LoadScene()
        SceneManager.LoadScene(sceneID);

    }
}
