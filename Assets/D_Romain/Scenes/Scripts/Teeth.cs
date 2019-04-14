using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teeth : MonoBehaviour
{
    bool snaping = false;
    public float snapTravelTime;
    // Start is called before the first frame update

    private List<Joycon> joycons;

    public float[] stick;

    public Vector3 gyro;

    public Vector3 accel;

    public int jc_ind = 0;

    public Quaternion orientation;

    private bool CanShake;

    public GameObject plane;


    void Start()
    {
        joycons = JoyconManager.Instance.j;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (joycons.Count > 0)
        {
            CheckShake();
            Joycon j = joycons[jc_ind];
            if (CanShake == true)
            {
                snaping = true;
            }

            if (snaping)
            {
                Snap();
            }


        }
    }

    void Snap()
    {
        plane.GetComponent<Plane>().StopPlaneRotation();
        if (transform.localScale.y < 8)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime*7*(1/snapTravelTime), transform.localScale.z);
        }
    }

    void ResetTeeth()
    {
        transform.localScale = new Vector3(1.5f,1, 1);
    }
    bool CheckShake()
    {
        Joycon j = joycons[jc_ind];
        if (joycons.Count > 0)
        {
            if (j.GetAccel().y > 1.5f || j.GetAccel().x > 1.5f || j.GetAccel().z > 1.5f)
            {
                Debug.Log("SHAKE IT OFF !");
                CanShake = true;
                return true;
            }
        }
        CanShake = false;
        return false;
    }




}
