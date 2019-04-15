using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMouth : MonoBehaviour
{
    Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jawClose()
    {
        Debug.Log("JawClose");
        AkSoundEngine.PostEvent("Play_jawclose", Camera.main.gameObject);
    }
    public void OnEvent()
    {
        if (playerScript.deathSoundIsPlayed == false)
        {
            Debug.Log("playMiss");
            AkSoundEngine.PostEvent("Play_miss", Camera.main.gameObject);
        }
        playerScript.deathSoundIsPlayed = false;
        Debug.Log("gnack " + name);
    }
}
