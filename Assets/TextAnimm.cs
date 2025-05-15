using System.Collections;
using UnityEngine;
using TMPro;    

public class TextAnim : MonoBehaviour
{
    
    [SerializeField] private float timeBtwnChars = 0.05f;
    [SerializeField] private float timeBtwnWords = 1f;

    [Header("References")]
    [SerializeField] private TMP_Text textMeshPro;
    [SerializeField] private string[] stringArray;

    private int i = 0;

    void Start()
    {
        EndCheck();
    }

    void EndCheck()
    {
        // Null checks for debugging
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro reference not assigned!");
            return;
        }

        if (stringArray == null || stringArray.Length == 0)
        {
            Debug.LogError("stringArray is empty or not assigned!");
            return;
        }

        if (i <= stringArray.Length - 1)
        {
            textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());
        }
    }

    private IEnumerator TextVisible()
    {
        textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                Invoke(nameof(EndCheck), timeBtwnWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }
}
