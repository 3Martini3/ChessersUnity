using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Check if piece is dragged
/// </summary>
public class ActivePiece : MonoBehaviour
{
  
    public bool isPieceDragged;
    // Start is called before the first frame update
    /// <summary>
    /// Set state of piece
    /// </summary>
    void Start()
    {
        isPieceDragged=false;
    }
}
