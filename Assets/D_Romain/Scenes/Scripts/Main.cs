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

    public float confrontationScore = 0;

    [Range(0.2f, 5f)]
    public float speedGameModifier;
    public int nbGames = 0;

    public ResourceLoader rs;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        plane = FindObjectOfType<Plane>();
        rs = new ResourceLoader();
        plane.GetComponentInChildren<ParticleSystem>().Stop();
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
            this.nbGames++;

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

    public void PlayerEaten()
    {
        this.confrontationScore += (20 + (speedGameModifier)) * (-1 * currentJoyconMouth);
    }

    private void CheckVictory()
    {
        if(this.confrontationScore<=-100 )
        {
            Debug.Log("Black wins");
        }

        if (this.confrontationScore >= 100)
        {
            Debug.Log("White wins");
        }
    }
}
