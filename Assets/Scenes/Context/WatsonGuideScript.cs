/*using UnityEngine;
using TMPro;
using System.Collections;

public class WatsonGuideScript : MonoBehaviour
{
    public TMP_Text textMeshPro; // Reference to the TextMeshPro component
    public float typingSpeed = 0.01f; // Speed of typing in seconds

    private string[] dialogueTexts;
    private int currentStep = 0;

    private void Start()
    {
        dialogueTexts = new string[]
        {
            "Hello, rookie Coder! Welcome to CS5: Byte and Battle! I’m Watson, your computing companion, here to keep you from getting compiled into oblivion. Ready to debug this mainframe?",
            "First, check your Player UI at the bottom of the screen. See the grid with your Coders’ stats? That’s your lifeline! It shows HP, Attack, and Movement Range. Click a Coder to highlight it. Got it?",
            "No worries! The UI is like your IDE’s debug panel. Look at the bottom: green bars for HP, a sword icon for Attack, and a boot icon for Movement. Try clicking a Coder now. Ready?",
            "Nice! Let’s get moving. Each Coder can move up to 3 tiles on the grid per turn. Click your first Coder (Unit 1, the one with the Hack attack), then click a highlighted tile to move. Try moving toward that Glitch in the top left corner. Don’t wander into the red zones—those are enemy attack ranges! Go for it!",
            "Smooth move! Now, let’s Hack that Glitch. When your Coder is next to an enemy, click the Glitch to attack. Hack deals 10 damage, and this Glitch has 15 HP, so it’ll take two hits. Click the Glitch to unleash your attack. Show that bug who’s boss!",
            "That’s the spirit! Take out the last Glitch, and Level 1 is yours. I’ll pop back if you need me. Compile those Glitches!",
            "Keep going, there are more enemies to defeat!",
            "Come to the other side of the map and the next level will begin!"
        };

        textMeshPro.text = string.Empty;
        StartCoroutine(TypeDialogueSequence());
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
}*/