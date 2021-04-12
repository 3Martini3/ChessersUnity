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

        if (Input.GetMouseButton(1))
        {
            
            transform.RotateAround(cameraRef.transform.position,new Vector3(0,90,0),-Input.GetAxis("Mouse X") * 100*moveSpeed);
           // transform.RotateAround(cameraRef.transform.position, new Vector3(0, 0, 90), -Input.GetAxis("Mouse Y") * 100 * moveSpeed);

            /*transform.RotateAround(cameraRef.transform.position,
                                             transform.right,
                                             -Input.GetAxis("Mouse Y") *100* moveSpeed);*/
        }

    }
}
