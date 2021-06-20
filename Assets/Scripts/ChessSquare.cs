using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class containing script that controll what happens on chess square
/// Most of game control (things on board happens on square, not with pieces)
/// </summary>
public class ChessSquare : MonoBehaviour
{

    public UnityChessPiece figure = null;
    public Material hoverMaterial;
    public bool availableMove;
    private bool hovered;
    public Vector3 center;
    public bool enPassantOnStep;
    public bool enPassantPossible;
    public int castling;
    public bool enPassantBeat;
    /// <summary>
    /// 0-none
    /// 1-white
    /// 2-black
    /// </summary>
    public int isTransform;
    public Turns turn;

    // Start is called before the first frame update
    /// <summary>
    /// Centers pieces on squares
    /// </summary>
    void Start()
    {
        castling = 0;
        center = GetComponent<MeshRenderer>().bounds.center;
        hovered = false;
    }
    /// <summary>
    /// Colors fields with avaible moves
    /// </summary>
    private void Update()
    {
        if(availableMove)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    /// <summary>
    ///  marks element as hovered using trigger entry
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(hoverMaterial!=null)
        {
            //Debug.Log($"trigger {name}");
          //  GetComponent<MeshRenderer>().material.color = Color.blue;
            hovered = true;
        }
    }

    /// <summary>
    /// unmarks element as hovered using trigger entry
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {  
        //GetComponent<MeshRenderer>().material.color=Color.white;
        hovered = false;
    }
    
  
    /// <summary>
    /// Check and manages all mechanics that happen on field: 
    /// Controls turn by checking if figure moved 
    /// Check if figure should be changed (pawn promotion)
    /// Places figures on squares
    /// Manages Castling an En passant beating 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Enter");
        if (hovered)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            var collisionFigure = collision.gameObject.GetComponent<UnityChessPiece>();
            if (availableMove && (figure==null||figure.color != collisionFigure.color))
            {

                turn.SwapTurn();
                if((isTransform==1 || isTransform ==2) 
                    && collisionFigure.figure==ChessEnum.Figure.Pawn)
                {

                    GameObject promotionMenu = GameObject.FindGameObjectWithTag("PromotionRadialMenu");
                    //collisionFigure.tag = "Promoted";
                    promotionMenu.GetComponent<PromotionMenuHandler>().collisionFigure = collisionFigure;
                    //Debug.Log(collisionFigure.tag);
                    promotionMenu.GetComponent<Canvas>().enabled = (!promotionMenu.GetComponent<Canvas>().enabled); //turn menu on
                }

                
                if (figure == null)
                {
                    if (this.castling == 1)
                    {
                        if (collision.gameObject.name.Contains("White"))
                        {
                            var squares = GameObject.FindGameObjectWithTag("Chess Board").gameObject.GetComponent<UnityChessBoard>().squares;
                            var sqInit = squares[7, 0].GetComponent<ChessSquare>();
                            var f = sqInit.figure;
                            sqInit.figure = null;
                            var sq = squares[4, 0].GetComponent<ChessSquare>();
                            sq.figure = f;
                            f.square = sq;
                            f.pinToPosition(sq.center);
                        }
                        else
                        {
                            var squares = GameObject.FindGameObjectWithTag("Chess Board").gameObject.GetComponent<UnityChessBoard>().squares;
                            var sqInit = squares[7, 7].GetComponent<ChessSquare>();
                            var f = sqInit.figure;
                            sqInit.figure = null;
                            var sq = squares[4, 7].GetComponent<ChessSquare>();
                            sq.figure = f;
                            f.square = sq;
                            f.pinToPosition(sq.center);
                            this.castling = 0;
                        }

                    }
                    else if (this.castling == -1)
                    {
                        if (collision.gameObject.name.Contains("White"))
                        {
                            var squares = GameObject.FindGameObjectWithTag("Chess Board").gameObject.GetComponent<UnityChessBoard>().squares;
                            var sqInit = squares[0, 0].GetComponent<ChessSquare>();
                            var f = sqInit.figure;
                            sqInit.figure = null;
                            var sq = squares[2, 0].GetComponent<ChessSquare>();
                            sq.figure = f;
                            f.square = sq;
                            f.pinToPosition(sq.center);
                        }
                        else
                        {
                            var squares = GameObject.FindGameObjectWithTag("Chess Board").gameObject.GetComponent<UnityChessBoard>().squares;
                            var sqInit = squares[0, 7].GetComponent<ChessSquare>();
                            var f = sqInit.figure;
                            sqInit.figure = null;
                            var sq = squares[2, 7].GetComponent<ChessSquare>();
                            sq.figure = f;
                            f.square = sq;
                            f.pinToPosition(sq.center);
                            this.castling = 0;
                        }
                    }

                    if(enPassantBeat)
                    {
                        if(collisionFigure.figure == ChessEnum.Figure.Pawn)
                        {
                            if (collisionFigure.color == ChessEnum.Color.White)
                            {
                                var sq = GameObject.Find(Convert.ToChar(name[0] - 1) + name[1].ToString()).GetComponent<ChessSquare>();
                                sq.figure.beat();
                                sq.figure = null;
                                enPassantBeat = false;
                            }
                            else
                            {

                                var sq = GameObject.Find(Convert.ToChar(name[0] + 1) + name[1].ToString()).GetComponent<ChessSquare>();
                                sq.figure.beat();
                                sq.figure = null;
                                enPassantBeat = false;
                            }
                        }
                        enPassantBeat = false;
                    }

                }else
                {
                    figure.beat();
                }


                figure = collisionFigure;
                if(figure.square.name!=this.name)
                {
                    var squares = GameObject.FindGameObjectsWithTag("Chess Square");
                    foreach (var square in squares)
                    {
                        if (square.name != this.name)
                        {
                            var sq = square.GetComponent<ChessSquare>();
                            sq.enPassantPossible = false;
                            //Debug.Log("DELETE1");
                            sq.availableMove = false;
                            sq.castling = 0;
                        }
                    }

                    figure.square.figure = null;
                    figure.square = this;
                    availableMove = false;
                    //Debug.Log("DELETE2");
                    if (enPassantOnStep)
                    {
                        enPassantPossible = true;
                        enPassantOnStep = false;
                       
                    }
                    if (!figure.didmove)
                    {
                        figure.didmove = true;
                    }
                }
                figure.pinToPosition(center);
                int checkMateType = collisionFigure.GetComponent<PossibleMoves>().DoesCheckMateExist();
                GameObject.FindGameObjectWithTag("CheckMateCanvas").GetComponent<CheckMateScreenHandler>().ShowEndScreen(checkMateType);
            }
            else
            {
                var obj = collision.gameObject.GetComponent<UnityChessPiece>();
                obj.pinToPosition(obj.square.GetComponent<MeshRenderer>().bounds.center);
            }
            hovered = false;
        
        var boardSquares = GameObject.FindGameObjectsWithTag("Chess Square");
        foreach (var square in boardSquares)
        {
                var sq = square.GetComponent<ChessSquare>();
               
                // sq.enPassantPossible = false;
                sq.availableMove = false;
                //Debug.Log("DELETE3");
                // sq.enPassantOnStep = false;
                sq.castling = 0;
                square.GetComponent<MeshRenderer>().material.color = Color.white;
        }
            //figure.goBackToSquare(); //GameObject.FindGameObjectWithTag("Chess Board").GetComponent<UnityChessBoard>().clearActive();
        }
    }
}
