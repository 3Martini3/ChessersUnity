using System;
using System.Collections.Generic;
using System.Text;
using static ChessEnum;

public class ChessPiece
    {
      
      

        public Figure figure { get; set; }
        public Color color { get; set; }
        public bool isCastlingPossibleWest;
        public bool isCastlingPossibleEast;
        public bool isEnPassantPossibleWest;
        public bool isEnPassantPossibleEast;

        public ChessPiece(Figure figure, Color color)
        {
            this.figure = figure;
            this.color = color;
            this.isCastlingPossibleWest = true;
            this.isCastlingPossibleEast = true;
            this.isEnPassantPossibleWest = false;
            this.isEnPassantPossibleEast = false;
        }

        public override bool Equals(object obj)
        {
            ChessPiece other = (ChessPiece)obj;

            return figure == other.figure && color == other.color;
        }

    }

