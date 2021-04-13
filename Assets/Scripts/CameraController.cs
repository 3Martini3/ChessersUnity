using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraRef;
    public float moveSpeed = 0.5f;
    public float scrollSpeed = 10f;
    public float minX = 0f;
    public float maxX = 0f;
    public float minY = 0f;
    public float maxY = 0f;
    public float minZ = 0f;
    public float maxZ = 0f;

    void Update()
    {
       /* if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX,transform.position.y,transform.position.z);
            }else if (transform.position.x < minX)
            {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            }
            if (transform.position.z > maxZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
            }else if (transform.position.z < minZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
            if(transform.position.y>maxY)
            {
                transform.position = new Vector3(transform.position.x,maxY,transform.position.z);
            }else if(transform.position.y<minY)
            {
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            }
        }*/

        if (Input.GetMouseButton(0)) //LMB
        {
            transform.RotateAround( new Vector3(0,0,0),new Vector3(0,1,0),-Input.GetAxis("Mouse X") * 100*moveSpeed);


        }
        if (Input.GetMouseButton(1)) //RMB
        {
            //relativeX is for relative coordinate of vector orthogonal to position vector and parallel to XY plane
            //math beyond this magical thing is quite simple- assuming that our vector v is parallel to XY, and assuming that position vector is p:
            //px*vx + py*vy + pz*vz = 0 (a dot product) ; where vy = 0
            //px*vx + pz*vz = 0 ; may vz = 1 and equation becomes simple:
            //vx = -pz/px 
            //of course its not normalized, but it works
            var relativeX = -transform.position.z / transform.position.x;
            transform.RotateAround(transform.position, new Vector3(relativeX, 0, 1), -Input.GetAxis("Mouse Y") * 100 * moveSpeed);
        }

    }
}
