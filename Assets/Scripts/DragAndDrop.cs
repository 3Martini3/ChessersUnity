using UnityEngine;
/// <summary>
/// Control piece movement, piece draging and mouse-piece interaction 
/// </summary>
class DragAndDrop : MonoBehaviour
{

    public bool dragging;
    private float distance { get; set; }
    private float defaultYPosition { get; set; }
    private BoxCollider boxCollider { get; set; }
    public float yChange;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    /// <summary>
    /// Set basic values, connect hidden menus
    /// </summary>
    void Start()
    {
        defaultYPosition = transform.position.y;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        pauseMenu = GameObject.Find("PauseMenu");
        settingsMenu = GameObject.Find("Settings");
    }

    /// <summary>
    /// start piece holding on click, contol movement, after checking if no menus are enabled
    /// </summary>
    void OnMouseDown()
    {
        if (settingsMenu != null || pauseMenu != null
            || GameObject.FindGameObjectWithTag("PromotionRadialMenu").GetComponent<Canvas>().enabled)
        {
            return;
        }
        var chessPiece = GetComponent<UnityChessPiece>();
        if (!chessPiece.beaten && GameObject.FindGameObjectWithTag("Turn Order").GetComponent<Turns>().turn == chessPiece.color)
        {

            var piece = GetComponent<UnityChessPiece>();
            piece.activePiece.isPieceDragged = true;
            piece.checkPositionAvailable = true;
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y + yChange, transform.position.z);
            dragging = true;
            boxCollider.enabled = true;

           

            GetComponent<PossibleMoves>().FindPossibleMoves();
            //Debug.Log("Drag");
        }
    }

    /// <summary>
    /// stops dragging, disables piece movement 
    /// </summary>
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
    /// <summary>
    /// Uptades per frame, control piece movement mid dragging
    /// </summary>
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
