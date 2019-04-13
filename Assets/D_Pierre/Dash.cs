using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    public float dashTime;
    public float dashSpeed;
    public float startDashTime;
    public int direction;
    Rigidbody rb;
    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = rb.velocity + (Vector3)dir.normalized * dashSpeed;
            Debug.Log(dir);
        }

        dashTime = dashTime - Time.deltaTime;

    }
}
