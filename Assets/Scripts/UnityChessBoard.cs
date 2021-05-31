using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChessBoard : MonoBehaviour
{
    public GameObject[,] squares;
    void Start()
    {
        squares = new GameObject[8, 8];
        Debug.Log($"there is started{name}");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Chess Square");
        foreach( var item in objects)
        {
            string name = item.name;
          //  Debug.Log($"({name}) ({name[0] - 65}) ({name[1]- 49})\n");
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
        Debug.Log(tab);*/

    }

    public void clearActive()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                
               // squares[i,j].GetComponent<ChessSquare>().availableMove = false;
                squares[i, j].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        Debug.Log("Cleared");
    }
    
}
