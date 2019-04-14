using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Transition : MonoBehaviour
{

    public Main main;
    public GameObject player1;
    public GameObject player2;
    private Renderer p2Renderer1;
    private Renderer p2Renderer2;
    private Renderer p1Renderer;

    [Tooltip("increase value for slow transtion")]
    [Range(0.1f,0.5f)]
    public float smoothTransition;

    private bool player1IsHunter;
    public bool inTransition;
    private Vector3 vecVelocitySmooth;
    private float velocitySmooth;
    private Camera cam;
    private Vector3 camPosition;
    private float camFieldOfView;

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
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

        p1Renderer = player1.GetComponent<Renderer>();
        p2Renderer1 = player2.transform.GetChild(0).GetComponent<Renderer>();
        p2Renderer2 = player2.transform.GetChild(1).GetComponent<Renderer>();
        p1Renderer.material = main.rs.whiteMat;
        p2Renderer1.material = main.rs.blackMat;
        p2Renderer2.material = main.rs.blackMat;
    }

    // Update is called once per frame
    void Update()
    {
        //Zoom in
        if (inTransition == true)
        {

            float newFieldOfView = 0.2f;
            Vector3 camTargetPosition;
            camTargetPosition = new Vector3(player1.transform.position.x, player1.transform.position.y, transform.position.z);

            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, newFieldOfView, ref velocitySmooth, smoothTransition);
            transform.position = Vector3.SmoothDamp(transform.position, camTargetPosition, ref vecVelocitySmooth, smoothTransition);
            
            if (cam.fieldOfView <= newFieldOfView + 0.005f && inTransition)
            {
                ChangePlayerColor();
                //charger la map
            }
            
        }
        //Zoom out
        else
        {

            if (cam.fieldOfView >= camFieldOfView - 0.5f)
            {
                cam.fieldOfView = camFieldOfView;
            }
            else
            {
                cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, camFieldOfView, ref velocitySmooth, smoothTransition);
            }
            if (cam.transform.position != camPosition)
            {
                    Debug.Log(camFieldOfView);
                    transform.position = Vector3.SmoothDamp(transform.position, camPosition, ref vecVelocitySmooth, smoothTransition);
            }
        }     
    }

    public void StartTransition()
    {
        StartCoroutine("delay");
    }
    private void ChangePlayerColor()
    {
        if (player1IsHunter)
        {
            p1Renderer.material = main.rs.whiteMat;
            p2Renderer1.material = main.rs.blackMat;
            p2Renderer2.material = main.rs.blackMat;

        }
        else
        {
            p1Renderer.material = main.rs.blackMat;
            p2Renderer1.material = main.rs.whiteMat;
            p2Renderer2.material = main.rs.whiteMat;
        }
        inTransition = false;
        player1IsHunter = !player1IsHunter;
        main.TriggerSwap();
    }
}

