using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maintest : MonoBehaviour
{
    public Transform planeTeeth;
    bool planeCanRotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(planeCanRotate)
        {
            if (Input.GetKey(KeyCode.Q))planeTeeth.Rotate(0, transform.rotation.y + 5, 0);
            if (Input.GetKey(KeyCode.D))planeTeeth.Rotate(0, transform.rotation.y - 5, 0);
        }

    }

    public void StopTeethRotation()
    {
        planeCanRotate = false;
    }
}
