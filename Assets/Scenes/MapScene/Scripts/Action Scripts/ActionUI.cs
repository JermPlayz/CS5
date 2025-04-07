using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUI : MonoBehaviour
{
    public Sprite Button;
    public GameObject actionSelector;
    public List<Button> actionButtons;

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
        for(int i = 0; i < actionButtons.Count; ++i)
        {
            actionButtons[i].interactable = enabled;
        }
    }
    // public void EnableMoveSelector(bool enabled)
    // {
    //     moveSelector.SetActive(enabled);
    //     for(int i = 0; i < moveButtons.Count; ++i)
    //     {
    //         moveButtons[i].interactable = enabled;
    //     }
    // }

//     public void NewButton(List<Move> moves)
//     {
//         for(int i = 0; i < moveButtons.Count; ++i)
//         {
//             if(i < moves.Count)
//             {
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
//                 moveTexts[i].text = moves[i].Base.Name;
//             }
//             else
//             {
//                 moveButtons[i].image.sprite = noButton;
//                 moveButtons[i].interactable = false;
//             }
//         }
//     }
}