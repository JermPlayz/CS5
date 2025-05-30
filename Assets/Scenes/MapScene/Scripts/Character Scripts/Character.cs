using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Character
{
    [SerializeField] CharacterBase _base {get; set;}
    [SerializeField] int level {get; set;}

    public int Exp{get; set;}

    public int HP{get; set;}

    public List<Move> Moves {get; set;}
    public bool isDeclared{get; set;}

    public Dictionary<Stat, int> Stats {get; private set;}
    public Dictionary<Stat, int> StatBoosts {get; private set;}

    public Character(CharacterBase iBase, int iLevel)
    {
        _base = iBase;
        level = iLevel;

        Init();
    }

    public void Init()
    {
        //Generate Moves
        Moves = new List<Move>();
        foreach(var move in Base.LearnableMoves)
        {
            if(move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if(Moves.Count >= 4)
                break;
        }

        Exp = Base.GetExpForLevel(Level);

        CalculateStats();
        HP = MaxHP;
        Debug.Log($"HP: {HP}");

        ResetStatBoost();
    }

    void CalculateStats()
    {
        Stats = new Dictionary<Stat, int>();
        Stats.Add(Stat.Attack, Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5);
        Stats.Add(Stat.Defense, Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5);
        Stats.Add(Stat.SpAttack, Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5);
        Stats.Add(Stat.SpDefense, Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5);
        Stats.Add(Stat.Speed, Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5);

        MaxHP = Base.MaxHP;
    }

    void ResetStatBoost()
    {
        StatBoosts = new Dictionary<Stat, int>()
        {
            {Stat.Attack, 0},
            {Stat.Defense, 0},
            {Stat.Accuracy, 0},
            {Stat.Evasion, 0},

        };
    }
    public CharacterBase Base
    {
        get{return _base;}
    }
    public int Level
    {
        get{return level;}
    }

    public bool CheckForLevelUp()
    {
        if(Exp > Base.GetExpForLevel(level + 1))
        {
            level++;
            return true;
        }
        return false;
    }

    public LearnableMove GetLearnableMoveAtCurrentLevel()
    {
        return Base.LearnableMoves.Where(x => x.Level == level).FirstOrDefault();
    }

    public void LearnMove(LearnableMove moveToLearn)
    {
        if(Moves.Count > 4)
            return;

        Moves.Add(new Move(moveToLearn.Base));
    }

    int GetStat(Stat stat)
    {
        int statVal = Stats[stat];

        int boost = StatBoosts[stat];
        var boostValues = new float[] {1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f};

        if(boost >= 0)
            statVal = Mathf.FloorToInt(statVal * boostValues[boost]);
        else
            statVal = Mathf.FloorToInt(statVal / boostValues[boost]);

        return statVal;
    }

    public int Attack
    {
        get{return GetStat(Stat.Attack);}
    }
    public int Defense
    {
        get{return GetStat(Stat.Defense);}
    }
    public int SpAttack
    {
        get{return GetStat(Stat.SpAttack);}
    }
    public int SpDefense
    {
        get{return GetStat(Stat.SpDefense);}
    }
    public int Speed
    {
        get{return GetStat(Stat.Speed);}
    }
    public int MaxHP {get; private set;}

    public bool TakeDamage(Move move, Character attacker)
    {
        int damage = move.Base.Power;
        Debug.Log($"Move Base Power: {move.Base.Power}");

        HP -= damage;
        if(HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}
