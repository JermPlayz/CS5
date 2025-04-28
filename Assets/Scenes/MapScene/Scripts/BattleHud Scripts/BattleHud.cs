using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Image characterSprite;
    public Text nameText;
    public Text levelText;
    public Text typeText;
    public HPBar hpBar;
    CharacterUnit _characterUnit;

    public void SetHUD(CharacterUnit characterUnit)
    {
        _characterUnit = characterUnit;

        characterSprite.sprite = characterUnit.Character.Base.Sprite;
        nameText.text = characterUnit.Name;
        typeText.text = $"{characterUnit.Character.Base.Type1}";
        SetLevel();
        hpBar.SetHP((float) characterUnit.Character.HP / characterUnit.Character.MaxHP);
    }

    public void SetLevel()
    {
        levelText.text = $"{_characterUnit.level}";
    }

    public void UpdateHP()
    {
        hpBar.SetHP((float) _characterUnit.Character.HP / _characterUnit.Character.MaxHP);
    }
}