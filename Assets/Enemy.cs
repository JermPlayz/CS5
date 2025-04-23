
using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Vector3 pos;
    public List<Move> Moves;
    public Vector3 closestplayer; 
    void Start()
    {
        Moves = new List<Move>();
        foreach(var move in Base.LearnableMoves)
        {
            if(move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if(Moves.Count >= 4)
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePos(Vector3 newpos)
    {
        pos = newpos;
    }
}