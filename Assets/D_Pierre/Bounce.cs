using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bang;

    public Main main;
    // Start is called before the first frame update
    void Start()
    {
        bang = 70;
        main = FindObjectOfType<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "BounceCollider")
        {
            float xTarget = Random.Range(-2f, 2f);
            float yTarget = Random.Range(-2f, 2f);
            Vector3 target = new Vector3(xTarget, yTarget, 0);
            collision.transform.parent.GetComponent<Player>().isBouncing = true;
            collision.transform.parent.GetComponent<Rigidbody2D>().velocity = (target - collision.transform.position).normalized*bang;
            AkSoundEngine.PostEvent("Play_bounce", Camera.main.gameObject);

            if (main.joycons.Count > 0)
            {
                Joycon j = main.joycons[main.currentJoyconPlayer];
                j.SetRumble(0.05f, 0.1f, 0.5f);
                Invoke("BreakRumbles", 0.05f);
            }

        }
        
    }

    void BreakRumbles()
    {
        Joycon j = main.joycons[main.currentJoyconMouth];
        j.SetRumble(0f, 0f, 0f);

        j = main.joycons[main.currentJoyconPlayer];
        j.SetRumble(0f, 0f, 0f);
    }


}
