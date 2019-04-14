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

    private bool canSnap;

    public GameObject plane;

    public Main main;

    [SerializeField]
    public float ShakeSensitivity = 1.4f;

    [Range (0.1f, 1f)]
    public float cooldownAfterSwap = 0.5f;

    private float cooldown;


    void Start()
    {
        joycons = JoyconManager.Instance.j;
        main = FindObjectOfType<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons[main.currentJoyconMouth];
            
            if (CheckShake() && canSnap)
            {
                snaping = true;
            }

            if (snaping && canSnap)
            {
                Snap();
            }
        }

        if(cooldown < cooldownAfterSwap)
        {
            cooldown += Time.deltaTime;
        }
        else
        {
            canSnap = true;
        }
    }

    void Snap()
    {
        plane.GetComponent<Plane>().StopPlaneRotation();
        if (transform.localScale.y < 8)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime*7*(1/snapTravelTime), transform.localScale.z);
        }
        else
        {
            if(snaping)
            {
                snaping = false;
                canSnap = false;
                main.TriggerFreeze();
            }
            
        }
    }

    void ResetTeeth()
    {
        transform.localScale = new Vector3(1.5f,1, 1);
    }
    bool CheckShake()
    {
        Joycon j = joycons[main.currentJoyconMouth];
        if (joycons.Count > 0)
        {
            if (j.GetAccel().y > ShakeSensitivity || j.GetAccel().x > ShakeSensitivity || j.GetAccel().z > ShakeSensitivity)
            {
                Debug.Log("SHAKE IT OFF !");
                return true;
            }
        }
        return false;
    }

    public void ResetPositionAndScale()
    {
        this.ResetTeeth();
        this.cooldown = 0;
        this.canSnap = false;
    }




}
