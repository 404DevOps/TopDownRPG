using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform cameraTarget;
    public Vector3 cameraOffset = new Vector3(0,0,-10);
    public float boundX = 0.15f;
    public float boundY = 0.05f;


    private void LateUpdate()
    {
        Vector3 delta =  Vector3.zero;

        float deltaX = cameraTarget.position.x - transform.position.x;

        if (deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < cameraTarget.position.x)
                delta.x = deltaX - boundX;
            else
                delta.x = deltaX + boundX;
        }

        float deltaY = cameraTarget.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < cameraTarget.position.y)
                delta.y = deltaY - boundY;
            else
                delta.y = deltaY + boundY;
        }

        transform.position += new Vector3(delta.x, delta.y, 0);


    }
}
