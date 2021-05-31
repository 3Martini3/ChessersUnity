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
            //Debug.Log("trigger");
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
            //Debug.Log(collision.gameObject.name);
            if (availableMove)
            {
                figure = collision.gameObject.GetComponent<UnityChessPiece>();
                if(figure.square.name!=this.name)
                {
                    figure.square.figure = null;
                    figure.square = this;
                }
                figure.pinToPosition(center);
                
            }else
            {
                var obj = collision.gameObject.GetComponent<UnityChessPiece>();
                obj.pinToPosition(obj.square.GetComponent<MeshRenderer>().bounds.center);
            }
            hovered = false;
        }
        GameObject.FindGameObjectWithTag("Chess Board")?.GetComponent<UnityChessBoard>().clearActive();

    }
}
