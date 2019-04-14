using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Transition : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    private Renderer p2Renderer;
    private Renderer p1Renderer;
    public Material white;
    public Material black;

    public bool player1IsHunter;
    public bool transition;
    private Vector3 vecVelocitySmooth;
    private float velocitySmooth;
    private Camera cam;
    private Vector3 camPosition;
    private float camOrthographicSize;

 
    void Start()
    {
        player1IsHunter = true;
        transition = false;
        vecVelocitySmooth = Vector3.zero;
        velocitySmooth = 0;
        cam = Camera.main;
        camPosition = cam.transform.position;
        camOrthographicSize = cam.orthographicSize;

        p1Renderer = player1.GetComponent<Renderer>();
        p2Renderer = player2.GetComponent<Renderer>();
        p1Renderer.material = white;
        p2Renderer.material = black;
    }

    // Update is called once per frame
    void Update()
    {
        if (transition == true)
        {
            float newOrthographicSize = 0.2f;
            Vector3 camTargetPosition;
            if (player1IsHunter)
            {
                camTargetPosition = new Vector3(player2.transform.position.x, player2.transform.position.y, transform.position.z);
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, newOrthographicSize, ref velocitySmooth, 1.2f);
                transform.position = Vector3.SmoothDamp(transform.position, camTargetPosition, ref vecVelocitySmooth, 0.5f, 5);
            }
            else
            {
                camTargetPosition = new Vector3(player1.transform.position.x, player1.transform.position.y, transform.position.z);
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, newOrthographicSize, ref velocitySmooth, 1.2f);
                transform.position = Vector3.SmoothDamp(transform.position, camTargetPosition, ref vecVelocitySmooth, 0.5f, 5);
            }

            if (cam.orthographicSize <= newOrthographicSize + 0.005f && transition)
            {
                Debug.Log("YO");
                ChangePlayerColor();
            }
            
        }

        if(cam.transform.position != camPosition && !transition)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, camOrthographicSize, ref velocitySmooth, 1.2f);
            transform.position = Vector3.SmoothDamp(transform.position, camPosition, ref vecVelocitySmooth, 0.5f, 5);
        }
    }

    private void ChangePlayerColor()
    {
        if (player1IsHunter)
        {
            Debug.Log("p1BLACK");
            p1Renderer.material = white;
            p2Renderer.material = black;
        }
        else
        {
            Debug.Log("p1WHITE");
            p1Renderer.material = black;
            p2Renderer.material = white;
        }
        transition = false;
        player1IsHunter = !player1IsHunter;
    }
}

