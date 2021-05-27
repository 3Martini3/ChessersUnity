using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {

   // public MeshRenderer square;
    public Material hoverMaterial;
    public ChessSquare square;
    public string hoverSquareName;
    public float yPosition { get; set; }
    private Color defaultColor { get; set; }
    public Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        hoverSquareName = "";
        center = GetComponent<MeshRenderer>().bounds.center;
        square = FindClosestSquare().GetComponent<ChessSquare>();
        pinToPosition(square.GetComponent<MeshRenderer>().bounds.center);
        square.figure = this;
    }

    private void Update()
    {
        if(hoverSquareName != "")
        Debug.Log($"{hoverSquareName}, {yPosition},{transform.position.y}");
        if (yPosition==transform.position.y&&hoverSquareName!="")
        {
            Debug.Log("Hi");
            var hoverSquare = GameObject.Find(hoverSquareName).GetComponent<MeshRenderer>().bounds.center;
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(hoverSquare.x, hoverSquare.z)) > 0.5f)
            {
                goBackToSquare();
            }
            hoverSquareName = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Coool");
       if(other.tag=="Chess Square")
        {
            hoverSquareName = other.name;
        }
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
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
        center = GetComponent<MeshRenderer>().bounds.center;
    }

    public void goBackToSquare()
    {
        pinToPosition(square.center);
    }
}
