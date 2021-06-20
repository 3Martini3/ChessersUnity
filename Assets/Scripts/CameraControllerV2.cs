using UnityEngine;
/// <summary>
/// Alows camera control, manages position and movement 
/// </summary>
public class CameraControllerV2 : MonoBehaviour
{
    public float moveSpeed;
    public float scrollSpeed;
    public float maxX;
    public float minX;
    public float maxZoomOut;
    public float maxZoomIn;
    public float currentZoomValue;



    // Start is called before the first frame update
    /// <summary>
    /// Set starting positon camera vlues
    /// </summary>
    void Start()
    {
        Application.targetFrameRate = 60;
        currentZoomValue = 20;
        maxZoomOut = 40;
        maxZoomIn = 8;
    //SetStartingCameraPosition();
}

    // Update is called once per frame
    /// <summary>
    /// Updates camera poziotion (chceck every frame for input)
    /// </summary>
    void Update()
    {
        RotateHorizontal();
        ////Debug.Log(transform.rotation.eulerAngles.ToString());
        RotateVertical();
        ForwardAxisMove();
    }
    /// <summary>
    /// Check input and rotate in horizontal axis if required
    /// </summary>
    void RotateHorizontal()
    {

        if (Input.GetKey(KeyCode.A)) //left rotation
        {
            transform.RotateAround(GameObject.FindWithTag("Center").transform.position, new Vector3(0, 1, 0), moveSpeed);
        }
        if (Input.GetKey(KeyCode.D)) //right rotation
        {
            transform.RotateAround(GameObject.FindWithTag("Center").transform.position, new Vector3(0, 1, 0), -moveSpeed);
        }

    }
    /// <summary>
    /// Check input and rotate in vertical axis if required 
    /// </summary>
    void RotateVertical()
    {
        if (Input.GetKey(KeyCode.W) &&
            transform.rotation.eulerAngles.x < maxX) //up rotation
        {
            transform.RotateAround(GameObject.FindWithTag("Center").transform.position, transform.right, moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) &&
            transform.rotation.eulerAngles.x > minX) //down rotation
        {
            transform.RotateAround(GameObject.FindWithTag("Center").transform.position, transform.right, -moveSpeed);
        }

    }

    /// <summary>
    /// Check input and zoom/ unzoom camera if required
    /// </summary>
    void ForwardAxisMove()
    {
       if(Input.GetAxis("Mouse ScrollWheel") > 0 &&
            currentZoomValue > maxZoomIn)
        {
            //transform.Translate(transform.forward * 0.01f );
            transform.position += transform.forward * scrollSpeed;
            currentZoomValue--;

        }
       if(Input.GetAxis("Mouse ScrollWheel") < 0 &&
            currentZoomValue < maxZoomOut)
        {
            //transform.Translate(transform.forward * 0.01f);
            transform.position -= transform.forward * scrollSpeed;
            currentZoomValue++;
        }
    }

    /// <summary>
    /// Set starting cameta position
    /// </summary>
    void SetStartingCameraPosition()
    {
        transform.rotation = GameObject.FindWithTag("Center").transform.rotation;
    }




}

