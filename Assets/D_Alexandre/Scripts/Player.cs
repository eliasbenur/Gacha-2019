﻿using System.Collections;
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

    [Range(0.5f, 100f)]
    public float acceleration;

    [Range(1f, 100f)]
    public float maxSpeed;

    private readonly float velocityThreshold = 0.1f;

    public bool isJumping = false;

    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.freezeRotation = true ;
    }

    // Update is called once per frame
    void Update()
    {
        this.ProceedJump();

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
            rBody.velocity += Vector2.up * jumpVelocity;
        }

        if (rBody.velocity.x >= -this.velocityThreshold && rBody.velocity.x <= this.velocityThreshold && rBody.velocity.x != 0) rBody.velocity = new Vector2(rBody.velocity.x, 0);
        if (rBody.velocity.y >= -this.velocityThreshold && rBody.velocity.y <= this.velocityThreshold && rBody.velocity.y != 0) rBody.velocity = new Vector2(0, rBody.velocity.y);

        if (this.transform.position.y <= -10) this.transform.position = new Vector3();

        if (Input.GetKey(KeyCode.LeftArrow)) {
            rBody.velocity += Vector2.left * acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rBody.velocity += Vector2.right * acceleration *  Time.deltaTime;
           
        }
        
        //MaxSpeed
        if (Mathf.Abs(rBody.velocity.x) > maxSpeed)
        {
            rBody.velocity = new Vector2(maxSpeed * (rBody.velocity.x > 0?1:-1), rBody.velocity.y);

        }


    }

    //Kinectic jump modulation
    private void ProceedJump()
    {
        //Debug.Log("Y = "+ rBody.velocity.y);
        
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
