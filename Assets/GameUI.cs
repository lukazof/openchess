using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    public Image mousePiece; // Will be the piece that follows the cursor
    public Game game;
    public static GameUI instance;

    private void Awake()
    {
        instance = this;
        game = Game.instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderMousePiece();
    }

    public void RenderMousePiece()
    {
        mousePiece.transform.position = Input.mousePosition;

        if(game.selectedSquare == null)
        {
            mousePiece.gameObject.SetActive(false);
            return;
        }
        else
        {
            mousePiece.gameObject.SetActive(true);

            switch (game.selectedSquare.piece.pieceType)
            {
                case Piece.EPiece_Type.None:
                    break;
                case Piece.EPiece_Type.Pawn:
                    mousePiece.sprite = Board.Instance.pawnSprite;
                    break;
                case Piece.EPiece_Type.Knight:
                    mousePiece.sprite = Board.Instance.knightSprite;
                    break;
                case Piece.EPiece_Type.Bishop:
                    mousePiece.sprite = Board.Instance.bishopSprite;
                    break;
                case Piece.EPiece_Type.Rook:
                    mousePiece.sprite = Board.Instance.rookSprite;
                    break;
                case Piece.EPiece_Type.Queen:
                    mousePiece.sprite = Board.Instance.queenSprite;
                    break;
                case Piece.EPiece_Type.King:
                    mousePiece.sprite = Board.Instance.kingSprite;
                    break;
                default:
                    break;
            }
        }

        
    }
}
