using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    public float interval;
    float currentTimer = 0;
    bool canSwith = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     

        currentTimer += Time.deltaTime;
        if(currentTimer > interval)
        {
                  Debug.Log("hello");
                currentTimer = 0;
                var hinge = GetComponent<HingeJoint>();

                var spring = hinge.spring;
 
                spring.targetPosition -= spring.targetPosition ;

            
         
        }

    }
}
