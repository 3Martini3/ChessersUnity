using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollision : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);


        if(collision.gameObject.tag=="Chess Piece")
        {
            collision.gameObject.GetComponent<ChessPiece>().goBackToSquare();
        }
    }
}
