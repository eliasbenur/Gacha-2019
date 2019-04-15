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

    private readonly float velocityThreshold = 0.001f;

    public bool isJumping = false;
    public bool isFrozen = false;

    private Rigidbody2D rBody;

    private List<Joycon> joycons;

    public float[] stick;

    public Vector3 gyro;

    public Vector3 accel;

    private Animator anim;

    public Vector3[] v3Spawns;
    public float spawnDistSquare = 2;

    public int jc_ind = 0;

    public Quaternion orientation;

    public Main main;

    public bool deathSoundIsPlayed;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.freezeRotation = true ;
        joycons = JoyconManager.Instance.j;
        main = FindObjectOfType<Main>();

        v3Spawns = new Vector3[5];
        v3Spawns[0] = new Vector3(0, 0, 0);
        v3Spawns[1] = new Vector3(-spawnDistSquare, -spawnDistSquare, 0);
        v3Spawns[2] = new Vector3(-spawnDistSquare, spawnDistSquare, 0);
        v3Spawns[3] = new Vector3(spawnDistSquare, -spawnDistSquare, 0);
        v3Spawns[4] = new Vector3(spawnDistSquare, spawnDistSquare, 0);

        deathSoundIsPlayed = false;

        anim = GetComponent<Animator>();


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
                this.GetComponentsInChildren<ParticleSystem>()[main.currentJoyconPlayer == 0 ? 2 : 1].Play();
            }

            if (rBody.velocity.x >= -this.velocityThreshold && rBody.velocity.x <= this.velocityThreshold && rBody.velocity.x != 0) rBody.velocity = new Vector2(rBody.velocity.x, 0);
            if (rBody.velocity.y >= -this.velocityThreshold && rBody.velocity.y <= this.velocityThreshold && rBody.velocity.y != 0) rBody.velocity = new Vector2(0, rBody.velocity.y);

            if (this.transform.position.y <= -10) this.ResetPos();


            //Gauche
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

            //Droite
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

            //Descente
            if((main.currentJoyconPlayer == 1 && j.GetStick()[0] > 0.8f) || (main.currentJoyconPlayer == 0 && j.GetStick()[0] < -0.8f))
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
            else {
                this.GetComponent<BoxCollider2D>().enabled = true;
            }

            //MaxSpeed
            if (Mathf.Abs(rBody.velocity.x) > maxSpeed)
            {
                rBody.velocity = new Vector2(maxSpeed * (rBody.velocity.x > 0 ? 1 : -1), rBody.velocity.y);

            }

            this.RenderAnimations();
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
        this.transform.position = v3Spawns[Random.Range(0, 5)];
    }

    public void Freeze()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.isFrozen = true;
        this.rBody.velocity = new Vector3();
        this.GetComponentsInChildren<ParticleSystem>()[0].Pause();
        this.GetComponentsInChildren<ParticleSystem>()[3].Pause();
        this.anim.enabled = false; 
    }

    public void UnFreeze()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        this.isFrozen = false;
        this.GetComponentsInChildren<ParticleSystem>()[main.currentJoyconPlayer == 0?0:3].Play();
        this.anim.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OUT");
        if (collision.gameObject.name == "Tooth_UP" || collision.gameObject.name == "Tooth_DOWN" && !deathSoundIsPlayed)
        {
            Debug.Log("IN");
            if (main.isFreezing || main.isSwapping)
            {
                AkSoundEngine.PostEvent("Play_player_killed", Camera.main.gameObject);
                deathSoundIsPlayed = true;
                main.PlayerEaten();
            }
        }

    }

    private void RenderAnimations()
    {
        if (rBody.velocity.x != 0)
        {
            if(rBody.velocity.y == 0)//Sol
            {
                anim.SetBool("ismoving", true);
                anim.SetFloat("velocity", 0);
            }
            else//Airs
            {
                anim.SetBool("ismoving", false);
                if(rBody.velocity.y > 0)
                {
                    anim.SetFloat("velocity", 1);

                }
                else
                {
                    anim.SetFloat("velocity", -1);
                }
            }

            if(rBody.velocity.x >= 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            anim.SetFloat("velocity", 0);
            anim.SetBool("ismoving", false);
        }
    }
}
