﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    private Camera cam;
    public int currentJoyconPlayer = 0;
    public int currentJoyconMouth = 1;

    public bool isGameUp = true;

    public bool isSwapping = false;
    public bool isFreezing = false;

    public Animator animWTooth;
    public Animator animBTooth;
    public GameObject blackT;
    public GameObject whiteT;
    public GameObject whitePlBack;
    public GameObject blackPlBack;
    public Material black;
    public Material white;

    public AkSoundEngine Wwise;
    public Plane plane;
    public Player player;
    public Transition transition;



    public float confrontationScore = 0;

    private List<Joycon> joycons;

    public float[] stick;

    public Vector3 gyro;

    public Vector3 accel;

    public int jc_ind = 0;

    public Quaternion orientation;

    [Range(0.2f, 5f)]
    public float speedGameModifier;
    public int nbGames = 0;

    [SerializeField]
    public float ShakeSensitivity = 1.4f;

    public ResourceLoader rs;

    public float delay_Max_ToShake;
    public float delay_Max_ToShake_tmp;
    public Text delay_txt;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = FindObjectOfType<Player>();
        plane = FindObjectOfType<Plane>();
        rs = new ResourceLoader();
        plane.GetComponentInChildren<ParticleSystem>().Stop();
        joycons = JoyconManager.Instance.j;
        animBTooth = blackT.GetComponent<Animator>();
        animWTooth = whiteT.GetComponent<Animator>();

        delay_Max_ToShake_tmp = delay_Max_ToShake;
    }

    // Update is called once per frame
    void Update()
    {
       
        delay_Max_ToShake_tmp -= Time.deltaTime;
        //delay_txt.text = ((int)delay_Max_ToShake_tmp).ToString();
        if (delay_Max_ToShake_tmp < 0)
        {
            delay_Max_ToShake_tmp = delay_Max_ToShake;
            Animator a = this.currentJoyconMouth == 0 ? animBTooth : animWTooth;
            a.SetTrigger("Gnack");
            TriggerFreeze();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            TriggerFreeze();
            Animator a = this.currentJoyconMouth == 0 ? animBTooth : animWTooth;
            a.SetTrigger("Gnack");
        }

        if(CheckShake() && !this.isFreezing)
        {
            Animator a = this.currentJoyconMouth == 0 ? animBTooth : animWTooth;
            a.SetTrigger("Gnack");
            this.TriggerFreeze();
        }
    }

    public void TriggerSwap()
    {
        if(!isSwapping)
        {
            this.isSwapping = true;
            this.isFreezing = false;

            delay_Max_ToShake_tmp = delay_Max_ToShake;

            player.UnFreeze();
            this.nbGames++;

            //Echange players
            currentJoyconMouth = (currentJoyconMouth == 1 ? 0 : 1);
            currentJoyconPlayer = (currentJoyconPlayer == 1 ? 0 : 1);

            //change player music
            string state = (currentJoyconPlayer == 1 ? "player1" : "player2");
            AkSoundEngine.SetState("Player_Music", state);

            ResetScene();

            FindObjectOfType<GeneracionAle_ale>().New_Generation(this.currentJoyconPlayer == 0 ? black : white);

            this.isSwapping = false;
        }
    }

    public void TriggerFreeze()
    {
        if(!isFreezing)
        {
            //Debug.Log("startFreeze");
            Invoke("playFeedbacks", 0.23f);
            this.isFreezing = true;
            this.isGameUp = false;
            if(player.deathSoundIsPlayed == false) AkSoundEngine.PostEvent("Play_miss", Camera.main.gameObject);
            player.deathSoundIsPlayed = false;
            transition.StartTransition();
            // this.freezeTimeElapsed = 0;
        }
        
    }

    void ResetScene()
    {
        player.ResetPos();
        plane.Reset();
    }

    void playFeedbacks()
    {
        plane.GetComponentInChildren<ParticleSystem>().Play();
        AkSoundEngine.PostEvent("Play_jawclose", cam.gameObject);
        CameraShake.Shake(0.1f, 0.75f);
        player.Freeze();
    }

    public void PlayerEaten()
    {
        this.confrontationScore += (20 + (speedGameModifier*nbGames)) * (currentJoyconMouth==1?-1:1);
        Debug.Log("Advancement : " + confrontationScore);
        CheckVictory();
    }

    private void CheckVictory()
    {
        if(this.confrontationScore<=-100 )
        {
            Debug.Log("White wins");
        }

        if (this.confrontationScore >= 100)
        {
            Debug.Log("Black wins");
        }
    }

    bool CheckShake()
    {
        Joycon j = joycons[this.currentJoyconMouth];
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

}
