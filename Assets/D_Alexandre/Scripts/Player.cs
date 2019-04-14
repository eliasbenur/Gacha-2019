using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range (1f, 8f)]
    public float fallMultiplier;

    [Range(1f, 5f)]
    public float lowJumpMultiplier;

    [Range(1f, 40f)]
    public float jumpVelocity;

    [Range(0.5f, 3f)]
    public float directionMultiplier;

    [Range(0.5f, 100f)]
    public float acceleration;

    [Range(1f, 100f)]
    public float maxSpeed;

    private readonly float velocityThreshold = 0.1f;

    public bool isJumping = false;
    public bool isFrozen = false;

    private Rigidbody2D rBody;

    private List<Joycon> joycons;

    public float[] stick;

    public Vector3 gyro;

    public Vector3 accel;

    public int jc_ind = 0;

    public Quaternion orientation;

    public Main main;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.freezeRotation = true ;
        joycons = JoyconManager.Instance.j;
        main = FindObjectOfType<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        this.ProceedJump();
        Joycon j = joycons[main.currentJoyconPlayer];

        if (joycons.Count > 0 && !isFrozen)
        {
            CheckShake();
            
            stick = j.GetStick();
            if ((j.GetButton(Joycon.Button.DPAD_UP) && main.currentJoyconPlayer == 1) || (j.GetButton(Joycon.Button.DPAD_DOWN) && main.currentJoyconPlayer == 0))
            {
                this.isJumping = true;
            }
            else
            {
                this.isJumping = false;
            }

            //Basic impulsion
            if (((j.GetButton(Joycon.Button.DPAD_UP) && main.currentJoyconPlayer == 1) || ((j.GetButton(Joycon.Button.DPAD_DOWN) && main.currentJoyconPlayer == 0))) && rBody.velocity.y == 0)
            {
                rBody.velocity += Vector2.up * jumpVelocity;
            }

            if (rBody.velocity.x >= -this.velocityThreshold && rBody.velocity.x <= this.velocityThreshold && rBody.velocity.x != 0) rBody.velocity = new Vector2(rBody.velocity.x, 0);
            if (rBody.velocity.y >= -this.velocityThreshold && rBody.velocity.y <= this.velocityThreshold && rBody.velocity.y != 0) rBody.velocity = new Vector2(0, rBody.velocity.y);

            if (this.transform.position.y <= -10) this.ResetPos();

            if ((main.currentJoyconPlayer == 1 && j.GetStick()[1] < -0.6f) || (main.currentJoyconPlayer == 0 && j.GetStick()[1] > 0.6f))
            {
                if(rBody.velocity.x < 0)
                {
                    rBody.velocity += Vector2.left * acceleration * Time.deltaTime * directionMultiplier;
                }
                else
                {
                    rBody.velocity += Vector2.left * acceleration * Time.deltaTime;
                }
                
            }

            if ((main.currentJoyconPlayer == 1 && j.GetStick()[1] > 0.6f) || (main.currentJoyconPlayer == 0 && j.GetStick()[1] < -0.6f))
            {

                if (rBody.velocity.x > 0)
                {
                    rBody.velocity += Vector2.right * acceleration * Time.deltaTime * directionMultiplier;
                }
                else
                {
                    rBody.velocity += Vector2.right * acceleration * Time.deltaTime;
                }

            }

            //MaxSpeed
            if (Mathf.Abs(rBody.velocity.x) > maxSpeed)
            {
                rBody.velocity = new Vector2(maxSpeed * (rBody.velocity.x > 0 ? 1 : -1), rBody.velocity.y);

            }
        }
    }

    bool CheckShake()
    {
        Joycon j = joycons[jc_ind];
        if (joycons.Count > 0)
        {
            if (j.GetAccel().y > 1.5f || j.GetAccel().x > 1.5f || j.GetAccel().z > 1.5f)
            {
                Debug.Log("SHAKE IT OFF !");
                return true;
            }
        }
        return false;
    }

    //Kinectic jump modulation
    private void ProceedJump()
    {
        //Debug.Log("Y = "+ rBody.velocity.y);
        if(!isFrozen)
        {
            if (rBody.velocity.y < 0)
            {
                rBody.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rBody.velocity.y > 0 && !this.isJumping)
            {
                rBody.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

        }
        
    }

    public void ResetPos()
    {
        this.transform.position = new Vector3();
    }

    public void Freeze()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.isFrozen = true;
        this.rBody.velocity = new Vector3();
    }

    public void UnFreeze()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        this.isFrozen = false;
    }
}
