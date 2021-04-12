using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {

    public MeshRenderer square;
    private float yPosition;
    // Start is called before the first frame update
    void Start()
    {
        yPosition = transform.position.y;
        transform.position = new Vector3(square.bounds.center.x, yPosition, square.bounds.center.z);
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }*/
}
