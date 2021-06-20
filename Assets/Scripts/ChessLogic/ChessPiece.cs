using System;
using System.Collections.Generic;
using System.Text;
using static ChessEnum;
/// <summary>
/// Contains parameters of fiqure
/// </summary>
public class ChessPiece
{



    public Figure figure { get; set; }
    public Color color { get; set; }
    public bool isCastlingPossibleWest;
    public bool isCastlingPossibleEast;
    public bool isEnPassantPossibleWest;
    public bool isEnPassantPossibleEast;
    public bool didmove;
    public bool possibleEnPassant;

    /// <summary>
    /// Constructor, sets basic values of parameters
    /// </summary>
    /// <param name="figure"></param>
    /// <param name="color"></param>
    /// <param name="didmove"></param>
    /// <param name="possibleEnPassant"></param>
    public ChessPiece(Figure figure, Color color, bool didmove, bool possibleEnPassant=false)
    {
        this.figure = figure;
        this.color = color;
        this.isCastlingPossibleWest = true;
        this.isCastlingPossibleEast = true;
        this.isEnPassantPossibleWest = false;
        this.isEnPassantPossibleEast = false;
        this.didmove = didmove;
        this.possibleEnPassant = possibleEnPassant;
    }

    public override bool Equals(object obj)
    {
        ChessPiece other = (ChessPiece)obj;

        return figure == other.figure && color == other.color;
    }

}

