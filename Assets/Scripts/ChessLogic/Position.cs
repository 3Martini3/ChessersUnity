using System;

    public class Position
    {
        public int row { get; set; }
        public int column { get; set; }

        private bool isMoveOnThisPossitionEnPassant;
        private bool isMoveOnThisPossitionCastling;

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

        }
        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
            isMoveOnThisPossitionCastling = false;
            isMoveOnThisPossitionEnPassant = false;
        }

        public bool IsPositionOnTheBoard()
        {
            return (row <= 7 && row >= 0 && column <= 7 && column >= 0);
        }

        public bool IsMoveOnThisPositionCastling()
        {
            return this.isMoveOnThisPossitionCastling;
        }
        public bool IsMoveOnThisPositionEnPassant()
        {
            return this.isMoveOnThisPossitionEnPassant;
        }

        public void SetMoveOnThisPositionCastling(bool answer)
        {
            this.isMoveOnThisPossitionCastling = answer;
        }
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

