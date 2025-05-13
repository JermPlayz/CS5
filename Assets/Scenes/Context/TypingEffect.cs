using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;             // Reference to TextMeshPro UI element
    public float typingSpeed = 0.01f;        // Delay between each letter

    private string fullText;                 // The full text to display
    private Coroutine typingCoroutine;       // Reference to the running coroutine
    private bool isTyping = false;           // Is the animation still running?

    void Update()
    {
        // If typing and player presses Space, show full text instantly
        if (isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(typingCoroutine);
            textMeshPro.text = fullText;
            isTyping = false;
        }
    }

    // Call this method to start typing a new message
    public void StartTyping(string newText)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        fullText = newText;
        textMeshPro.text = string.Empty;
        typingCoroutine = StartCoroutine(TypeText());
    }

    // Coroutine to animate text appearance
    private IEnumerator TypeText()
    {
        isTyping = true;

        foreach (char letter in fullText)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}