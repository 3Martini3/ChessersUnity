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
    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        hovered = false;
        if (figure != null)
        {
            figure.pinToPosition(GetComponent<MeshRenderer>().bounds.center);
        }
    }




  

    private void OnTriggerEnter(Collider other)
    {
        if(hoverMaterial!=null)
        {
            Debug.Log("trigger");
            GetComponent<MeshRenderer>().material = hoverMaterial;
            hovered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {  
        GetComponent<MeshRenderer>().material = originalMaterial;
        hovered = false;
    }
    
  

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("AAAA");
        if(hovered)
        {
            Debug.Log("hovered");
            GetComponent<MeshRenderer>().material = originalMaterial;
            Debug.Log(collision.gameObject.name);
            if (availableMove)
            {
                figure = collision.gameObject.GetComponent<ChessPiece>();
                figure.square= this;
                figure.pinToPosition(GetComponent<MeshRenderer>().bounds.center);
            }else
            {
                var obj = collision.gameObject.GetComponent<ChessPiece>();
                obj.pinToPosition(obj.square.GetComponent<MeshRenderer>().bounds.center);
            }
            hovered = false;
        }
       
    }
}
