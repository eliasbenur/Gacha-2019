using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundWwise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetState("Music_state","InGame");
        AkSoundEngine.PostEvent("Play_Music", this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
