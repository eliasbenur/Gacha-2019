using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTPS_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BounceCollider")
        {
            AkSoundEngine.PostEvent("Play_FTPS", Camera.main.gameObject);
            collision.transform.parent.GetComponent<Player>().isBouncing = false;
        }
    }
}
