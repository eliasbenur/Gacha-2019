using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public int currentJoyconPlayer = 0;
    public int currentJoyconMouth = 1;

    public bool isGameUp = true;

    public bool isSwapping = false;
    public bool isFreezing = false;

    public Plane plane;
    public Player player;
    public Transition transition;

    [Range(0.05f, 1f)]
    public float freezeTimeDuration;
    private float freezeTimeElapsed = 0;

    private ResourceLoader rs;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        plane = FindObjectOfType<Plane>();
        rs = new ResourceLoader();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.A))
        {
            TriggerFreeze();
        }
        if (isFreezing)
        {
            if(freezeTimeElapsed < freezeTimeDuration)
            {
                freezeTimeElapsed += Time.deltaTime;
            }
            else
            {
                this.TriggerSwap();
                this.isFreezing = false;
            }
        }
    }

    public void TriggerSwap()
    {
        if(!isSwapping)
        {
            this.isSwapping = true;

            player.UnFreeze();

            //Echange players
            currentJoyconMouth = (currentJoyconMouth == 1 ? 0 : 1);
            currentJoyconPlayer = (currentJoyconPlayer == 1 ? 0 : 1);

            ResetScene();

            this.isSwapping = false;
        }
    }

    public void TriggerFreeze()
    {
        if(!isFreezing)
        {
            //Debug.Log("startFreeze");
            this.isFreezing = true;
            this.isGameUp = false;
            transition.StartTransition();
            player.Freeze();
            this.freezeTimeElapsed = 0;
            CameraShake.Shake(0.1f, 0.75f);
        }
        
    }

    void ResetScene()
    {
        player.ResetPos();
        plane.Reset();
    }
}
