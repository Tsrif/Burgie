using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPipe : MonoBehaviour
{
    public GameObject wallColliders;
    public GameObject groundColliders;

    //ground=12
    //wall =13

    private int ground = 12;
    private int wall = 13;
    public float inTime;
    public float startTime;

    private float angle = 90;
    private Vector3 zRotation = new Vector3(0, 0, 1);

    // Use this for initialization
    void Start()
    {

        StartCoroutine(rotatePipe(angle, zRotation, inTime, startTime));

    }

    //Since the pipes are rotating, what is considered ground and walls need to swap
    void swapColliders()
    {
        if (wallColliders.layer == ground)
        {
            wallColliders.layer = wall;
            wallColliders.tag = "WallColliders";
        }
        else if (wallColliders.layer == wall)
        {
            wallColliders.layer = ground;
            wallColliders.tag = "Untagged";
        }

        if (groundColliders.layer == ground)
        {
            groundColliders.layer = wall;
            groundColliders.tag = "WallColliders";
        }
        else if (groundColliders.layer == wall)
        {
            groundColliders.layer = ground;
            groundColliders.tag = "Untagged";
        }
    }



    private IEnumerator rotatePipe(float angle, Vector3 axis, float inTime, float startTime)
    {
        yield return new WaitForSeconds(startTime);

        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        while (true)
        {
            // save starting rotation position
            Quaternion startRotation = transform.rotation;

            float deltaAngle = 0;

            // rotate until reaching angle
            while (deltaAngle < angle)
            {
                deltaAngle += rotationSpeed * Time.deltaTime;
                deltaAngle = Mathf.Min(deltaAngle, angle);

                transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);


                yield return null;
            }
            swapColliders();

            // delay here
            yield return new WaitForSeconds(inTime);
        }
    }
}
