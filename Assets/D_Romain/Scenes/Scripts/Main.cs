﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject victoryAffiche;
    public Material black;
    public Material white;

    public AkSoundEngine Wwise;
    public Plane plane;
    public Player player;
    public Transition transition;

    [Range(1f,3f)]
    public float cooldownGnack;
    private float cd;
    private bool canGnack = true;

    public int confrontationScore = 0;
    public int scoreforBWin;
    public int scoreforWWin;
    public int scorePerRound;

    public List<Joycon> joycons;

    public float[] stick;

    public Vector3 gyro;

    public Vector3 accel;

    public int jc_ind = 0;

    public Quaternion orientation;

    [Range(0f, 5f)]
    public int speedGameModifier;
    public int nbGames = 0;

    [SerializeField]
    public float ShakeSensitivity = 1.4f;

    public ResourceLoader rs;

    public float delay_Max_ToShake;
    public float delay_Max_ToShake_tmp;
    public Text delay_txt;

    public Slider slider_points;
    public Button bt_resetGame;

    public GameObject wise_prefab;

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
        if (delay_Max_ToShake_tmp < 0 && this.isGameUp)
        {
            delay_Max_ToShake_tmp = delay_Max_ToShake;
            Animator a = this.currentJoyconMouth == 0 ? animBTooth : animWTooth;
            a.SetTrigger("Gnack");
            TriggerFreeze();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            this.confrontationScore = 10;
            CheckVictory();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.confrontationScore = -10;
            CheckVictory();
        }


        if (Input.GetKeyDown(KeyCode.A) && this.isGameUp)
        {
            TriggerFreeze();
            Animator a = this.currentJoyconMouth == 0 ? animBTooth : animWTooth;
            a.SetTrigger("Gnack");
        }

        if(CheckShake() && !this.isFreezing && cd >= cooldownGnack && this.isGameUp)
        {
            TriggerFreeze();
            Animator a = this.currentJoyconMouth == 0 ? animBTooth : animWTooth;
            a.SetTrigger("Gnack");
        }

        if(cd < cooldownGnack)
        {
            cd += Time.deltaTime;
        }
        else
        {
            this.canGnack = true;
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

            cd = 0;
            this.canGnack = false;

            player.Reset_num_mouths_coll();
        }
    }

    public void TriggerFreeze()
    {
        if(!isFreezing)
        {
            //Debug.Log("startFreeze");
            Invoke("playFeedbacks", 0.31f);
            this.isFreezing = true;
            //this.isGameUp = false;
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
        CameraShake.Shake(0.1f, 0.75f);
        player.Freeze();

        if (joycons.Count > 0)
        {
            Joycon j = joycons[this.currentJoyconMouth];
            j.SetRumble(0.2f, 0.3f, 1f);


            if (player.deathSoundIsPlayed)
            {
                j = joycons[this.currentJoyconPlayer];
                j.SetRumble(0.2f, 0.3f, 1f);
            }

            Invoke("BreakRumbles", 0.1f);
        }

    }

    void BreakRumbles()
    {
        Joycon j = joycons[this.currentJoyconMouth];
        j.SetRumble(0f, 0f, 0f);

        j = joycons[this.currentJoyconPlayer];
        j.SetRumble(0f, 0f, 0f);
    }

    public void PlayerEaten()
    {
        this.confrontationScore += (scorePerRound) * (currentJoyconMouth==1?-1:1);
        Debug.Log("Advancement : " + confrontationScore);
        slider_points.value = confrontationScore;
        CheckVictory();
    }

    private void CheckVictory()
    {
        if(this.confrontationScore <= scoreforWWin )
        {
            Debug.Log("White wins");
            victoryAffiche.GetComponent<Animator>().SetBool("isBlackWinner", false);
            Invoke("popVictory", 1f);
            
            this.isGameUp = false;
            bt_resetGame.gameObject.SetActive(true);
        }

        if (this.confrontationScore >= scoreforBWin)
        {
            Debug.Log("Black wins");
            victoryAffiche.GetComponent<Animator>().SetBool("isBlackWinner", true);
            Invoke("popVictory", 1f);

            this.isGameUp = false;
            bt_resetGame.gameObject.SetActive(true);
        }
    }

    void popVictory()
    {
        victoryAffiche.transform.position = new Vector3(-0.1f, 0, -58f);
        player.gameObject.SetActive(false);
    }

    bool CheckShake()
    {
        if (joycons.Count > 0)
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
        }
        else
        {
            if ((Input.GetButton("Shake_P2") && currentJoyconMouth == 1) || (Input.GetButton("Shake_P1") && currentJoyconMouth == 0))
            {
                return true;
            }
        }

        return false;
    }
    
    public void ResetGame()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("Main_Menu");
    }

}
