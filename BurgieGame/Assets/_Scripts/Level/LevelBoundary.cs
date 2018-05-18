using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create the boundary positions in the map, draw them, the campera can retreive them 
public class LevelBoundary : MonoBehaviour {

    public float Boundary_MIN_Y;
    public float Boundary_MAX_Y;
    public float Boundary_MIN_X;
    public float Boundary_MAX_X;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        //left and right 
        Gizmos.DrawLine(new Vector3(Boundary_MIN_X,Boundary_MIN_Y,0), new Vector3(Boundary_MIN_X, Boundary_MAX_Y, 0));
        Gizmos.DrawLine(new Vector3(Boundary_MAX_X, Boundary_MIN_Y, 0), new Vector3(Boundary_MAX_X, Boundary_MAX_Y, 0));

        //bottom and top 
        Gizmos.DrawLine(new Vector3(Boundary_MIN_X, Boundary_MIN_Y, 0), new Vector3(Boundary_MAX_X, Boundary_MIN_Y, 0));
        Gizmos.DrawLine(new Vector3(Boundary_MIN_X, Boundary_MAX_Y, 0), new Vector3(Boundary_MAX_X, Boundary_MAX_Y, 0));

    }


}
