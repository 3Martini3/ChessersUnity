using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {

   // public MeshRenderer square;
    public Material hoverMaterial;
    private float yPosition { get; set; }
    private Material originalMaterial { get; set; }
    private Color defaultColor { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }


    private void OnMouseOver()
    {
        GetComponent<Renderer>().material = hoverMaterial;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }

    public void pinToPosition(Vector3 position3)
    {
        yPosition = transform.position.y;
        transform.position = new Vector3(position3.x/*square.bounds.center.x*/, yPosition, /*square.bounds.center.z*/position3.z);
    }

    // Update is called once per frame
    /* void Update()
     {

     }*/
}
