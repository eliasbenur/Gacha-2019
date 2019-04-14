using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teeth : MonoBehaviour
{
    bool snaping = false;
    public float snapTravelTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            snaping = true;
        }

        if(snaping)
        {
            Snap();
        }


    }

    void Snap()
    {
      
        if(transform.localScale.y < 8)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime*7*(1/snapTravelTime), transform.localScale.z);
        }
    }

    void ResetTeeth()
    {
        transform.localScale = new Vector3(1.5f,1, 1);
    }
    
}
