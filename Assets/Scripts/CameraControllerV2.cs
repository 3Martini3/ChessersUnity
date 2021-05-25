using UnityEngine;

public class CameraControllerV2 : MonoBehaviour
{
    public float moveSpeed;
    public float scrollSpeed;
    public float maxX;
    public float minX;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //SetStartingCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        RotateHorizontal();
        //Debug.Log(transform.rotation.eulerAngles.ToString());
        RotateVertical();

    }

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

    void SetStartingCameraPosition()
    {
        transform.rotation = GameObject.FindWithTag("Center").transform.rotation;
    }




}

