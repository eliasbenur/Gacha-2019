using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [Range(50f, 200f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    private List<Joycon> joycons;

    public float[] stick;

    public Vector3 gyro;

    public Vector3 accel;

    public int jc_ind;

    public Quaternion orientation;

    public bool planeCanRotate = true;

    public Teeth t1;
    public Teeth t2;

    public Main main;

    void Start()
    {
        joycons = JoyconManager.Instance.j;
        main = FindObjectOfType<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        Joycon j = joycons[main.currentJoyconMouth];
        if (joycons.Count > 0)
        {
            stick = j.GetStick();
            if (planeCanRotate == true)
            {
                if (/*Droite*/(main.currentJoyconPlayer == 1 && j.GetStick()[1] < -0.6f) || (main.currentJoyconPlayer == 0 && j.GetStick()[1] > 0.6f))
                {
                    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
                }
                if (/*Gauche*/(main.currentJoyconPlayer == 1 && j.GetStick()[1] > 0.6f) || (main.currentJoyconPlayer == 0 && j.GetStick()[1] < -0.6f))
                {
                    transform.Rotate(0, rotationSpeed * Time.deltaTime * -1, 0, Space.Self);
                }
            }

        }

    }

    public void StopPlaneRotation()
    {
        planeCanRotate = false;
    }

    public void Reset()
    {
        t1.ResetPositionAndScale();
        t2.ResetPositionAndScale();
        this.planeCanRotate = true;
    }
}
