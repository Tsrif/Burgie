using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTopController : MonoBehaviour {
    public Vector3 startPoint;
    public Vector3 aboveStart;
    public Vector3 aboveEnd;
    public Vector3 endPoint;
    private List<Vector3> points;
    private int destPoint = 0;
    public float speed;
    public bool showPath;
    public float time;

    // Use this for initialization
    void Start()
    {
        points = new List<Vector3> { startPoint, aboveStart, aboveEnd, endPoint, aboveEnd, aboveStart };
        transform.position = startPoint;
    }

    private void Update()
    {
        StartCoroutine(wait(time));
    }

    IEnumerator wait(float time) {
        yield return new WaitForSeconds(time);
        moveTop();
    }

    private void moveTop() {
        // Set the top to go to the currently selected destination.
        transform.position = Vector2.Lerp(transform.position, points[destPoint], speed);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        if (new Vector3(Mathf.Round(transform.position.x * 100), Mathf.Round(transform.position.y * 100), Mathf.Round(transform.position.z * 100)) * 0.01f == points[destPoint])
        {
           destPoint = (destPoint + 1) % points.Count;
        }
    }

    private void OnDrawGizmos()
    {
        if (showPath)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPoint, aboveStart);
            Gizmos.DrawLine(aboveStart, aboveEnd);
            Gizmos.DrawLine(aboveEnd, endPoint);
        }

    }
}
