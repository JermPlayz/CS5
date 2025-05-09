using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUnit : MonoBehaviour
{
    public CharacterBase _base;
    public string Name;
    public int level;
    public bool isPlayerUnit;
    public Character Character{get; set;}
    public int CurrentHP{get; set;}
    public int moveconstraint;
    
    public void Setup()
    {
        Character = new Character(_base, level);
    }
}
