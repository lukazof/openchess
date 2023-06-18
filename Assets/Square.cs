using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public int index;
    public Vector2 coord;
    public Piece piece;

    public int numNorth;
    public int numSouth;
    public int numWest;
    public int numEast;

    
    void Start()
    {
        coord = transform.position;

        name = coord.ToString("F0");

        index = (int)coord.y * 8 + (int)coord.x - 9;
    }

    void Update()
    {
        piece = GetComponentInChildren<Piece>();

    }

    private void OnMouseDown()
    {
        Square selected = Game.instance.selectedSquare;
        Piece myPiece = piece;

        if(selected == null)
        {
            if(myPiece.pieceType != Piece.EPiece_Type.None) SelectSquare();
        }

        else
        {
            if(selected.piece.pieceType != Piece.EPiece_Type.None)
            {
                Move move;
                move.initialSquare = selected;
                move.finalSquare = this;

                if(selected.piece.pieceColor == piece.pieceColor && piece.pieceType != Piece.EPiece_Type.None)
                {
                    SelectSquare();
                    return;
                }

                if (Engine.instance.ValidateMove(move))
                {
                    myPiece.ChangePiece(selected.piece.pieceType, selected.piece.pieceColor);
                    selected.piece.ChangePiece(Piece.EPiece_Type.None, Piece.EPiece_Color.White);
                    UnselectSquare();
                }
                else
                {
                    UnselectSquare();
                }
            }
        }
        
    }


    public void SelectSquare()
    {
        Game.instance.selectedSquare = this;
        //print("Selecting Square:" + name);
    }

    public void UnselectSquare()
    {
        //print("Unselecting Square: " + Game.instance.selectedSquare.name);
        Game.instance.selectedSquare = null;
    }

}

