using System.Collections;
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
    public GameObject m_canvas;
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

    private List<Joycon> joycons;

    public float[] stick;

    public Vector3 gyro;

    public Vector3 accel;

    public int jc_ind = 0;

    public Quaternion orientation;

    [Range(1f, 5f)]
    public int speedGameModifier;
    public int nbGames = 0;

    [SerializeField]
    public float ShakeSensitivity = 1.4f;

    public ResourceLoader rs;

    public float delay_Max_ToShake;
    public float delay_Max_ToShake_tmp;
    public Text delay_txt;

    bool cameraShakeTimer = true;

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

        if(delay_Max_ToShake_tmp<1.5f && cameraShakeTimer)
        {
            cameraShakeTimer = false;
            Debug.Log("shake");
            CameraShake.Shake(2f, 0.075f);
        }

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

        if(CheckShake() && !this.isFreezing && cd >= cooldownGnack)
        {
            Animator a = this.currentJoyconMouth == 0 ? animBTooth : animWTooth;
            a.SetTrigger("Gnack");
            this.TriggerFreeze();
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

            this.cameraShakeTimer = true;
        }
    }

    public void TriggerFreeze()
    {
        if(!isFreezing)
        {
            //Debug.Log("startFreeze");
            Invoke("playFeedbacks", 0.31f);
            this.isFreezing = true;
            this.isGameUp = false;
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

        Joycon j = joycons[this.currentJoyconMouth];
        j.SetRumble(0.2f, 0.3f, 1f);


        if (player.deathSoundIsPlayed)
        {
            j = joycons[this.currentJoyconPlayer];
            j.SetRumble(0.2f, 0.3f, 1f);
        }

        Invoke("BreakRumbles", 0.1f);
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
        this.confrontationScore += (scorePerRound + (speedGameModifier*nbGames)) * (currentJoyconMouth==1?-1:1);
        Debug.Log("Advancement : " + confrontationScore);
        CheckVictory();
    }

    private void CheckVictory()
    {
        if(this.confrontationScore<=-scoreforWWin )
        {
            m_canvas.SendMessage("ManageScoreWhite", SendMessageOptions.RequireReceiver);
            Debug.Log("White wins");
            
        }

        if (this.confrontationScore >= scoreforBWin)
        {
            m_canvas.SendMessage("ManageScoreBlack", SendMessageOptions.RequireReceiver);
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
