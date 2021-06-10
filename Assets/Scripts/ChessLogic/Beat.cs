using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    public bool isWhiteStack;
    public Stack[] stacks;
    // Start is called before the first frame update
    void Start()
    {
        stacks = new Stack[15];
        string colorNamePart = "";
        if(isWhiteStack)
        {
            colorNamePart = "White";
        }else
        {
            colorNamePart = "Black";
        }
        var stackObjects = GameObject.FindGameObjectsWithTag($"{colorNamePart}Stack");
        foreach(var stack in stackObjects)
        {
            var name = stack.name;
            var countLetter = name[name.Length - 1];
            if ("123456789".Contains(countLetter.ToString()))
            {
                stacks[countLetter - '1'] = stack.GetComponent<Stack>();
            }else
            {
                stacks[countLetter - 'A'+9] = stack.GetComponent<Stack>();
            }
        }
    }
}
