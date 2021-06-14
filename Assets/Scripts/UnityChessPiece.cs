using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChessEnum;

public class UnityChessPiece : MonoBehaviour {

   // public MeshRenderer square;
    public Material hoverMaterial;
    public ChessSquare square;
    public ChessEnum.Color color;
    public string hoverSquareName;
    public Figure figure;
    public float yPosition;
    private ChessEnum.Color defaultColor { get; set; }
    public Vector3 center;
    public bool didmove;
    public ActivePiece activePiece;
    public bool beaten;
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
        if (hoverSquareName != "")
            ////Debug.Log($"{hoverSquareName}, {yPosition},{transform.position.y}");
            if (yPosition == transform.position.y && hoverSquareName != "")
            {
                if (hoverSquareName == "Border")
                {
                    goBackToSquare();
                    var boardSquares = GameObject.FindGameObjectsWithTag("Chess Square");
                    foreach (var square in boardSquares)
                    {
                        var sq = square.GetComponent<ChessSquare>();

                        // sq.enPassantPossible = false;
                        sq.availableMove = false;
                        // sq.enPassantOnStep = false;
                        sq.castling = 0;
                        square.GetComponent<MeshRenderer>().material.color = UnityEngine.Color.white;
                    }
                }
                else
                {
                    // //Debug.Log("Hi");
                    var hoverSquare = GameObject.Find(hoverSquareName).GetComponent<MeshRenderer>().bounds.center;
                    if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(hoverSquare.x, hoverSquare.z)) > 0.5f)
                    {
                        goBackToSquare();
                        var boardSquares = GameObject.FindGameObjectsWithTag("Chess Square");
                        foreach (var square in boardSquares)
                        {
                            var sq = square.GetComponent<ChessSquare>();

                            // sq.enPassantPossible = false;
                            sq.availableMove = false;
                            // sq.enPassantOnStep = false;
                            sq.castling = 0;
                            square.GetComponent<MeshRenderer>().material.color = UnityEngine.Color.white;
                        }
                    }
                    hoverSquareName = "";
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag=="Chess Square")
        {
            hoverSquareName = other.name;
        }
    }

    private void OnMouseOver()
    {
        if(!beaten&&GameObject.FindGameObjectWithTag("Turn Order").GetComponent<Turns>().turn==this.color)
        {
            if (!activePiece.isPieceDragged)
            {
                GetComponent<Renderer>().material.color = UnityEngine.Color.blue;
            }
        }
        
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = UnityEngine.Color.white;
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

    public void beat()
    {
        
        var beatenStack = GameObject.Find($"Beaten Pieces {color.ToString()}");
        var stacks = beatenStack.GetComponent<Beat>().stacks;
        for (int i=0;i<15;i++)
        {
            var stack = stacks[i];
            if (stack.isEmpty)
            {
                square = null;
                pinToPosition(stack.transform.position);
                stack.isEmpty = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.1111f, transform.position.z);
                tag = "Untagged";
                beaten = true;
                break;
            }
            
        }
        Debug.Log("Beat boi!");
    }
}
