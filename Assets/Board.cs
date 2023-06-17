using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public GameObject squareObject;

    public Square[] squares;

    public Color lightColor = Color.white;
    public Color darkColor = Color.black;

    public Sprite pawnSprite, knightSprite, bishopSprite, rookSprite, queenSprite, kingSprite;

    public string defaultPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GenerateBoard();
        LoadPosition(defaultPosition);
        GetSquares();
    }


    // Will create the physical Board (8x8)
    void GenerateBoard()
    {
        for (int x = 1; x <= 8; x++)
        {
            for (int y = 1; y <= 8; y++)
            {
                squareObject.GetComponent<Square>().transform.position = new Vector2(x, y);
                squareObject.GetComponent<SpriteRenderer>().color = (x + y) % 2 == 0 ? darkColor : lightColor;
                Instantiate(squareObject, new Vector3(x, y, 0), Quaternion.identity, transform);
            }
        }
    }


    // Will loop through the Board in search of the Squares
    private void GetSquares()
    {
        squares = GetComponentsInChildren<Square>();
    }


    // Will load a fen string
    public void LoadPosition(string fen)
    {
        var pieceSymbol = new Dictionary<char, Piece.EPiece_Type>()
        {
            ['k'] = Piece.EPiece_Type.King,
            ['p'] = Piece.EPiece_Type.Pawn,
            ['n'] = Piece.EPiece_Type.Knight,
            ['r'] = Piece.EPiece_Type.Rook,
            ['q'] = Piece.EPiece_Type.Queen,
            ['b'] = Piece.EPiece_Type.Bishop
        };

        string fenBoard = fen.Split(' ')[0];
        int x = 1, y = 8;

        foreach (char symbol in fenBoard)
        {
            if (symbol == '/')
            {
                x = 1;
                y--;
            }
            else
            {
                if (char.IsDigit(symbol))
                {
                    x += (int)char.GetNumericValue(symbol);
                }
                else
                {
                    Piece.EPiece_Color pColor = (char.IsUpper(symbol)) ? Piece.EPiece_Color.White : Piece.EPiece_Color.Black;
                    Piece.EPiece_Type pType = (pieceSymbol[char.ToLower(symbol)]);
                    foreach (Transform child in transform)
                    {
                        Square childSquare = child.GetComponent<Square>();
                        if (childSquare.transform.position.x == x && childSquare.transform.position.y == y)
                        {
                            childSquare.GetComponentInChildren<Piece>().ChangePiece(pType, pColor);
                        }

                    }

                    x++;
                }
            }
        }
    }


}

