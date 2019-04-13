using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range (1f, 8f)]
    public float fallMultiplier;

    [Range(1f, 3f)]
    public float lowJumpMultiplier;

    [Range(1f, 20f)]
    public float jumpVelocity;

    [Range(0.5f, 100f)]
    public float speed;

    [Range(1f, 100f)]
    public float maxSpeed;

    private readonly float velocityThreshold = 0.001f;

    public bool isJumping = false;

    private Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.freezeRotation = true ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.isJumping = true;
        }
        else
        {
            this.isJumping = false;
        }

        //Basic impulsion
        if (Input.GetKey(KeyCode.UpArrow) && rBody.velocity.y == 0 )
        {
            rBody.velocity += Vector3.up * jumpVelocity;
        }

       // if (rBody.velocity.x >= -this.velocityThreshold && rBody.velocity.x <= this.velocityThreshold && rBody.velocity.x != 0) rBody.velocity = new Vector3(rBody.velocity.x, 0, 0);
       // if (rBody.velocity.y >= -this.velocityThreshold && rBody.velocity.y <= this.velocityThreshold && rBody.velocity.y != 0) rBody.velocity = new Vector3(0, rBody.velocity.y, 0);


        if (Input.GetKey(KeyCode.LeftArrow)) {
            rBody.velocity -= new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rBody.velocity += new Vector3(1, 0, 0) * speed *  Time.deltaTime;
           
        }
        
        //MaxSpeed
        if (Mathf.Abs(rBody.velocity.x) > maxSpeed)
        {
            rBody.velocity = new Vector3(maxSpeed * (rBody.velocity.x > 0?1:-1), rBody.velocity.y, 0);

        }

        this.ProceedJump();
    }

    //Kinectic jump modulation
    private void ProceedJump()
    {
        //Debug.Log("Y = "+ rBody.velocity.y);

        if (rBody.velocity.y < 0)
        {
            rBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rBody.velocity.y > 0 && !this.isJumping)
        {
            rBody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
