using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {

    public MeshRenderer square;
    public Material hoverMaterial;
    private float yPosition { get; set; }
    private Material originalMaterial { get; set; }
    private Color defaultColor { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        yPosition = transform.position.y;
        transform.position = new Vector3(square.bounds.center.x, yPosition, square.bounds.center.z);
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

    // Update is called once per frame
    /* void Update()
     {

     }*/
}
