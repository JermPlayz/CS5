using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public CombatCtrl combatCtrl;
    public GameObject dialogueBox;
    public Image characterIcon;
    public Text dialogueText;
    public Text characterName;
    public int lettersPerSecond;
    public bool isDialogueActive = false;
    public GameObject nextSelector;
    public Button next;
    private Queue<DialogueLine> lines = new Queue<DialogueLine>();

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        EnableNextSelector(false);
    }
    public void ShowDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        EnableNextSelector(false);

        lines.Clear();

        foreach(DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if(lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeDialog(currentLine));
    }

    public IEnumerator TypeDialog(DialogueLine dialogueLine)
    {
        dialogueText.text = "";
        foreach(char letter in dialogueLine.line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
        yield return new WaitForSeconds(.5f);
        EnableNextSelector(true);
    }

    public void EnableNextSelector(bool enabled)
    {
        nextSelector.SetActive(enabled);
    }
    public void OnNextButton()
    {
        DisplayNextDialogueLine();
        EnableNextSelector(false);
    }
    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        combatCtrl.combatState = CombatState.COMBAT;
    }
}