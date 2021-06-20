using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChessEnum;
/// <summary>
///  Control,store piece and piece data
/// </summary>
public class UnityChessPiece : MonoBehaviour
{

    // public MeshRenderer square;
    public Material hoverMaterial;
    public ChessSquare square;
    public ChessEnum.Color color;
    public UnityEngine.Color hoverColor;
    public string hoverSquareName;
    public Figure figure;
    public float yPosition;
    private ChessEnum.Color defaultColor { get; set; }
    public Vector3 center;
    public bool didmove;
    public ActivePiece activePiece;
    public bool beaten;
    public bool checkPositionAvailable;

    public GameObject pauseMenu;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    /// <summary>
    /// Initialize basic piece values, center them
    /// </summary>
    void Start()
    {
        hoverSquareName = "";
        center = GetComponent<MeshRenderer>().bounds.center;
        square = FindClosestSquare().GetComponent<ChessSquare>();
        pinToPosition(square.GetComponent<MeshRenderer>().bounds.center);
        square.figure = this;

        pauseMenu = GameObject.Find("PauseMenu");
        settingsMenu = GameObject.Find("Settings");
    }
    /// <summary>
    /// private
    /// </summary>
    private void Update()
    {

        pauseMenu = GameObject.Find("PauseMenu");
        settingsMenu = GameObject.Find("Settings");

        if (checkPositionAvailable&&!GetComponent<DragAndDrop>().dragging)
        {
            if (hoverSquareName != "")
            {
                ////Debug.Log($"{hoverSquareName}, {yPosition},{transform.position.y}");
                if (yPosition == transform.position.y && hoverSquareName != "")
                {
                    if (hoverSquareName == "Border")
                    {
                       
                        goBackToSquare();
                        Debug.Log("Back border2");
                        var boardSquares = GameObject.FindGameObjectsWithTag("Chess Square");
                        foreach (var square in boardSquares)
                        {
                            var sq = square.GetComponent<ChessSquare>();

                            // sq.enPassantPossible = false;
                            sq.availableMove = false;
                            Debug.Log("DELETE4");
                            // sq.enPassantOnStep = false;
                            sq.castling = 0;
                            square.GetComponent<MeshRenderer>().material.color = UnityEngine.Color.white;
                            
                            
                        }
                        hoverSquareName = "";
                        checkPositionAvailable = false;
                    }
                    else
                    {
                        // //Debug.Log("Hi");
                        var hoverSquare = GameObject.Find(hoverSquareName).GetComponent<MeshRenderer>().bounds.center;
                        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(hoverSquare.x, hoverSquare.z)) > 0.5f)
                        {
                            Debug.Log("Go back! no position");
                            goBackToSquare();
                            Debug.Log("Back3");
                            var boardSquares = GameObject.FindGameObjectsWithTag("Chess Square");
                            foreach (var square in boardSquares)
                            {
                                var sq = square.GetComponent<ChessSquare>();

                                // sq.enPassantPossible = false;
                                Debug.Log("DELETE5");
                                sq.availableMove = false;
                                // sq.enPassantOnStep = false;
                                sq.castling = 0;
                                square.GetComponent<MeshRenderer>().material.color = UnityEngine.Color.white;
                            }
                        }
                        hoverSquareName = "";
                        checkPositionAvailable = false;
                    }
                }
            } else
            {
                goBackToSquare();
                Debug.Log("Back2");
                checkPositionAvailable = false;
            }
        }
    }
    /// <summary>
    /// private
    /// </summary>
    /// <param name="other"></param>

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chess Square")
        {
            hoverSquareName = other.name;
        }
    }
    /// <summary>
    /// private
    /// </summary>
    private void OnMouseOver()
    {
        if (settingsMenu != null || pauseMenu != null) return;
        if (!beaten && GameObject.FindGameObjectWithTag("Turn Order").GetComponent<Turns>().turn == this.color)
        {
            if (!activePiece.isPieceDragged)
            {
                GetComponent<Renderer>().material.color = hoverColor;
            }
        }

    }
    /// <summary>
    /// private
    /// </summary>
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = UnityEngine.Color.white;
    }
    /// <summary>
    /// private
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// set piece on position 
    /// </summary>
    /// <param name="position3"></param>
    public void pinToPosition(Vector3 position3)
    {
        yPosition = transform.position.y;
        transform.position = new Vector3(position3.x/*square.bounds.center.x*/, yPosition, /*square.bounds.center.z*/position3.z);
        center = GetComponent<MeshRenderer>().bounds.center;

    }
    /// <summary>
    /// set piece back to square from which it wanted to move
    /// </summary>
    public void goBackToSquare()
    {
         var boardSquares = GameObject.FindGameObjectsWithTag("Chess Square");
         foreach (var square in boardSquares)
         {
             var sq = square.GetComponent<ChessSquare>();
             // sq.enPassantPossible = false;
             sq.availableMove = false;
             Debug.Log("DELETE5");
             // sq.enPassantOnStep = false;
             sq.castling = 0;
             square.GetComponent<MeshRenderer>().material.color = UnityEngine.Color.white;
         }
        Debug.Log("GO BACK!");
        pinToPosition(square.center);
        


    }
    /// <summary>
    /// transform pawn, changes  data and mesh
    /// </summary>
    /// <param name="newFigure"></param>
    public void tranformPawn(ChessEnum.Figure newFigure)
    {
        figure = newFigure;
        GetComponent<PossibleMoves>().figure = newFigure;
        var anyOtherFigure = GameObject.Find(newFigure.ToString() + " " + color.ToString());
        var pos = anyOtherFigure.transform;
        GetComponent<MeshFilter>().mesh = anyOtherFigure.GetComponent<MeshFilter>().mesh;
        GetComponent<MeshCollider>().sharedMesh = anyOtherFigure.GetComponent<MeshCollider>().sharedMesh;
        GetComponent<Renderer>().material = anyOtherFigure.GetComponent<Renderer>().material;
        name = name.Replace("Pawn", newFigure.ToString());
        transform.position = new Vector3(transform.position.x, pos.position.y, transform.position.z);
        transform.rotation = pos.rotation;
        transform.localScale = pos.localScale;

    }
    /// <summary>
    /// manages beating, send piece status to beaten 
    /// </summary>
    public void beat()
    {
        var beatenStack = GameObject.Find($"Beaten Pieces {color.ToString()}");
        var stacks = beatenStack.GetComponent<Beat>().stacks;
        for (int i = 0; i < 15; i++)
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
        //Debug.Log("Beat boi!");
    }
}
