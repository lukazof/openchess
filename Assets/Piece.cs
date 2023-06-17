using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Piece;

public class Piece : MonoBehaviour
{
    public EPiece_Type pieceType;
    public EPiece_Color pieceColor;
    public SpriteRenderer pieceSprite;

    void Awake()
    {
        pieceSprite = GetComponent<SpriteRenderer>();
        //ChangePiece(EPiece_Type.None, EPiece_Color.White);
    }

    void Update()
    {
        pieceSprite.enabled = (pieceType != EPiece_Type.None);
    }

    public void ChangePiece(EPiece_Type newPieceType, EPiece_Color newPieceColor)
    {
        pieceType = newPieceType;
        pieceColor = newPieceColor;


        switch (pieceType)
        {
            case EPiece_Type.None:
                pieceSprite.enabled = false;
                break;
            case EPiece_Type.Pawn:
                pieceSprite.sprite = Board.Instance.pawnSprite;
                break;
            case EPiece_Type.Knight:
                pieceSprite.sprite = Board.Instance.knightSprite;
                break;
            case EPiece_Type.Bishop:
                pieceSprite.sprite = Board.Instance.bishopSprite;
                break;
            case EPiece_Type.Rook:
                pieceSprite.sprite = Board.Instance.rookSprite;
                break;
            case EPiece_Type.Queen:
                pieceSprite.sprite = Board.Instance.queenSprite;
                break;
            case EPiece_Type.King:
                pieceSprite.sprite = Board.Instance.kingSprite;
                break;
            default:
                break;
        }

        if(pieceColor == EPiece_Color.Black)
        {
            pieceSprite.color = Color.black;
        }
        else
        {
            pieceSprite.color = Color.white;
        }

        if (pieceType != EPiece_Type.None)
        {
            pieceSprite.enabled = true;
        }
    }

    public enum EPiece_Type
    {
        None, Pawn, Knight, Bishop, Rook, Queen, King,
    }

    public enum EPiece_Color
    {
        White, Black
    }
}
