using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSquare : MonoBehaviour
{

    public UnityChessPiece figure = null;
    public Material hoverMaterial;
    public bool availableMove = false;
    private bool hovered;
    public Vector3 center;
    public bool enPassantOnStep;
    public bool enPassantPossible;
    public int castling;
    // Start is called before the first frame update
    void Start()
    {
        castling = 0;
        center = GetComponent<MeshRenderer>().bounds.center;
        hovered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(hoverMaterial!=null)
        {
            ////Debug.Log("trigger");
          //  GetComponent<MeshRenderer>().material.color = Color.blue;
            hovered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {  
        //GetComponent<MeshRenderer>().material.color=Color.white;
        hovered = false;
    }
    
  

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (hovered)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;

            if (availableMove && figure == null)
            {
                Debug.Log(this.castling);
                if(this.castling==1)
                {
                    if(collision.gameObject.name.Contains("White"))
                    {
                        var squares = GameObject.FindGameObjectWithTag("Chess Board").gameObject.GetComponent<UnityChessBoard>().squares;
                        var sqInit = squares[7, 0].GetComponent<ChessSquare>();
                        var f = sqInit.figure;
                        sqInit.figure = null;
                        var sq = squares[4, 0].GetComponent<ChessSquare>();
                        sq.figure = f;
                        f.square = sq;
                        f.pinToPosition(sq.center);
                    }else
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
                    
                }else if(this.castling==-1)
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
                

                figure = collision.gameObject.GetComponent<UnityChessPiece>();
                if(figure.square.name!=this.name)
                {
                    var squares = GameObject.FindGameObjectsWithTag("Chess Square");
                    foreach (var square in squares)
                    {
                        if (square.name != this.name)
                        {
                            var sq = square.GetComponent<ChessSquare>();
                            sq.enPassantPossible = false;
                            sq.availableMove = false;
                            sq.castling = 0;
                        }
                    }

                    figure.square.figure = null;
                    figure.square = this;
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
            }
            else
            {
                var obj = collision.gameObject.GetComponent<UnityChessPiece>();
                obj.pinToPosition(obj.square.GetComponent<MeshRenderer>().bounds.center);
            }
            hovered = false;
        }
        GameObject.FindGameObjectWithTag("Chess Board")?.GetComponent<UnityChessBoard>().clearActive();

    }
}
