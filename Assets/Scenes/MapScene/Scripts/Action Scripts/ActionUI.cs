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
    public CharacterUnit characterUnit;

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

     public void NewButton(List<Move> moves)
     {
         for(int i = 0; i < moveButtons.Count; ++i)
         {
             if(i < moves.Count)
             {
//                 if(moves[i].Base.Type == InstrumentType.Music)
//                 {
//                     moveButtons[i].image.sprite = musicButton;
//                 }
//                 else if(moves[i].Base.Type == InstrumentType.Brass)
//                 {
//                     moveButtons[i].image.sprite = brassButton;
//                 }
//                 else if(moves[i].Base.Type == InstrumentType.Woodwind)
//                 {
//                     moveButtons[i].image.sprite = woodwindButton;
//                 }
//                 else if(moves[i].Base.Type == InstrumentType.Percussion)
//                 {
//                     moveButtons[i].image.sprite = percButton;
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