using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUI : MonoBehaviour
{
    public Sprite actionButton;
    public Sprite moveButton;
    public Sprite noButton;
    public GameObject actionSelector;
    public GameObject attackSelector;
    public List<Text> moveTexts;
    public List<Button> actionButtons;
    public List<Button> moveButtons;
    public CombatCtrl combatCtrl;

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
        for(int i = 0; i < actionButtons.Count; ++i)
        {
            actionButtons[i].interactable = enabled;
        }
    }
    public void OnAttackButton()
    {
        Debug.Log("Action Button");
        combatCtrl.combatState = CombatState.ATTACKS;
        EnableActionSelector(false);
        EnableAttackSelector(true);
    }
    public void OnItemsButton()
    {
        Debug.Log("Items Button");
    }
    public void OnWaitButton()
    {
        Debug.Log("Wait Button");
    }
    public void EnableAttackSelector(bool enabled)
    {
        attackSelector.SetActive(enabled);
        for(int i = 0; i < moveButtons.Count; ++i)
        {
            moveButtons[i].interactable = enabled;
        }
    }
    public void OnGoBackButton()
    {
        Debug.Log("Go Back Button");
        combatCtrl.combatState = CombatState.ACTION;
        EnableAttackSelector(false);
        EnableActionSelector(true);
    }
     public void NewButton(List<Move> moves)
     {
         for(int i = 0; i < moveButtons.Count; ++i)
         {
             if(i < moves.Count)
             {
//                 if(moves[i].Base.Type == CharacterType.Hacker)
//                 {
//                     moveButtons[i].image.sprite = hackerButton;
//                 }
//                 else if(moves[i].Base.Type == CharacterType.Data_Structurer)
//                 {
//                     moveButtons[i].image.sprite = dsButton;
//                 }
//                 else if(moves[i].Base.Type == CharacterType.Debugger)
//                 {
//                     moveButtons[i].image.sprite = debuggerButton;
//                 }
//                 else if(moves[i].Base.Type == CharacterType.IT_Support)
//                 {
//                     moveButtons[i].image.sprite = itsButton;
//                 }
                 moveTexts[i].text = moves[i].Base.Name;
             }
             else
             {
                 moveButtons[i].image.sprite = noButton;
                 moveButtons[i].interactable = false;
             }
         }
    }
}