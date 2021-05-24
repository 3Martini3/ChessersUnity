using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {

   // public MeshRenderer square;
    public Material hoverMaterial;
    public ChessSquare square { get; set; }
    private float yPosition { get; set; }
    private Material originalMaterial { get; set; }
    private Color defaultColor { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;

        square = FindClosestSquare().GetComponent<ChessSquare>();
        pinToPosition(square.GetComponent<MeshRenderer>().bounds.center);
        square.figure = this;
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material = hoverMaterial;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }

    private GameObject FindClosestSquare()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Chess Square");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = GetComponent<MeshRenderer>().bounds.center;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.GetComponent<MeshRenderer>().bounds.center - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }



    public void pinToPosition(Vector3 position3)
    {
        yPosition = transform.position.y;
        transform.position = new Vector3(position3.x/*square.bounds.center.x*/, yPosition, /*square.bounds.center.z*/position3.z);
    }

    
}
