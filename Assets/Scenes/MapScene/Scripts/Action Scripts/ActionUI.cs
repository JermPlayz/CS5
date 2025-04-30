using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class ActionUI : MonoBehaviour
{
    public Sprite actionButton;
    public Sprite moveButton;
    public Sprite noButton;
    public GameObject actionSelector;
    public GameObject attackSelector;
    public GameObject useSelector;
    public GameObject enemySelector;
    public List<Text> moveTexts;
    public List<Text> statTexts;
    public List<Button> actionButtons;
    public List<Button> moveButtons;
    public Button useButton;
    public CharacterUnit characterUnit;
   // public CombatCtrl combatCtrl;

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
        EnableActionSelector(false);
        //combatCtrl.combatState = CombatState.ENDTURN;

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
        EnableUseSelector(false);
        EnableActionSelector(true);
    }
    public void NewButton(List<Move> moves)
    {
        for(int i = 0; i < moveButtons.Count; ++i)
        {
            if(i < moves.Count)
            {
                moveTexts[i].text = moves[i].Base.Name;
            }
            else
            {
                moveButtons[i].image.sprite = noButton;
                moveButtons[i].interactable = false;
            }
        }
    }
    public void OnMove1Button()
    {
        Debug.Log("Move 1 Button");
        statTexts[0].text = $"{characterUnit.Character.Moves[0].Base.Power}";
        statTexts[1].text = $"{characterUnit.Character.Moves[0].Base.Accuracy}";
        statTexts[2].text = $"{characterUnit.Character.Moves[0].Base.PP}";
        statTexts[3].text = $"{characterUnit.Character.Moves[0].Base.Range}";
        EnableUseSelector(true);
    }
    public void OnMove2Button()
    {
        Debug.Log("Move 2 Button");
        statTexts[0].text = $"{characterUnit.Character.Moves[1].Base.Power}";
        statTexts[1].text = $"{characterUnit.Character.Moves[1].Base.Accuracy}";
        statTexts[2].text = $"{characterUnit.Character.Moves[1].Base.PP}";
        statTexts[3].text = $"{characterUnit.Character.Moves[1].Base.Range}";
        EnableUseSelector(true);
    }
    public void OnMove3Button()
    {
        Debug.Log("Move 3 Button");
        statTexts[0].text = $"{characterUnit.Character.Moves[2].Base.Power}";
        statTexts[1].text = $"{characterUnit.Character.Moves[2].Base.Accuracy}";
        statTexts[2].text = $"{characterUnit.Character.Moves[2].Base.PP}";
        statTexts[3].text = $"{characterUnit.Character.Moves[2].Base.Range}";
        EnableUseSelector(true);
    }
    public void OnMove4Button()
    {
        Debug.Log("Move 4 Button");
        statTexts[0].text = $"{characterUnit.Character.Moves[3].Base.Power}";
        statTexts[1].text = $"{characterUnit.Character.Moves[3].Base.Accuracy}";
        statTexts[2].text = $"{characterUnit.Character.Moves[3].Base.PP}";
        statTexts[3].text = $"{characterUnit.Character.Moves[3].Base.Range}";
        EnableUseSelector(true);
    }
    public void EnableUseSelector(bool enabled)
    {
        useSelector.SetActive(enabled);
        //combatCtrl.combatState = CombatState.STATS;
    }
    public void OnUseButton()
    {
        Debug.Log("Use Button");
        EnableAttackSelector(false);
        EnableUseSelector(false);
        EnableEnemySelector(true);
    }
    public void EnableEnemySelector(bool enabled)
    {
        enemySelector.SetActive(enabled);
        //combatCtrl.combatState = CombatState.ENEMYSELECT;
    }
}
*/