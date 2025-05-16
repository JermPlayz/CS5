using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScrollText : MonoBehaviour
{
    public TMP_Text textMeshPro; // Reference to the TextMeshPro component
    public float typingSpeed = 0.01f; // Speed of typing in seconds

    private string[] dialogueTexts;
    private int currentStep = 0;

    private void Start()
    {
        dialogueTexts = new string[]
        {
           "Chi-Chi, Temi, Jeremy, and Tyler, are about to enter a tech-battle! Your classroom has transformed into a tactical battlefield of doom and destruction!",

"Get ready for tile-based combat, where every move you make on the grid counts. You'll each be playing as yourselves, but with super cool computer science-themed attacks! Cool your CPU as you start this epic adventure!",

"But watch out! You never know who the enemy may be... and they're not holding back! You'll need to cook to defeat them.",

"The ultimate showdown? A final boss battle! You'll need all your skills to win!",

"Your goal is to make it through this single, super-long level to save the day! Good luck, you've got this!"

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
}
