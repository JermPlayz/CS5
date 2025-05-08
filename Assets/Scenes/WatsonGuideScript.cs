using JetBrains.Annotations;
using UnityEngine;

public class WatsonGuideScript : MonoBehaviour
{
    public TMP_Text textMeshPro; // Reference to the TextMeshPro component
    public float typingSpeed = 0.01f; // Speed of typing in seconds

    private string fullText; // The complete text to be typed
    private string text1 = "Hello, rookie Coder! Welcome to CS5: Byte and Battle! I’m Watson, your AI strategist, here to keep you from getting compiled into oblivion. Ready to debug this mainframe?";
    private string text2 = "First, check your Player UI at the bottom of the screen. See the grid with your Coders’ stats? That’s your lifeline! It shows HP, Attack, and Movement Range. Click a Coder to highlight it. Got it?";
    private string text3NO = "No worries! The UI is like your IDE’s debug panel. Look at the bottom: green bars for HP, a sword icon for Attack, and a boot icon for Movement. Try clicking a Coder now. Ready?";
    private string text4 = "Nice! Let’s get moving. Each Coder can move up to 3 tiles on the grid per turn. Click your first Coder (Unit 1, the one with the Hack attack), then click a highlighted tile to move. Try moving toward that Glitch in the top left corner. Don’t wander into the red zones—those are enemy attack ranges! Go for it!";
    private string text5 = "Smooth move! Now, let’s Hack that Glitch. When your Coder is next to an enemy, click the Glitch to attack. Hack deals 10 damage, and this Glitch has 15 HP, so it’ll take two hits. Click the Glitch to unleash your attack. Show that bug who’s boss!";
    private void Start()
    {
        fullText = textMeshPro.text; // Store the full text
        textMeshPro.text = string.Empty; // Clear the text
        StartCoroutine(TypeText(text1)); // Start typing animation
        textMeshPro.text = string.Empty; // Clear the text
        StartCoroutine(TypeText(text2));
        textMeshPro.text = string.Empty; // Clear the text
        StartCoroutine(TypeText(text3NO));
        textMeshPro.text = string.Empty; // Clear the text
        StartCoroutine(TypeText(text4));
        textMeshPro.text = string.Empty; // Clear the text
        StartCoroutine(TypeText(text4));

    }

    // Coroutine to simulate typing effect
    public void TypeText( string fullText)
    {
        foreach (char letter in fullText)
        {
            textMeshPro.text += letter; // Append each letter to the text
            yield return new WaitForSeconds(typingSpeed); // Wait for the specified duration
        }
    }

    // make the code so tha it highlights each part of the UI and tells you what is what 

}
