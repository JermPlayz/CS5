using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonTut : MonoBehaviour
{
     public void MoveScene(int sceneID)
    {
        
        //SceneManager.LoadScene()
        SceneManager.LoadScene(sceneID);
        gameObject.SetActive(false); // Hides the button entirely
    }
}
