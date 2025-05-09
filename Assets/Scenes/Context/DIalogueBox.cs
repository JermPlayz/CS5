using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic; // Import TextMeshPro
/*
public class TypingEffect : MonoBehaviour
{
    public TMP_Text textMeshPro; // Reference to the TextMeshPro component
    public float typingSpeed = 0.01f; // Speed of typing in seconds

    private string fullText; // The complete text to be typed
    private string text1 = "Nice! Let’s get moving. Each Coder can move up to 3 tiles on the grid per turn. Click your first Coder (Unit 1, the one with the Hack attack), then click a highlighted tile to move. Try moving toward that Glitch in the top left corner. Don’t wander into the red zones—those are enemy attack ranges! Go for it!";
    private string text2 = "Smooth move! Now, let’s Hack that Glitch. When your Coder is next to an enemy, click the Glitch to attack. Hack deals 10 damage, and this Glitch has 15 HP, so it’ll take two hits. Click the Glitch to unleash your attack. Show that bug who’s boss!";
    private string text3 = "C";
    private string text4 = "";
    private string text5 = "";
    private string text6 = "";
    private void int a = 0;
    public List(int a) randomTextNum;
    private void randomText()
    {
        foreach number in 
    }
    private void Start()
    {
        fullText = textMeshPro.text; // Store the full text
        textMeshPro.text = string.Empty; // Clear the text
        StartCoroutine(TypeText()); // Start typing animation
    }


    // Coroutine to simulate typing effect
    IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            textMeshPro.text += letter; // Append each letter to the text
            yield return new WaitForSeconds(typingSpeed); // Wait for the specified duration
        }
    }
}
*/