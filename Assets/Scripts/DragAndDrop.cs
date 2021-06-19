using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DragAndDrop : MonoBehaviour
{

    public bool dragging;
    private float distance { get; set; }
    private float defaultYPosition { get; set; }
    private BoxCollider boxCollider { get; set; }
    public float yChange;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    void Start()
    {
        defaultYPosition=transform.position.y;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        pauseMenu = GameObject.Find("PauseMenu");
        settingsMenu = GameObject.Find("Settings");
    }

    void OnMouseDown()
    {
        if (settingsMenu != null || pauseMenu != null) return;
        var chessPiece = GetComponent<UnityChessPiece>();
        if (!chessPiece.beaten&&GameObject.FindGameObjectWithTag("Turn Order").GetComponent<Turns>().turn == chessPiece.color)
        {
           
            var piece = GetComponent<UnityChessPiece>();
            piece.activePiece.isPieceDragged = true;
            piece.checkPositionAvailable = true ;
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y + yChange, transform.position.z);
            dragging = true;
            boxCollider.enabled = true;

            int checkMateType = GetComponent<PossibleMoves>().DoesCheckMateExist();
            GameObject.FindGameObjectWithTag("CheckMateCanvas").GetComponent<CheckMateScreenHandler>().ShowEndScreen(checkMateType);

            GetComponent<PossibleMoves>().FindPossibleMoves();
            //Debug.Log("Drag");
        }
    }

    void OnMouseUp()
    {
        if (!GetComponent<UnityChessPiece>().beaten)
        {
            dragging = false;
            transform.position = new Vector3(transform.position.x, defaultYPosition, transform.position.z);
            boxCollider.enabled = false;
            GetComponent<UnityChessPiece>().activePiece.isPieceDragged = false;
        }
        
    }

    void Update()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        settingsMenu = GameObject.Find("Settings");
        if (dragging)
        {
            distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.y + yChange, transform.position.z), Camera.main.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, transform.position.y, rayPoint.z);
        }
    }
}
