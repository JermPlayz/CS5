using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Watson : MonoBehaviour
{
    [SerializeField] TMP_Text textMeshPro; // Reference to the TextMeshPro component
    public float typingSpeed = 0.01f; // Speed of typing in seconds
    private string[] dialogueTexts;
    private int currentStep = 0;

    private void Start()
    {
        dialogueTexts = new string[]
        {
            "Hello, rookie Coder! Welcome to CS5: Byte and Battle! I’m Watson, your computing companion, here to keep you from getting compiled into oblivion. Ready to debug this mainframe?",
            "Click a Coder. Got it? First, check your Player UI go ahead and click me. See the grid with your Coders’ stats? It shows HP, Attack, and Movement Range.",
            "The UI is like your IDE’s debug panel. Look at the top: green bars for HP, an icon for Attack, and a way of Movement. Try clicking a Coder now. Ready?",
            "Nice! Let’s get moving. Each Coder can move tiles on the grid per turn. Click your first Coder then click a place to move. Go for it!",
            "Smooth move! Now, let’s Hack that Glitch. When your Coder is next to an enemy, click Hack to attack. Hack deals damage, and Debug gives HP. Click the Hack Button to unleash your attack. Show that enemy who’s boss!",
            "If you want the player to go back you can press escape",
            "That’s the spirit! Take out the last Enemy, and Level 1 is yours. I’ll pop back if you need me. Compile those Glitches!",
            "Keep going, there are more enemies to defeat!",
            "Defeat the enemy and the next level will begin!"
        };

        textMeshPro.text = string.Empty;
        StartCoroutine(TypeDialogueSequence());
        Task.Delay(60000); // Wait for 2000 milliseconds (2 seconds)
        SceneManager.LoadScene(7);

    }

    private IEnumerator TypeDialogueSequence()
    {
        while (currentStep < dialogueTexts.Length)
        {
            yield return StartCoroutine(TypeText(dialogueTexts[currentStep]));

            // Optionally wait for player input before continuing
            yield return new WaitForSeconds(1.5f); // Or replace with button press or condition

            // Call highlighting here if needed
            HighlightUI(currentStep);

            currentStep++;
        }
    }

    // Coroutine to simulate typing effect
    private IEnumerator TypeText(string fullText)
    {
        textMeshPro.text = "";
        foreach (char letter in fullText)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Stub function for highlighting UI components
    private void HighlightUI(int step)
    {
        switch (step)
        {
            case 1:
                // Highlight stats panel (e.g., HP, Attack, Movement)
                Debug.Log("Highlighting Stats Panel...");
                break;
            case 2:
                // Highlight individual stat icons (green bar, sword, boot)
                Debug.Log("Highlighting Stat Icons...");
                break;
            case 3:
                // Highlight Coder unit and movement tiles
                Debug.Log("Highlighting Movement Tiles...");
                break;
            case 4:
                // Highlight enemy and attack action
                Debug.Log("Highlighting Attack Function...");
                break;
                // Add more cases if needed
        }
    }

}
