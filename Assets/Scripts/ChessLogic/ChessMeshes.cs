using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMeshes : MonoBehaviour
{
    public Mesh queen;
    public Mesh knight;
    public Mesh bishop;
    public Mesh rook;


    public Mesh returnMesh(ChessEnum.Figure figure)
    {
        switch (figure)
        {
            case (ChessEnum.Figure.Bishop):
            {
                 return bishop;
            }
            case (ChessEnum.Figure.Knight):
            {
                return knight;
            }
            case (ChessEnum.Figure.Queen):
            {
                return queen;
            }
            case (ChessEnum.Figure.Rook):
            {
                return rook;
            }
            default:
            {
                    return null;
            }

        }
    }
}
