using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ChessEnum;

/// <summary>
/// Class responsible for all form od checking possible moves and functions contected with move
/// </summary>
public class PossibleMoves : MonoBehaviour
{
    public Figure figure;
    public UnityChessBoard unityBoard;
    public UnityEngine.Color activeColor;
    public bool didMove;
    public bool enPassauntPossible;

    /// <summary>
    /// Convert chess board to two dimensional table for game logic, then sends to function checking for 
    /// figure which moves need to be chcecked 
    /// </summary>
    public void FindPossibleMoves()
    {
        //Debug.Log("Searching");
        ChessPiece[,] board = new ChessPiece[8, 8];
        var sq = unityBoard.gameObject.GetComponent<UnityChessBoard>().squares;
        var currentPiece = GetComponent<UnityChessPiece>();
        var currentSquare = currentPiece.square.name;
        didMove = currentPiece.didmove;
        //Debug.Log("this is square"+GetComponent<UnityChessPiece>().square.name);
        var currentPos = new Position(currentSquare[0] - 'A', currentSquare[1] - '1');
        //Debug.Log($"this is position: {currentPos.row},{currentPos.column}");
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var localSquare = sq[i, j].GetComponent<ChessSquare>();
                var localFigure = localSquare.figure;
                if (localFigure != null)
                {

                    var color = ChessEnum.Color.Black;
                    ////Debug.Log($"name is {localFigure.name}");
                    if (localFigure.name.Contains("White"))
                    {
                        color = ChessEnum.Color.White;
                    }
                    board[i, j] = new ChessPiece(localFigure.figure, color, localFigure.didmove, localSquare.enPassantPossible);
                    // //Debug.Log($"({i},{j})={board[i, j].figure},{color}");
                }
                else
                {
                    board[i, j] = null;
                    ////Debug.Log($"({i},{j})=null");
                }
            }
        }
        //Debug.Log($"{currentPos.column},{currentPos.row}, {board[currentPos.column, currentPos.row]?.figure}");
        switch (figure)
        {
            case Figure.Pawn:
                HighlightSquares(PossibleMovesPawn(new ChessBoard(board), currentPos, didMove), sq); break;
            case Figure.King:
                HighlightSquares(PossibleMovesKing(new ChessBoard(board), currentPos, didMove), sq); break;
            case Figure.Queen:
                HighlightSquares(PossibleMovesQueen(new ChessBoard(board), currentPos, didMove), sq); break;
            case Figure.Rook:
                HighlightSquares(PossibleMovesRook(new ChessBoard(board), currentPos, didMove), sq); break;
            case Figure.Bishop:
                HighlightSquares(PossibleMovesBishop(new ChessBoard(board), currentPos, didMove), sq); break;
            case Figure.Knight:
                HighlightSquares(PossibleMovesKnight(new ChessBoard(board), currentPos, didMove), sq); break;

        }
    }

    /// <summary>
    /// Highlight in game sqares at which piece can move 
    /// </summary>
    /// <param name="positions"></param>
    /// <param name="board"></param>
    private void HighlightSquares(Position[] positions, GameObject[,] board)
    {
        //Debug.Log($"Highlighting {positions.Length}");
        if (positions.Length == 0)
        {
            Debug.Log("No available moves!");
        }

        //Debug.Log($"found {positions.Length} moves");
        foreach (var position in positions)
        {
            //Debug.Log("one");
            //Debug.Log(board[position.column, position.row].name);
            // board[position.column, position.row].GetComponent<MeshRenderer>().material.color = activeColor;
            //Debug.Log("HighLighted");
            var sq = board[position.column, position.row].GetComponent<ChessSquare>();
            sq.availableMove = true;
            if (position.enPassantBeat)
            {
                Debug.Log($"Found it!!!!!!!! {sq.name}");
                sq.enPassantBeat = true;
            }
            sq.enPassantOnStep = position.enPassantPossible;
            sq.castling = position.castling;
        }
    }
    /// <summary>
    /// Add moves possible for Pawn to list 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <param name="didMove"></param>
    /// <returns> List of possible moves </returns>
    private static Position[] PossibleMovesPawn(ChessBoard board, Position pos, bool didMove)
    {
        //Debug.Log($"current position -{pos.column},{pos.row}");
        ChessPiece piece = board.GetPieceFromPosition(pos);
        List<Position> listPossibleMoves;
        if (piece.color == ChessEnum.Color.White)
            listPossibleMoves = ExploreWhitePawn(board, pos);
        else
            listPossibleMoves = ExploreBlackPawn(board, pos);

        listPossibleMoves = DeleteMoveWhenCheckExistAfterMove(board, pos, listPossibleMoves);

        // en passant

        if (piece?.color == ChessEnum.Color.White)
            return listPossibleMoves.Concat(EnPassantWhite(board, pos)).ToArray();
        else
            return listPossibleMoves.Concat(EnPassantBlack(board, pos)).ToArray();


    }
    /// <summary>
    /// Add moves possible for King to list, and removes moves that lead chceck on own king 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <param name="didMove"></param>
    /// <returns> List of possible moves </returns>
    private static Position[] PossibleMovesKing(ChessBoard board, Position pos, bool didMove)
    {
        List<Position> listPossiblePos;

        listPossiblePos = ExploreKing(board, pos);

        listPossiblePos = DeleteMoveWhenCheckExistAfterMove(board, pos, listPossiblePos);

        // castling
        return listPossiblePos.Concat(Castling(board, pos, didMove)).ToArray();


    }
    /// <summary>
    ///  Add moves possible for Queen to list, and removes moves that lead chceck on own king 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <param name="didMove"></param>
    ///<returns> List of possible moves </returns>
    private static Position[] PossibleMovesQueen(ChessBoard board, Position pos, bool didMove)
    {
        List<Position> listPossiblePos;

        listPossiblePos = ExploreQueen(board, pos);

        listPossiblePos = DeleteMoveWhenCheckExistAfterMove(board, pos, listPossiblePos);

        return listPossiblePos.ToArray();
    }
    /// <summary>
    ///  Add moves possible for Rook to list, and removes moves that lead chceck on own king  
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <param name="didMove"></param>
    ///<returns> List of possible moves </returns>
    private static Position[] PossibleMovesRook(ChessBoard board, Position pos, bool didMove)
    {
        List<Position> listPossiblePos;

        listPossiblePos = ExploreRook(board, pos);

        listPossiblePos = DeleteMoveWhenCheckExistAfterMove(board, pos, listPossiblePos);

        return listPossiblePos.ToArray();
    }
    /// <summary>
    ///  Add moves possible for Bishop to list, and removes moves that lead chceck on own king  
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <param name="didMove"></param>
    ///<returns> List of possible moves </returns>
    private static Position[] PossibleMovesBishop(ChessBoard board, Position pos, bool didMove)
    {
        List<Position> listPossiblePos;

        listPossiblePos = ExploreBishop(board, pos);

        listPossiblePos = DeleteMoveWhenCheckExistAfterMove(board, pos, listPossiblePos);


        return listPossiblePos.ToArray();
    }
    /// <summary>
    ///  Add moves possible for Knight to list, and removes moves that lead chceck on own king  
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <param name="didMove"></param>
    ///<returns> List of possible moves </returns>
    private static Position[] PossibleMovesKnight(ChessBoard board, Position pos, bool didMove)
    {
        List<Position> listPossiblePos;

        listPossiblePos = ExploreKnight(board, pos);

        listPossiblePos = DeleteMoveWhenCheckExistAfterMove(board, pos, listPossiblePos);

        return listPossiblePos.ToArray();
    }
    /// <summary>
    /// Define direction, check for squares on which move or beat is possible for white pawn 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <returns>list of posiblle moves </returns>
    private static List<Position> ExploreWhitePawn(ChessBoard board, Position pos)
    {
        List<Position> listpossibleMoves = new List<Position>();
        ChessPiece piece = board.GetPieceFromPosition(pos);


        var possiblePos = new Dictionary<string, Position>
        {
            ["normal"] = new Position(pos.row + 1, pos.column),
            ["start"] = new Position(pos.row + 2, pos.column, true),
            ["northWest"] = new Position(pos.row + 1, pos.column - 1),
            ["northEast"] = new Position(pos.row + 1, pos.column + 1),

        };

        //normal position
        if (possiblePos["normal"].IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos["normal"]))
            listpossibleMoves.Add(possiblePos["normal"]);

        //starting position
        if (pos.row == 1 && board.IsSquareEmpty(possiblePos["normal"]) && board.IsSquareEmpty(possiblePos["start"]))
            listpossibleMoves.Add(possiblePos["start"]);

        // captured 
        if (possiblePos["northWest"].IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(possiblePos["northWest"]))
            listpossibleMoves.Add(possiblePos["northWest"]);
        if (possiblePos["northEast"].IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(possiblePos["northEast"]))
            listpossibleMoves.Add(possiblePos["northEast"]);

        return listpossibleMoves;
    }
    /// <summary>
    /// Define direction, check for squares on which move or beat is possible for blac pawn 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <returns>list of posiblle moves </returns>
    private static List<Position> ExploreBlackPawn(ChessBoard board, Position pos)
    {
        List<Position> listpossibleMoves = new List<Position>();

        var possiblePos = new Dictionary<string, Position>
        {
            ["normal"] = new Position(pos.row - 1, pos.column),
            ["start"] = new Position(pos.row - 2, pos.column, true),
            ["northWest"] = new Position(pos.row - 1, pos.column - 1),
            ["northEast"] = new Position(pos.row - 1, pos.column + 1),

        };
        //normal position
        if (possiblePos["normal"].IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos["normal"]))
            listpossibleMoves.Add(possiblePos["normal"]);

        //starting position
        if (pos.row == 6 && board.IsSquareEmpty(possiblePos["normal"]) && board.IsSquareEmpty(possiblePos["start"]))
            listpossibleMoves.Add(possiblePos["start"]);

        // captured 
        if (possiblePos["northWest"].IsPositionOnTheBoard() && board.IsWhitePieceOnSquare(possiblePos["northWest"]))
            listpossibleMoves.Add(possiblePos["northWest"]);
        if (possiblePos["northEast"].IsPositionOnTheBoard() && board.IsWhitePieceOnSquare(possiblePos["northEast"]))
            listpossibleMoves.Add(possiblePos["northEast"]);

        return listpossibleMoves;
    }
    /// <summary>
    /// Define direction, check for squares on which move or beat is possible for King
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <returns>list of posiblle moves </returns>
    private static List<Position> ExploreKing(ChessBoard board, Position pos)
    {
        List<Position> listPossibleMoves = new List<Position>();
        ChessPiece piece = board.GetPieceFromPosition(pos);

        Position[] possiblePos =
        {
                new Position(pos.row + 1, pos.column),
                new Position(pos.row - 1, pos.column),
                new Position(pos.row, pos.column - 1),
                new Position(pos.row, pos.column + 1),
                new Position(pos.row + 1, pos.column-1),
                new Position(pos.row - 1, pos.column-1),
                new Position(pos.row+1, pos.column + 1),
                new Position(pos.row-1, pos.column + 1),

            };

        foreach (Position p in possiblePos)
        {
            if (p.IsPositionOnTheBoard() && (board.IsSquareEmpty(p) || board.IsBlackPieceOnSquare(p) != board.IsBlackPieceOnSquare(pos)))
            {
                listPossibleMoves.Add(p);
            }
        }

        return listPossibleMoves;
    }
    /// <summary>
    /// Define direction, check for squares on which move or beat is possible for Queen 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <returns>list of posiblle moves </returns>
    private static List<Position> ExploreQueen(ChessBoard board, Position pos)
    {
        List<Position> listPossibleMoves = new List<Position>();

        listPossibleMoves = ExploreBishop(board, pos);
        return listPossibleMoves.Concat(ExploreRook(board, pos)).ToList();

    }
    /// <summary>
    /// Define direction, check for squares on which move or beat is possible for Rook
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <returns>list of posiblle moves </returns>
    private static List<Position> ExploreRook(ChessBoard board, Position pos)
    {
        List<Position> listPossibleMoves = new List<Position>();
        Position possiblePos;

        // North
        possiblePos = new Position(pos.row + 1, pos.column);
        while (possiblePos.IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos))
        {
            listPossibleMoves.Add(possiblePos);
            possiblePos = new Position(possiblePos.row + 1, possiblePos.column);
        }
        if (possiblePos.IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(pos) != board.IsBlackPieceOnSquare(possiblePos))
            listPossibleMoves.Add(possiblePos);

        // South
        possiblePos = new Position(pos.row - 1, pos.column);
        while (possiblePos.IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos))
        {
            listPossibleMoves.Add(possiblePos);
            possiblePos = new Position(possiblePos.row - 1, possiblePos.column);
        }
        if (possiblePos.IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(pos) != board.IsBlackPieceOnSquare(possiblePos))
            listPossibleMoves.Add(possiblePos);

        // West
        possiblePos = new Position(pos.row, pos.column - 1);
        while (possiblePos.IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos))
        {
            listPossibleMoves.Add(possiblePos);
            possiblePos = new Position(possiblePos.row, possiblePos.column - 1);
        }
        if (possiblePos.IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(pos) != board.IsBlackPieceOnSquare(possiblePos))
            listPossibleMoves.Add(possiblePos);

        // South
        possiblePos = new Position(pos.row, pos.column + 1);
        while (possiblePos.IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos))
        {
            listPossibleMoves.Add(possiblePos);
            possiblePos = new Position(possiblePos.row, possiblePos.column + 1);
        }
        if (possiblePos.IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(pos) != board.IsBlackPieceOnSquare(possiblePos))
            listPossibleMoves.Add(possiblePos);

        return listPossibleMoves;
    }
    /// <summary>
    /// Define direction, check for squares on which move or beat is possible for Bishop
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <returns>list of posiblle moves </returns>
    private static List<Position> ExploreBishop(ChessBoard board, Position pos)
    {
        List<Position> listPossibleMoves = new List<Position>();
        Position possiblePos;

        // NorthWest
        possiblePos = new Position(pos.row + 1, pos.column - 1);
        while (possiblePos.IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos))
        {
            listPossibleMoves.Add(possiblePos);
            possiblePos = new Position(possiblePos.row + 1, possiblePos.column - 1);
        }
        if (possiblePos.IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(pos) != board.IsBlackPieceOnSquare(possiblePos))
            listPossibleMoves.Add(possiblePos);

        // NorthEast
        possiblePos = new Position(pos.row + 1, pos.column + 1);
        while (possiblePos.IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos))
        {
            listPossibleMoves.Add(possiblePos);
            possiblePos = new Position(possiblePos.row + 1, possiblePos.column + 1);
        }
        if (possiblePos.IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(pos) != board.IsBlackPieceOnSquare(possiblePos))
            listPossibleMoves.Add(possiblePos);

        // SouthWest
        possiblePos = new Position(pos.row - 1, pos.column - 1);
        while (possiblePos.IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos))
        {
            listPossibleMoves.Add(possiblePos);
            possiblePos = new Position(possiblePos.row - 1, possiblePos.column - 1);
        }
        if (possiblePos.IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(pos) != board.IsBlackPieceOnSquare(possiblePos))
            listPossibleMoves.Add(possiblePos);

        // SouthEast
        possiblePos = new Position(pos.row - 1, pos.column + 1);
        while (possiblePos.IsPositionOnTheBoard() && board.IsSquareEmpty(possiblePos))
        {
            listPossibleMoves.Add(possiblePos);
            possiblePos = new Position(possiblePos.row - 1, possiblePos.column + 1);
        }
        if (possiblePos.IsPositionOnTheBoard() && board.IsBlackPieceOnSquare(pos) != board.IsBlackPieceOnSquare(possiblePos))
            listPossibleMoves.Add(possiblePos);

        return listPossibleMoves;
    }
    /// <summary>
    /// Define direction, check for squares on which move or beat is possible for Knight
    /// </summary>
    /// <param name="board"></param>
    /// <param name="pos"></param>
    /// <returns>list of posiblle moves </returns>
    private static List<Position> ExploreKnight(ChessBoard board, Position pos)
    {
        List<Position> listPossibleMoves = new List<Position>();

        Position[] possiblePos =
        {
                new Position(pos.row + 2, pos.column+1),
                new Position(pos.row + 2 , pos.column-1),
                new Position(pos.row - 2, pos.column+1),
                new Position(pos.row - 2 , pos.column-1),
                new Position(pos.row + 1, pos.column+2),
                new Position(pos.row + 1, pos.column-2),
                new Position(pos.row - 1, pos.column+2),
                new Position(pos.row - 1, pos.column-2),

            };

        foreach (Position p in possiblePos)
        {
            if (p.IsPositionOnTheBoard() && (board.IsSquareEmpty(p) || board.IsBlackPieceOnSquare(p) != board.IsBlackPieceOnSquare(pos)))
            {
                listPossibleMoves.Add(p);
            }
        }

        return listPossibleMoves;
    }
    /// <summary>
    /// Check if EnPassant move is possible for white pawn 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="capturingPiecePosition"></param>
    /// <returns>list of possible moves </returns>
    private static List<Position> EnPassantWhite(ChessBoard board, Position capturingPiecePosition)
    {
        List<Position> listPossibleMoves = new List<Position>();
        ChessPiece piece = board.GetPieceFromPosition(capturingPiecePosition);



        //Position capturedPiecePosition = new Position(capturingPiecePosition.row, capturingPiecePosition.column - 1);


        //Debug.Log($"Position: col{capturingPiecePosition.column}, row{capturingPiecePosition.row}");

        if (capturingPiecePosition.column < 7 && board[new Position(capturingPiecePosition.row, capturingPiecePosition.column + 1)]?.possibleEnPassant == true)
        {
            // Debug.Log("There is it");
            // Debug.Log($"{board[new Position(capturingPiecePosition.row, capturingPiecePosition.column + 1)].possibleEnPassant}");
            listPossibleMoves.Add(new Position(capturingPiecePosition.row + 1, capturingPiecePosition.column + 1, enPassantBeat: true));
        }
        if (capturingPiecePosition.column > 0 && board[new Position(capturingPiecePosition.row, capturingPiecePosition.column - 1)]?.possibleEnPassant == true)
        {
            //Debug.Log("There is it");
            // Debug.Log($"{board[new Position(capturingPiecePosition.row, capturingPiecePosition.column + 1)].possibleEnPassant}");
            listPossibleMoves.Add(new Position(capturingPiecePosition.row + 1, capturingPiecePosition.column - 1, enPassantBeat: true));
        }
        return listPossibleMoves;

    }
    /// <summary>
    /// Check if EnPassant move is possible for black pawn 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="capturingPiecePosition"></param>
    /// <returns>list of possible moves </returns>
    private static List<Position> EnPassantBlack(ChessBoard board, Position capturingPiecePosition)
    {
        List<Position> listPossibleMoves = new List<Position>();
        ChessPiece piece = board.GetPieceFromPosition(capturingPiecePosition);



        //Position capturedPiecePosition = new Position(capturingPiecePosition.row, capturingPiecePosition.column - 1);


        //Debug.Log($"Position: col{capturingPiecePosition.column}, row{capturingPiecePosition.row}");

        if (capturingPiecePosition.column > 0 && board[new Position(capturingPiecePosition.row, capturingPiecePosition.column - 1)]?.possibleEnPassant == true)
        {
            // Debug.Log("There is it");
            // Debug.Log($"{board[new Position(capturingPiecePosition.row, capturingPiecePosition.column + 1)].possibleEnPassant}");
            listPossibleMoves.Add(new Position(capturingPiecePosition.row - 1, capturingPiecePosition.column - 1, enPassantBeat: true));
        }
        if (capturingPiecePosition.column < 7 && board[new Position(capturingPiecePosition.row, capturingPiecePosition.column + 1)]?.possibleEnPassant == true)
        {
            //Debug.Log("There is it");
            // Debug.Log($"{board[new Position(capturingPiecePosition.row, capturingPiecePosition.column + 1)].possibleEnPassant}");
            listPossibleMoves.Add(new Position(capturingPiecePosition.row - 1, capturingPiecePosition.column + 1, enPassantBeat: true));
        }
        return listPossibleMoves;

    }
    /// <summary>
    /// Check if castling move is possible 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="kingPosition"></param>
    /// <param name="didMove"></param>
    /// <returns>list of possible moves </returns>
    private static List<Position> Castling(ChessBoard board, Position kingPosition, bool didMove)
    {
        List<Position> listPossibleMoves = new List<Position>();

        if (!didMove)
        {
            if (board[new Position(kingPosition.row, 0)]?.didmove == false && board[new Position(kingPosition.row, 1)] == null && board[new Position(kingPosition.row, 2)] == null)
            {
                var boardNew = board;
                boardNew[new Position(kingPosition.row, 1)] = boardNew[new Position(kingPosition.row, 3)];
                boardNew[new Position(kingPosition.row, 3)] = null;
                boardNew[new Position(kingPosition.row, 2)] = boardNew[new Position(kingPosition.row, 0)];
                boardNew[new Position(kingPosition.row, 0)] = null;

                if (!DoesCheckExist(boardNew, new Position(kingPosition.row, 1)))
                {
                    listPossibleMoves.Add(new Position(kingPosition.row, 1, castling: -1));
                }
            }
            if (board[new Position(kingPosition.row, 7)]?.didmove == false && board[new Position(kingPosition.row, 6)] == null && board[new Position(kingPosition.row, 5)] == null && board[new Position(kingPosition.row, 4)] == null)
            {
                var boardNew = board;
                boardNew[new Position(kingPosition.row, 5)] = boardNew[new Position(kingPosition.row, 3)];
                boardNew[new Position(kingPosition.row, 3)] = null;
                boardNew[new Position(kingPosition.row, 4)] = boardNew[new Position(kingPosition.row, 7)];
                boardNew[new Position(kingPosition.row, 7)] = null;

                if (!DoesCheckExist(boardNew, new Position(kingPosition.row, 5)))
                {
                    listPossibleMoves.Add(new Position(kingPosition.row, 5, castling: 1));
                }
            }
        }
        return listPossibleMoves;
    }
    /// <summary>
    ///  Check for and delete move which lead to self check - opening way to own king etc
    /// </summary>
    /// <param name="board"></param>
    /// <param name="capturingPiecePosition"></param>
    /// <returns>list of possible moves </returns>
    private static List<Position> DeleteMoveWhenCheckExistAfterMove(ChessBoard board, Position piecePosition, List<Position> possibleMoves)
    {
        ChessPiece piece = board.GetPieceFromPosition(piecePosition);

        List<Position> listpossibleMoves = new List<Position>();

        Position kingPosition = FindKing(board, piece.color);

        board[piecePosition] = null;

        foreach (Position pieceNewPosition in possibleMoves)
        {
            ChessPiece pieceOnChangedPositionBeforeChange;

            if (board.IsSquareEmpty(pieceNewPosition))
                pieceOnChangedPositionBeforeChange = null;
            else
                pieceOnChangedPositionBeforeChange = board.GetPieceFromPosition(pieceNewPosition);

            board.SetPieceOnPosition(pieceNewPosition, piece);

            if (piece.figure == Figure.King)
                kingPosition = FindKing(board, piece.color); // if the piece is king, king's possition is changed

            if (!DoesCheckExist(board, kingPosition))
                listpossibleMoves.Add(pieceNewPosition);

            board.SetPieceOnPosition(pieceNewPosition, pieceOnChangedPositionBeforeChange);
        }
        board.SetPieceOnPosition(piecePosition, piece);
        return listpossibleMoves;
    }
    /// <summary>
    /// Check if in situation given in input exists check 
    /// </summary>
    /// <param name="board"></param>
    /// <param name="kingPosition"></param>
    /// <returns>boolean state of check </returns>
    private static bool DoesCheckExist(ChessBoard board, Position kingPosition)
    {
        Position pos;
        List<Position> possibleMoves;

        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                pos = new Position(i, j);

                if (!board.IsSquareEmpty(pos) && board.GetPieceFromPosition(pos).color != board.GetPieceFromPosition(kingPosition).color)
                {
                    if (board.GetPieceFromPosition(pos).figure == Figure.Pawn && board.GetPieceFromPosition(pos).color == ChessEnum.Color.White)
                        possibleMoves = ExploreWhitePawn(board, pos);
                    else if (board.GetPieceFromPosition(pos).figure == Figure.Pawn)
                        possibleMoves = ExploreBlackPawn(board, pos);
                    else if (board.GetPieceFromPosition(pos).figure == Figure.Bishop)
                        possibleMoves = ExploreBishop(board, pos);
                    else if (board.GetPieceFromPosition(pos).figure == Figure.King)
                        possibleMoves = ExploreKing(board, pos);
                    else if (board.GetPieceFromPosition(pos).figure == Figure.Knight)
                        possibleMoves = ExploreKnight(board, pos);
                    else if (board.GetPieceFromPosition(pos).figure == Figure.Queen)
                        possibleMoves = ExploreQueen(board, pos);
                    else
                        possibleMoves = ExploreRook(board, pos);

                    foreach (Position p in possibleMoves)
                        if (p.Equals(kingPosition))
                            return true;
                }
            }
        }

        return false;
    }
    /// <summary>
    /// Localize and return poosition of king on board
    /// </summary>
    /// <param name="board"></param>
    /// <param name="kingColor"></param>
    /// <returns>position of king</returns>
    private static Position FindKing(ChessBoard board, ChessEnum.Color kingColor)
    {
        Position pos;
        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                pos = new Position(i, j);

                if (!board.IsSquareEmpty(pos) && board.GetPieceFromPosition(pos).figure == Figure.King && board.GetPieceFromPosition(pos).color == kingColor)
                    return pos;
            }
        }
        return null;
    }
    /// <summary>
    /// searches for check mate with current board state
    /// </summary>
    /// <returns>0 there is no checkmate, 1 black are under checkmate, 2 white are under checkmate, 3 a tie</returns>
    public int DoesCheckMateExist()
    {
        //converting actual chessboard to chessboard type:
        Debug.Log("Searching for CheckMate");
        ChessPiece[,] board = new ChessPiece[8, 8];
        var sq = unityBoard.gameObject.GetComponent<UnityChessBoard>().squares;
        var currentPiece = GetComponent<UnityChessPiece>();
        var currentSquare = currentPiece.square.name;
        didMove = currentPiece.didmove;
        var currentPos = new Position(currentSquare[0] - 'A', currentSquare[1] - '1');
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var localSquare = sq[i, j].GetComponent<ChessSquare>();
                var localFigure = localSquare.figure;
                if (localFigure != null)
                {

                    var color = ChessEnum.Color.Black;
                    ////Debug.Log($"name is {localFigure.name}");
                    if (localFigure.name.Contains("White"))
                    {
                        color = ChessEnum.Color.White;
                    }
                    board[i, j] = new ChessPiece(localFigure.figure, color, localFigure.didmove, localSquare.enPassantPossible);
                    // //Debug.Log($"({i},{j})={board[i, j].figure},{color}");
                }
                else
                {
                    board[i, j] = null;
                    ////Debug.Log($"({i},{j})=null");
                }
            }
        }

        ChessBoard chessBoard = new ChessBoard(board);

        //arranging array of pieces on board and corresponding positions
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Chess Piece");
        List<Position> positionsOfPieces = new List<Position>();
        UnityChessPiece[] figures = new UnityChessPiece[pieces.Length];
        for(int i = 0; i < pieces.Length; i++)
        {
            figures[i] = pieces[i].GetComponent<UnityChessPiece>();
            currentSquare = figures[i].square.name;
            positionsOfPieces.Add(new Position(currentSquare[0] - 'A', currentSquare[1] - '1'));
        }
        Position[] positions = positionsOfPieces.ToArray();
        List<Position> defMoves = new List<Position>();
        //1. is check
        //2. does any figure of checked color have movement possibility?
        //3. YES/NO -> ok, game persists/GG

        if (DoesCheckExist(chessBoard, FindKing(chessBoard, ChessEnum.Color.White))) //we;re looking for checkmate for white
        {
            for (int i = 0; i < figures.Length; i++)
            {
                UnityChessPiece piece = figures[i].GetComponent<UnityChessPiece>();
                if(piece.color == ChessEnum.Color.White)
                {
                    switch (figures[i].figure)
                    {
                        case Figure.Pawn:
                            defMoves = PossibleMovesPawn(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Knight:
                            defMoves = PossibleMovesKnight(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Bishop:
                            defMoves = PossibleMovesBishop(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Rook:
                            defMoves = PossibleMovesRook(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Queen:
                            defMoves = PossibleMovesQueen(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.King:
                            defMoves = PossibleMovesKing(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                    }
                    if (defMoves.Count > 0)
                    {
                        return 0;
                    }
                    defMoves.Clear();
                }

            }
            return 2; //white under checkmate

        }
        else if (DoesCheckExist(chessBoard, FindKing(chessBoard, ChessEnum.Color.Black))) //we;re looking for checkmate for black
        {
            for (int i = 0; i < figures.Length; i++)
            {
                if (figures[i].color == ChessEnum.Color.Black)
                {
                    switch (figures[i].figure)
                    {
                        case Figure.Pawn:
                            defMoves = PossibleMovesPawn(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Knight:
                            defMoves = PossibleMovesKnight(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Bishop:
                            defMoves = PossibleMovesBishop(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Rook:
                            defMoves = PossibleMovesRook(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Queen:
                            defMoves = PossibleMovesQueen(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.King:
                            defMoves = PossibleMovesKing(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                    }
                    if (defMoves.Count > 0)
                    {
                        return 0;
                    }
                    defMoves.Clear();
                }

            }
            return 1; //black under checkmate

        }
        else if (DoesTieExist())
        {
            return 3;
        }
        return 0;
        //Debug.Log("no checkmate found!");
    }
    /// <summary>
    /// searches for a tie on the basis of non-existing check and number of available moves
    /// </summary>
    /// <returns>true, when there is no check and one side has no possible moves,
    /// false in every other situation </returns>
    private bool DoesTieExist()
    {
        //converting actual chessboard to chessboard type:
        #region conversion
        Debug.Log("Searching for CheckMate");
        ChessPiece[,] board = new ChessPiece[8, 8];
        var sq = unityBoard.gameObject.GetComponent<UnityChessBoard>().squares;
        var currentPiece = GetComponent<UnityChessPiece>();
        var currentSquare = currentPiece.square.name;
        didMove = currentPiece.didmove;
        var currentPos = new Position(currentSquare[0] - 'A', currentSquare[1] - '1');
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var localSquare = sq[i, j].GetComponent<ChessSquare>();
                var localFigure = localSquare.figure;
                if (localFigure != null)
                {

                    var color = ChessEnum.Color.Black;
                    ////Debug.Log($"name is {localFigure.name}");
                    if (localFigure.name.Contains("White"))
                    {
                        color = ChessEnum.Color.White;
                    }
                    board[i, j] = new ChessPiece(localFigure.figure, color, localFigure.didmove, localSquare.enPassantPossible);
                    // //Debug.Log($"({i},{j})={board[i, j].figure},{color}");
                }
                else
                {
                    board[i, j] = null;
                    ////Debug.Log($"({i},{j})=null");
                }
            }
        }

        ChessBoard chessBoard = new ChessBoard(board);

        //arranging array of pieces on board and corresponding positions
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Chess Piece");
        List<Position> positionsOfPieces = new List<Position>();
        UnityChessPiece[] figures = new UnityChessPiece[pieces.Length];
        for (int i = 0; i < pieces.Length; i++)
        {
            figures[i] = pieces[i].GetComponent<UnityChessPiece>();
            currentSquare = figures[i].square.name;
            positionsOfPieces.Add(new Position(currentSquare[0] - 'A', currentSquare[1] - '1'));
        }
        Position[] positions = positionsOfPieces.ToArray();
        List<Position> defMoves = new List<Position>();
        #endregion

        if (!DoesCheckExist(chessBoard, FindKing(chessBoard, ChessEnum.Color.White)))
        {
            for (int i = 0; i < figures.Length; i++)
            {
                UnityChessPiece piece = figures[i].GetComponent<UnityChessPiece>();
                if (piece.color == ChessEnum.Color.White)
                {
                    switch (figures[i].figure)
                    {
                        case Figure.Pawn:
                            defMoves = PossibleMovesPawn(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Knight:
                            defMoves = PossibleMovesKnight(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Bishop:
                            defMoves = PossibleMovesBishop(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Rook:
                            defMoves = PossibleMovesRook(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Queen:
                            defMoves = PossibleMovesQueen(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.King:
                            defMoves = PossibleMovesKing(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                    }
                    if (defMoves.Count > 0)
                    {
                        return false;
                    }
                    defMoves.Clear();
                }

            }
            Debug.Log("white cannot move");
            return true; //a tie, no check and white cannot move

        }
        else if (!DoesCheckExist(chessBoard, FindKing(chessBoard, ChessEnum.Color.Black)))
        {
            for (int i = 0; i < figures.Length; i++)
            {
                if (figures[i].color == ChessEnum.Color.Black)
                {
                    switch (figures[i].figure)
                    {
                        case Figure.Pawn:
                            defMoves = PossibleMovesPawn(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Knight:
                            defMoves = PossibleMovesKnight(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Bishop:
                            defMoves = PossibleMovesBishop(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Rook:
                            defMoves = PossibleMovesRook(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.Queen:
                            defMoves = PossibleMovesQueen(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                        case Figure.King:
                            defMoves = PossibleMovesKing(chessBoard, positions[i], didMove).ToList<Position>();
                            break;
                    }
                    if (defMoves.Count > 0)
                    {
                        return false;
                    }
                    defMoves.Clear();
                }

            }

            Debug.Log("black cannot move");
            return true; //a tie, (no check and black cant move)
        }
        else
        {
            return false; //will never be reached
        }

    }


}



