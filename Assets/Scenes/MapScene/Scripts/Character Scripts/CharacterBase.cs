using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new character")]
public class CharacterBase : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] string name;
    [TextArea]
    [SerializeField] string description;
    //[SerializeField] Sprite frontSprite;
    // [SerializeField] Sprite backSprite;
    [SerializeField] CharacterType type1;

    //Base Stats
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] int expYield;
    [SerializeField] GrowthRate growthRate;

    [SerializeField] List<LearnableMove> learnableMoves;

    public int GetExpForLevel(int level)
    {
        if(growthRate == GrowthRate.Fast)
        {
            return 4 * (level * level * level) / 5;
        }
        else if(growthRate == GrowthRate.MediumFast)
        {
            return level * level * level;
        }

        return -1;
    }

    public Sprite Sprite
    {
        get{return sprite;}
    }
    public string Name
    {
        get{return name;}
    }
    public string Description
    {
        get{return description;}
    }
    // public Sprite FrontSprite
    // {
    //     get{return frontSprite;}
    // }
    // public Sprite BackSprite
    // {
    //     get{return backSprite;}
    // }
    public CharacterType Type1
    {
        get{return type1;}
    }
    public int MaxHP
    {
        get{return maxHP;}
    }
    public int Attack
    {
        get{return attack;}
    }
    public int Defense
    {
        get{return defense;}
    }
    public int SpAttack
    {
        get{return spAttack;}
    }
    public int SpDefense
    {
        get{return spDefense;}
    }
    public int Speed
    {
        get{return speed;}
    }
    public List<LearnableMove> LearnableMoves
    {
        get{return learnableMoves;}
    }
    public int ExpYield => expYield;

    public GrowthRate GrowthRate => growthRate;
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get{return moveBase;}
    }
    public int Level
    {
        get{return level;}
    }
}

public enum CharacterType
{
    None,
    Hacker,
    Data_Structurer,
    Debugger,
    IT_Support
}
public enum GrowthRate
{
    Fast, MediumFast
}
public enum Stat
{
    Attack,
    Defense,
    SpAttack,
    SpDefense,
    Speed,
    Accuracy,
    Evasion
}