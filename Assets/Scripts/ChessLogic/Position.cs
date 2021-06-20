using System;

/// <summary>
/// Function manages position and posibilities of moves connected with position
/// </summary>
public class Position
{
    public int row { get; set; }
    public int column { get; set; }
    public bool enPassantPossible { get; set; }
    public bool enPassantBeat { get; set; }
    /// <summary>
    /// 0 - if not,
    /// 1 - if left
    /// 2- if right
    /// </summary>
    public int castling { get; set; }
    
    private bool isMoveOnThisPossitionEnPassant;
    private bool isMoveOnThisPossitionCastling;

    /// <summary>
    /// Set states of moves conected with position
    /// </summary>
    /// <param name="position"></param>
    public Position(string position)
    {
        if (position.Length != 2)
            throw new Exception("Wrong Position");

        char row = position[1];
        char column = position[0];

        this.column = column - 'A';
        this.row = row - '1';
        isMoveOnThisPossitionCastling = false;
        isMoveOnThisPossitionEnPassant = false;
        enPassantPossible = false;
        enPassantBeat = false;

    }
    /// <summary>
    /// Set basic values of postion posibilities 
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="enpassant"></param>
    /// <param name="castling"></param>
    /// <param name="enPassantBeat"></param>
    public Position(int row, int column, bool enpassant = false,int castling = 0, bool enPassantBeat=false)
    {
        this.row = row;
        this.column = column;
        isMoveOnThisPossitionCastling = false;
        isMoveOnThisPossitionEnPassant = false;
        enPassantPossible = enpassant;
        this.castling = castling;
        this.enPassantBeat = enPassantBeat;
        
    }
    /// <summary>
    /// chceck is positon on board
    /// </summary>
    /// <returns>boolean state of position</returns>
    public bool IsPositionOnTheBoard()
    {
        return (row <= 7 && row >= 0 && column <= 7 && column >= 0);
    }
    /// <summary>
    /// Return is move Castling
    /// </summary>
    /// <returns>boolean state of castling</returns>
    public bool IsMoveOnThisPositionCastling()
    {
        return this.isMoveOnThisPossitionCastling;
    }
    /// <summary>
    /// Return is move EnPassant
    /// </summary>
    /// <returns>boolean state of EnPassant</returns>
    public bool IsMoveOnThisPositionEnPassant()
    {
        return this.isMoveOnThisPossitionEnPassant;
    }
    /// <summary>
    /// Set state of Castling move on position
    /// </summary>
    /// <param name="answer"></param>
    public void SetMoveOnThisPositionCastling(bool answer)
    {
        this.isMoveOnThisPossitionCastling = answer;
    }
    /// <summary>
    /// Set state of EnPassant move on position
    /// </summary>
    /// <param name="answer"></param>
    public void SetMoveOnThisPositionEnPassant(bool answer)
    {
        this.isMoveOnThisPossitionEnPassant = answer;
    }

    //Determines whether two object instances are equal.
    public override bool Equals(object obj)
    {
        Position other = (Position)obj;

        return column == other.column && row == other.row;
    }

    // Method provides this hash code for algorithms that need quick checks of object equality
    public override int GetHashCode()
    {
        return column * 8 + row;
    }

}

