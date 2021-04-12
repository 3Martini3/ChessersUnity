using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DragAndDrop : MonoBehaviour
{

    private bool dragging = false;
    private float distance { get; set; }
    private float defaultYPosition { get; set; }

    void Start()
    {
        defaultYPosition=transform.position.y;
    }

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
        transform.position = new Vector3(transform.position.x, defaultYPosition, transform.position.z);
    }

    void Update()
    {
        if (dragging)
        {
            distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Camera.main.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, transform.position.y, rayPoint.z);
        }
    }
}
