using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Joycon> joycons;

    public float[] stick;

    public Vector3 gyro;

    public Vector3 accel;

    public int jc_ind = 0;

    public Quaternion orientation;

    public bool planeCanRotate = true;

    void Start()
    {
        joycons = JoyconManager.Instance.j;
    }

    // Update is called once per frame
    void Update()
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            stick = j.GetStick();
            if (planeCanRotate == true)
            {
                if (/*Droite*/j.GetStick()[1] < -0.6f)
                {
                    transform.Rotate(0, transform.rotation.y + (100 * Time.deltaTime), 0);
                }
                if (/*Gauche*/j.GetStick()[1] > 0.6f)
                {
                    transform.Rotate(0, transform.rotation.y - (100 * Time.deltaTime), 0);
                }
            }

        }

    }

    public void StopPlaneRotation()
    {
        planeCanRotate = false;
    }
}
