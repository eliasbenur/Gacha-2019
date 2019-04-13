using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, transform.rotation.y + 5, 0);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, transform.rotation.y - 5, 0);
        }
    }
}
