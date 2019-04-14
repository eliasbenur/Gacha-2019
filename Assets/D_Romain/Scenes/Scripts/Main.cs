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

    public ResourceLoader rs;

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
    }

    public void TriggerSwap()
    {
        if(!isSwapping)
        {
            this.isSwapping = true;
            this.isFreezing = false;

            player.UnFreeze();

            //Echange players
            currentJoyconMouth = (currentJoyconMouth == 1 ? 0 : 1);
            currentJoyconPlayer = (currentJoyconPlayer == 1 ? 0 : 1);

            ResetScene();

            FindObjectOfType<GeneracionAle_ale>().New_Generation();

            this.isSwapping = false;
        }
    }

    public void TriggerFreeze()
    {
        if(!isFreezing)
        {
            //Debug.Log("startFreeze");
            plane.GetComponentInChildren<ParticleSystem>().Play();
            this.isFreezing = true;
            this.isGameUp = false;
            CameraShake.Shake(0.1f, 0.75f);
            player.Freeze();


            transition.StartTransition();
            
           // this.freezeTimeElapsed = 0;
           
        }
        
    }

    void ResetScene()
    {
        player.ResetPos();
        plane.Reset();
    }
}
