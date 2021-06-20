using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Initialize game board 
/// </summary>
public class UnityChessBoard : MonoBehaviour
{
    public GameObject[,] squares;
    /// <summary>
    /// CInitialize game board as game object two dimensional table, assign names to squares
    /// </summary>
    void Start()
    {
        squares = new GameObject[8, 8];
        //Debug.Log($"there is started{name}");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Chess Square");
        foreach( var item in objects)
        {
            string name = item.name;
          //  //Debug.Log($"({name}) ({name[0] - 65}) ({name[1]- 49})\n");
            squares[name[1] - '1', name[0] - 'A'] = item;
            
        }
        /*string tab = "";
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                tab += squares[i, j].name+" ";
            }
            tab += "\n";
        }
        //Debug.Log(tab);*/

    }

    
}
