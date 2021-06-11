using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChessEnum;

public class ChessBoard
{
    public ChessPiece[,] board { get; set; }

    public ChessBoard(ChessPiece[,] board)
    {
        this.board = board;
    }

    public ChessPiece this[Position position]
    {
        get => board[position.column, position.row];
        set => board[position.column, position.row] = value;
    }

    public ChessPiece GetPieceFromPosition(Position position)
    {   
        return board[position.column, position.row];
    }

    public bool IsSquareEmpty(Position position)
    {
        return board[position.column, position.row] == null;
    }
    public bool IsWhitePieceOnSquare(Position position)
    {
        return board[position.column, position.row] != null && board[position.column, position.row].color == ChessEnum.Color.White;
    }

    public bool IsBlackPieceOnSquare(Position position)
    {
        return board[position.column, position.row] != null && board[position.column, position.row].color == ChessEnum.Color.Black;
    }

    public bool SetPieceOnPosition(Position position, ChessPiece piece)
    {
        if (!position.IsPositionOnTheBoard()) return false;

        board[position.column, position.row] = piece;
        return true;

    }


    public void SetSquareEmpty(Position position)
    {
        board[position.column, position.row] = null;
    }

    public override bool Equals(object obj)
    {
        ChessBoard other = (ChessBoard)obj;

        for (int i = 0; i <= 7; i++)
            for (int j = 0; j <= 7; j++)
                if (board[i, j] != other.board[i, j])
                    return false;
        return true;
    }

    public override int GetHashCode()
    {
        if (board != null)
        {
            unchecked
            {
                int hash = 17;
                // get hash code for all items in array
                foreach (var item in board)
                {
                    hash = hash * 23 + ((item != null) ? item.GetHashCode() : 0);
                }

                return hash;
            }
        }

        // if null, hash code is zero
        return 0;
    }
    
}
