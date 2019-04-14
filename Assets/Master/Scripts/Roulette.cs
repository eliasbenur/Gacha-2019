using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    private int player_whoStart;
    public AnimationCurve speed_curve;
    private int speed = 5;
    private int numOf_turns = 5;

    private float Timer_total;
    private float delay_tmp;

    private bool roulling = false;

    public GameObject pointer;

    public GameObject Player, Bouche;

    // Start is called before the first frame update
    void Start()
    {
        roulling = true;

        Player.GetComponent<Player>().enabled = false;
        Bouche.GetComponent<Plane>().enabled = false;

        StartCoroutine("Player_Chose");


        
    }

    // Update is called once per frame
    void Update()
    {
        if (roulling)
        {
            delay_tmp += Time.deltaTime;
            pointer.transform.Rotate(0,0,speed * speed_curve.Evaluate(delay_tmp / Timer_total));
        }   
    }

    IEnumerator Player_Chose()
    {
        

        Timer_total = Random.Range(2.5f, 4);
        yield return new WaitForSeconds(Timer_total);
        roulling = false;
        if (pointer.transform.rotation.z > 180)
        {
            player_whoStart = 2;
        }
        else
        {
            player_whoStart = 1;
        }
        yield return new WaitForSeconds(1);
        Player.GetComponent<Player>().enabled = true;
        Bouche.GetComponent<Plane>().enabled = true;
        if (player_whoStart == 2)
        {
            GameObject.Find("JoyconManager").GetComponent<Main>().TriggerFreeze();
        }
    }
}
