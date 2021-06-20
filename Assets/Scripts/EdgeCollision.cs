using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check if elements are coliding with game board border 
/// </summary>
public class EdgeCollision : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);


       /* if(collision.gameObject.tag=="Chess Piece")
        {
            Debug.Log("Go Back! Border");
            collision.gameObject.GetComponent<UnityChessPiece>().goBackToSquare();

        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Chess Piece")
        {
            other.gameObject.GetComponent<UnityChessPiece>().hoverSquareName = "Border";
        }
    }
}
