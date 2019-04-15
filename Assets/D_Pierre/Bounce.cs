using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bang;
    // Start is called before the first frame update
    void Start()
    {
        bang = 70;
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

            Debug.Log(((target - collision.transform.position).normalized * bang).magnitude);
        }
        
    }


}
