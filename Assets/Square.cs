using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public int index;
    public string coord;

    public Piece piece;
    
    void Start()
    {
        
    }

    void Update()
    {
        piece = GetComponentInChildren<Piece>();
    }

    private void OnMouseDown()
    {
        bool hasSelected = Game.instance.selectedSquare != null;

        if(!hasSelected)
        {
            SelectSquare();
        }
        
    }

    private void OnMouseUp()
    {
        piece.ChangePiece(Game.instance.selectedSquare.piece.pieceType, Game.instance.selectedSquare.piece.pieceColor);
        Game.instance.selectedSquare = null;
    }

    public void SelectSquare()
    {
        Game.instance.selectedSquare = this;
    }

}

