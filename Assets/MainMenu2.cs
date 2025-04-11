using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
