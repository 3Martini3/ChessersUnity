using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSquare : MonoBehaviour
{

    public ChessPiece figure = null;
    public Material hoverMaterial;
    private Material originalMaterial { get; set; }
    private bool hovered = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(hovered)
        {
            GetComponent<MeshRenderer>().material = originalMaterial;
            Debug.Log(collision.gameObject.name);
            figure = collision.gameObject.GetComponent<ChessPiece>();
            figure.pinToPosition(GetComponent<MeshRenderer>().bounds.center);
            hovered = false;
        }
       
    }
}
