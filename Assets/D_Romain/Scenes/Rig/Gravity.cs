using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public Transform target; // Big object
    Vector3 targetDirection;

    public int forceAmount = 1;
    private Rigidbody rb;

    private float distance;


    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        targetDirection = target.position - transform.position; // Save direction
        distance = targetDirection.magnitude; // Find distance between this object and target object
        targetDirection = targetDirection.normalized; // Normalize target direction vector

        rb.AddForce(targetDirection * forceAmount * Time.deltaTime);
        


    }
}
