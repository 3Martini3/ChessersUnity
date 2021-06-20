using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChessEnum;

/// <summary>
/// Contains methods responsible for state of chess board and pieces 
/// </summary>
public class ChessBoard
{   
    /// <summary>
    /// Initializes two dimensional table 
    /// </summary>
    public ChessPiece[,] board { get; set; }

    /// <summary>
    /// Initialize starting boards state
    /// </summary>
    /// <param name="board"></param>
    public ChessBoard(ChessPiece[,] board)
    {
        this.board = board;
    }
    /// <summary>
    /// Manage ( set/get) data of Piece position on  board 
    /// </summary>
    /// <param name="position"></param>
    /// <returns>Position of chess piece </returns>
    public ChessPiece this[Position position]
    {
        get => board[position.column, position.row];
        set => board[position.column, position.row] = value;
    }
    /// <summary>
    /// Returns positon of piece on board
    /// </summary>
    /// <param name="position"></param>
    /// <returns> Position of piece on board</returns>
    public ChessPiece GetPieceFromPosition(Position position)
    {   
        return board[position.column, position.row];
    }
    /// <summary>
    /// Check state of board square
    /// </summary>
    /// <param name="position"></param>
    /// <returns> boolean value of board square emptiness </returns>
    public bool IsSquareEmpty(Position position)
    {
        return board[position.column, position.row] == null;
    }
    /// <summary>
    /// Check if white piece is on square
    /// </summary>
    /// <param name="position"></param>
    /// <returns> boolean value of is white piece prestent </returns>
    public bool IsWhitePieceOnSquare(Position position)
    {
        return board[position.column, position.row] != null && board[position.column, position.row].color == ChessEnum.Color.White;
    }
    /// <summary>
    /// Check if black piece is on square
    /// </summary>
    /// <param name="position"></param>
    /// <returns> boolean value of is black  piece prestent </returns>
    public bool IsBlackPieceOnSquare(Position position)
    {
        return board[position.column, position.row] != null && board[position.column, position.row].color == ChessEnum.Color.Black;
    }

    /// <summary>
    /// Sets chcess piece position on board
    /// </summary>
    /// <param name="position"></param>
    /// <param name="piece"></param>
    /// <returns> boolean state of seted piece </returns>
    public bool SetPieceOnPosition(Position position, ChessPiece piece)
    {
        if (!position.IsPositionOnTheBoard()) return false;

        board[position.column, position.row] = piece;
        return true;

    }

    /// <summary>
    /// Sets state of board square to empty
    /// </summary>
    /// <param name="position"></param>
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
