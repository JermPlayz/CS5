
using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] CharacterBase Base;
    [SerializeField] int Level;
    public Vector3 pos;
    public List<Move> Moves;
    public List<GameObject> players;
    public Vector3 closestplayer; 
    public GameObject player1; //remove after alpha
    public int moveconstraint;
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
        //remove after alpha
        closestplayer = player1.transform.position;
        pos = new Vector3Int(1, 0, 0);
        moveconstraint = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //foreach(GameObject player in players)
    }

    public void UpdatePos(Vector3 newpos)
    {
        transform.position = newpos;
        pos = newpos;
    }

    public Vector3 Getpos()
    {
        return(pos);
    }
}