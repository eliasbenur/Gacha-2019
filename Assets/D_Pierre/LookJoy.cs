using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookJoy : MonoBehaviour
{

    public float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        float inputY = Input.GetAxis("joyLookX");
        float inputX = Input.GetAxis("joyLookY");

        if (inputX != 0 || inputY != 0)
        {
            direction = new Vector3(inputY, -inputX, 0);
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}