using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public static Engine instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Will check if the specified move is valid
    public bool ValidateMove(Move move)
    {
        Square initial = move.initialSquare;
        Square final = move.finalSquare;

        if (initial == final) { print($"Move {initial.coord}-{final.coord} is not valid"); return false; }

        switch (initial.piece.pieceType)
        {
            case Piece.EPiece_Type.Pawn:
                return Pawn(initial, final);
                break;
            case Piece.EPiece_Type.Knight:
                return Knight(initial, final);
                break;
            case Piece.EPiece_Type.Bishop:
                break;
            case Piece.EPiece_Type.Rook:
                return Rook(initial, final);
                break;
            case Piece.EPiece_Type.Queen:
                break;
            case Piece.EPiece_Type.King:
                return King(initial, final);
                break;
            default:
                break;
        }


        print($"Move {initial.coord}-{final.coord} is not valid");
        return false;

    }

    private bool Pawn(Square initial, Square final)
    {
        int colorChecker = initial.piece.pieceColor == Piece.EPiece_Color.White ? 1 : -1;
        bool hasntMoved = (initial.piece.pieceColor == Piece.EPiece_Color.White ? 2 : 7) == initial.coord.y ? true : false;

        // Moving Forward
        if (final.coord.y == initial.coord.y + 1 * colorChecker)
        {
            if (final.coord.x == initial.coord.x && final.piece.pieceType == Piece.EPiece_Type.None)
            {
                return true;
            }
            else if (final.coord.x == initial.coord.x + 1 || final.coord.x == initial.coord.x - 1)
            {
                return final.piece.pieceType != Piece.EPiece_Type.None;
            }
        }
        else if (final.coord.y == initial.coord.y + 2 * colorChecker && hasntMoved)
        {
            return final.coord.x == initial.coord.x && final.piece.pieceType == Piece.EPiece_Type.None;
        }

        return false;

    }

    private bool King(Square initial, Square final)
    {

        int difference = final.index - initial.index;

        return difference == 1 || difference == -1 || difference == 7 || difference == -7 || difference == 8 || difference == -8 || difference == 9 || difference == -9;
    }

    private bool Knight(Square initial, Square final)
    {
        int difference = final.index - initial.index;

        return difference == 10 || difference == -10 || difference == 15 || difference == -15 || difference == 17 || difference == -17 || difference == 6 || difference == -6;
    }

    private bool Rook(Square initial, Square final)
    {
        return SlidingPiece(initial, final);

    }

    public bool Queen(Square initial, Square final)
    {
        foreach (Square currentSquare in Board.Instance.squares)
        {
            int xDifference = (int)final.coord.x - (int)initial.coord.x;
            int yDifference = (int)final.coord.y - (int)initial.coord.y;

            bool betweenY = yDifference > 0 ? currentSquare.coord.y > initial.coord.y && currentSquare.coord.y < final.coord.y : currentSquare.coord.y < initial.coord.y && currentSquare.coord.y > final.coord.y;
            bool betweenX = xDifference > 0 ? currentSquare.coord.x > initial.coord.x && currentSquare.coord.x < final.coord.x : currentSquare.coord.x < initial.coord.x && currentSquare.coord.x > final.coord.x;

            bool vertical = initial.coord.x == final.coord.x;
            bool horizontal = initial.coord.y == final.coord.y;

            // Moving the Rook vertically
            if (vertical && betweenY)
            {
                if (currentSquare.piece.pieceType != Piece.EPiece_Type.None && currentSquare.coord.x == initial.coord.x)
                {
                    print($"Piece blocking at square {currentSquare.coord}");
                    return false;
                }
            }

            // Moving the Rook horizontally
            if (horizontal && betweenX && currentSquare.coord.y == initial.coord.y)
            {
                if (currentSquare.piece.pieceType != Piece.EPiece_Type.None)
                {
                    print($"Piece blocking at square {currentSquare.coord}");
                    return false;
                }
            }
        }

        return true;
    }

    private bool Bishop(Square initial, Square final)
    {
        return true;
    }

    private bool SlidingPiece(Square initial, Square final)
    {
        foreach (Square currentSquare in Board.Instance.squares)
        {
            int difference = final.index - initial.index;
            int xDifference = (int)final.coord.x - (int)initial.coord.x;
            int yDifference = (int)final.coord.y - (int)initial.coord.y;

            bool betweenY = yDifference > 0 ? currentSquare.coord.y > initial.coord.y && currentSquare.coord.y < final.coord.y : currentSquare.coord.y < initial.coord.y && currentSquare.coord.y > final.coord.y;
            bool betweenX = xDifference > 0 ? currentSquare.coord.x > initial.coord.x && currentSquare.coord.x < final.coord.x : currentSquare.coord.x < initial.coord.x && currentSquare.coord.x > final.coord.x;

            bool vertical = initial.coord.x == final.coord.x;
            bool horizontal = initial.coord.y == final.coord.y;

            bool diagonal = initial.coord.x != final.coord.x && initial.coord.y != final.coord.y;

            // Moving the Piece vertically
            if (vertical && betweenY)
            {
                if (currentSquare.piece.pieceType != Piece.EPiece_Type.None && currentSquare.coord.x == initial.coord.x)
                {
                    print($"Piece blocking at square {currentSquare.coord}");
                    return false;
                }
            }

            //Moving the Piece horizontally
            if (horizontal && betweenX && currentSquare.coord.y == initial.coord.y)
            {
                if (currentSquare.piece.pieceType != Piece.EPiece_Type.None)
                {
                    print($"Piece blocking at square {currentSquare.coord}");
                    return false;
                }
            }

            if ((diagonal && betweenX && betweenY))
            {
                // Moving UP RIGHT
                if (xDifference > 0 && yDifference > 0 && difference % 9 == 0)
                {
                    if (currentSquare.piece.pieceType != Piece.EPiece_Type.None)
                    {
                        if(final.index - currentSquare.index % 9 == 0)
                        {
                            return false;
                        }
                        
                    }
                }

                // Moving DOWN RIGHT
                if (xDifference > 0 && yDifference < 0 && difference % 7 == 0)
                {
                    if (currentSquare.piece.pieceType != Piece.EPiece_Type.None)
                    {
                        if (final.index - currentSquare.index % 7 == 0)
                        {
                            return false;
                        }
                    }
                }

                // Moving DOWN LEFT
                if (xDifference < 0 && yDifference < 0 && difference % 9 == 0)
                {
                    if (currentSquare.piece.pieceType != Piece.EPiece_Type.None)
                    {
                        if (final.index - currentSquare.index % 9 == 0)
                        {
                            return false;
                        }
                    }
                }

                // Moving UP LEFT
                if (xDifference < 0 && yDifference > 0 && difference % 7 == 0)
                {
                    if (currentSquare.piece.pieceType != Piece.EPiece_Type.None)
                    {
                        if (final.index - currentSquare.index % 7 == 0)
                        {
                            return false;
                        }
                    }
                }
            }

        }

        return true;
    }

    private void CalculateToEdge()
    {
        foreach (Square square in Board.Instance.squares)
        {
            //square.
        }
    }
}

public struct Move
{
    public Square initialSquare;
    public Square finalSquare;
}
