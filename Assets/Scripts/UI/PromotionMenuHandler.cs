using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromotionMenuHandler : MonoBehaviour
{
    public GameObject theMenu;
    public Vector2 moveInput;
    public int selectedOption;
    public GameObject highlightSegment;

    public UnityChessPiece collisionFigure;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    theMenu.SetActive(true);
        //}
        if (theMenu.activeInHierarchy)
        {
            moveInput.x = Input.mousePosition.x - (Screen.width / 2); //gets mouse position relative to center of screen
            moveInput.y = Input.mousePosition.y - (Screen.height / 2);
            moveInput.Normalize();

            if (moveInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveInput.y, moveInput.x) / Mathf.PI;
                angle *= 180;
                if (angle < 0)
                {
                    angle += 360;
                }

                //Debug.Log(angle);

                for (int i = 0; i < 4; i++) //4 stands for queen, rook, bishop, knight
                {
                    if (angle > i * 90 && angle < (i + 1) * 90)
                    {

                        selectedOption = i;
                        highlightSegment.transform.rotation = Quaternion.Euler(0, 0, i * 90);

                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && theMenu.GetComponent<Canvas>().enabled)
            {
                collisionFigure = GameObject.FindGameObjectWithTag("Promoted").GetComponent<UnityChessPiece>();
                switch (selectedOption)
                {
                    //transform Pawn option
                    case 0: //rook
                        collisionFigure?.tranformPawn(ChessEnum.Figure.Rook);
                        break;
                    case 1: //queen
                        collisionFigure?.tranformPawn(ChessEnum.Figure.Queen);
                        break;
                    case 2: //bishop
                        collisionFigure?.tranformPawn(ChessEnum.Figure.Bishop);
                        break;
                    case 3: //knight
                        collisionFigure?.tranformPawn(ChessEnum.Figure.Knight);
                        break;
                }


                collisionFigure.tag = "Chess Piece";
                theMenu.GetComponent<Canvas>().enabled = false;
            }
        }
    }
}
