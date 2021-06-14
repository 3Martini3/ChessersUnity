using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DragAndDrop : MonoBehaviour
{

    private bool dragging = false;
    private float distance { get; set; }
    private float defaultYPosition { get; set; }
    private BoxCollider boxCollider { get; set; }
    public float yChange;

    void Start()
    {
        defaultYPosition=transform.position.y;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    void OnMouseDown()
    {
        var chessPiece = GetComponent<UnityChessPiece>();
        if (!chessPiece.beaten&&GameObject.FindGameObjectWithTag("Turn Order").GetComponent<Turns>().turn == chessPiece.color)
        {
            GetComponent<UnityChessPiece>().activePiece.isPieceDragged = true;
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y + yChange, transform.position.z);
            dragging = true;
            boxCollider.enabled = true;
            GetComponent<PossibleMoves>().FindPossibleMoves();
        }
    }

    void OnMouseUp()
    {
        if(!GetComponent<UnityChessPiece>().beaten)
        {
            dragging = false;
            transform.position = new Vector3(transform.position.x, defaultYPosition, transform.position.z);
            boxCollider.enabled = false;
            GetComponent<UnityChessPiece>().activePiece.isPieceDragged = false;
        }
        
    }

    void Update()
    {
        if (dragging)
        {
            distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.y + yChange, transform.position.z), Camera.main.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, transform.position.y, rayPoint.z);
        }
    }
}
