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
    // Start is called before the first frame update
    void Start()
    {
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
        if (hovered)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            ////Debug.Log(collision.gameObject.name);
            if (availableMove)
            {
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
