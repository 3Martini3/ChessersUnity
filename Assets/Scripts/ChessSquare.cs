using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSquare : MonoBehaviour
{

    public ChessPiece figure = null;
    public Material hoverMaterial;
    public bool availableMove = false;
    private Material originalMaterial { get; set; }
    private bool hovered;
    private bool setUp = false;
    // Start is called before the first frame update
    void Start()
    {

        originalMaterial = GetComponent<Renderer>().material;
        
        if (figure != null)
        {
            figure.pinToPosition(GetComponent<MeshRenderer>().bounds.center);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(hoverMaterial!=null)
        {
            GetComponent<MeshRenderer>().material = hoverMaterial;
            hovered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {  
        GetComponent<MeshRenderer>().material = originalMaterial;
        hovered = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (setUp == false)
        {
            setUp = true;
            figure = collision.gameObject.GetComponent<ChessPiece>();
            figure.squarePosition = GetComponent<MeshRenderer>().bounds.center;
            figure.pinToPosition(GetComponent<MeshRenderer>().bounds.center);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hovered)
        {
            GetComponent<MeshRenderer>().material = originalMaterial;
            Debug.Log(collision.gameObject.name);
            if (availableMove)
            {
                figure = collision.gameObject.GetComponent<ChessPiece>();
                figure.squarePosition = GetComponent<MeshRenderer>().bounds.center;
                figure.pinToPosition(GetComponent<MeshRenderer>().bounds.center);
            }else
            {
                var obj = collision.gameObject.GetComponent<ChessPiece>();
                obj.pinToPosition(obj.squarePosition);
            }
            hovered = false;
        }
       
    }
}
