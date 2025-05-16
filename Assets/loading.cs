using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public Slider loadingSlider;    // Reference to the UI Slider
    public float loadDuration = 3f; // Simulated load time in seconds
    public int sceneToLoad;         // Scene index or name to load
    private float timer = 0f;
    void Update()
    {
        // Increase timer
        timer += Time.deltaTime;

        // Calculate progress (0 to 1)
        float progress = Mathf.Clamp01(timer / loadDuration);

        // Set slider value
        loadingSlider.value = progress;

        // Load the scene once progress is complete
        if (progress >= 1f)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}