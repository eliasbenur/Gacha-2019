using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Transition : MonoBehaviour
{

    public Main main;
    public GameObject player1;
    public GameObject player2;
    public GameObject colorInversor;
    private Renderer p2Renderer1;
    private Renderer p2Renderer2;
    private Renderer p1Renderer;
    public Material white;
    public Material black;

    [Tooltip("increase value for slow transtion")]
    [Range(0.1f,0.5f)]
    public float smoothTransition;

    private bool cameraIsOnInitialPosition;
    private bool player1IsHunter;
    private bool cameraIsReset;
    public bool inTransition;
    private Vector3 vecVelocitySmooth;
    private float velocitySmooth;
    private Camera cam;
    private Vector3 camPosition;
    private float camFieldOfView;

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(0.15f);
        inTransition = true;
    }

    void Start()
    {
        main = FindObjectOfType<Main>();
        player1IsHunter = true;
        inTransition = false;
        vecVelocitySmooth = Vector3.zero;
        velocitySmooth = 0;
        cam = Camera.main;
        camPosition = cam.transform.position;
        camFieldOfView = cam.fieldOfView;
        cameraIsOnInitialPosition = true;
        cameraIsReset = true;
        p1Renderer = player1.GetComponent<Renderer>();
        p2Renderer1 = player2.transform.GetChild(0).GetComponent<Renderer>();
        p2Renderer2 = player2.transform.GetChild(1).GetComponent<Renderer>();
        p1Renderer.material = black;
        p2Renderer1.material = white;
        p2Renderer2.material = white;
    }

    // Update is called once per frame
    void Update()
    {
        //Zoom in
        if (inTransition == true)
        {
            cameraIsReset = false;
            cameraIsOnInitialPosition = false;
            float newFieldOfView = 0.2f;
            Vector3 camTargetPosition;
            camTargetPosition = new Vector3(player1.transform.position.x, player1.transform.position.y, transform.position.z);

            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, newFieldOfView, ref velocitySmooth, smoothTransition);
            transform.position = Vector3.SmoothDamp(transform.position, camTargetPosition, ref vecVelocitySmooth, smoothTransition);

            if (cam.fieldOfView <= newFieldOfView + 0.005f && inTransition)
            {
                ChangePlayerColor();
                if(colorInversor.activeSelf) colorInversor.SetActive(false);
                else colorInversor.SetActive(true);

                //charger la map
            }

        }
        else if (!cameraIsOnInitialPosition)
        {

            if(cameraIsReset == false) ResetCamera();
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, camFieldOfView, ref velocitySmooth, 0.5f);
        }
    }

    public void StartTransition()
    {
        StartCoroutine("delay");
    }
    private void ResetCamera()
    {
        cam.fieldOfView = camFieldOfView+5;
        cam.transform.position = camPosition;
        cameraIsReset = true;

    }

    private void ChangePlayerColor()
    {
        if (player1IsHunter)
        {
            p1Renderer.material = white;
            p2Renderer1.material = black;
            p2Renderer2.material = black;
        }
        else
        {
            p1Renderer.material = black;
            p2Renderer1.material = white;
            p2Renderer2.material = white;
        }
        inTransition = false;
        player1IsHunter = !player1IsHunter;
        main.TriggerSwap();
    }
}

