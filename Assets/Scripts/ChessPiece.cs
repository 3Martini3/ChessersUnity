using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {

    public GameObject square;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(square.transform.position.x, square.transform.position.y, square.transform.position.z);
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }*/
}
