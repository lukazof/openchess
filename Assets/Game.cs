using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;

    public Square selectedSquare;

    public Piece.EPiece_Color colorTurn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        colorTurn = Piece.EPiece_Color.White;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
