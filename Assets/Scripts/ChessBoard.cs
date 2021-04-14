using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    GameObject[,] squares = new GameObject[8, 8];
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Chess Square");
        foreach( var item in objects)
        {
            string name = item.name;
          //  Debug.Log($"({name}) ({name[0] - 65}) ({name[1]- 49})\n");
            squares[name[0] - 65, name[1] - 49] = item;
            
        }
        string tab = "";
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                tab += squares[i, j].name+" ";
            }
            tab += "\n";
        }
        Debug.Log(tab);

    }
    
}
