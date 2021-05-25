using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerV2 : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float scrollSpeed = 10f;
    public float minX = 0f;
    public float maxX = 0f;
    public float minY = 0f;
    public float maxY = 0f;
    public float minZ = 0f;
    public float maxZ = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        RotateHorizontal();
        RotateVertical();
    }

    void RotateHorizontal()
    {
        
        if(Input.GetKey(KeyCode.A)) //left rotation
        {
            transform.Translate(transform.right * -1 * moveSpeed);
            transform.LookAt(GameObject.FindWithTag("Center").transform);
        }
        if (Input.GetKey(KeyCode.D)) //right rotation
        {
            transform.Translate(transform.right * moveSpeed);
            transform.LookAt(GameObject.FindWithTag("Center").transform);
        }

    }

    void RotateVertical() 
    {

        if (Input.GetKey(KeyCode.W)) //up rotation
        {
            transform.Translate(transform.up *  moveSpeed);
            transform.LookAt(GameObject.FindWithTag("Center").transform);
        }
        if (Input.GetKey(KeyCode.S)) //down rotation
        {
            transform.Translate(transform.up * -1 * moveSpeed);
            transform.LookAt(GameObject.FindWithTag("Center").transform);
        }

    }

    void SetStartingCameraPosition()
    {

    }
}

