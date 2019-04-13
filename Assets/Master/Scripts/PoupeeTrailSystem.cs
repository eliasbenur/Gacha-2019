using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoupeeTrailSystem : MonoBehaviour
{

    public int numOf_PR;
    public GameObject pref_PR;
    public GameObject player_ref;

    // Start is called before the first frame update
    void Start()
    {
        if (player_ref == null)
        {
            player_ref = GameObject.Find("Player");
        }

        GameObject prev_father = null;

        for (int x = 0; x < numOf_PR; x++)
        {
            GameObject inst =  Instantiate(pref_PR, transform.position, Quaternion.identity);
            inst.transform.parent = transform;
            inst.name = "Poupee_" + x;

            if (x == 0)
            {
                inst.GetComponent<PoupeeMovement>().SetFather(player_ref);
            }
            else
            {
                inst.GetComponent<PoupeeMovement>().SetFather(prev_father);
            }

            inst.GetComponent<PoupeeMovement>().Set_pointsToFollow();

            prev_father = inst;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
