using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Transition : MonoBehaviour
{

    public Main main;
    public GameObject player;
    public GameObject colorInversor;
    public GameObject planeForBlackSfx;
    private Renderer p2Renderer1;
    private Renderer p2Renderer2;
    private Renderer p1Renderer;
    public Material white;
    public Material black;


    public RuntimeAnimatorController contWhite;
    public RuntimeAnimatorController contBlack;
    public Sprite spWhite;
    public Sprite spBlack;

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
    float newFieldOfView;

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(0.3f);
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
        //p1Renderer = player.GetComponent<Renderer>();
        main.blackT.SetActive(false);
        main.whiteT.SetActive(true);
        main.blackPlBack.SetActive(false);
        main.whitePlBack.SetActive(true);
        cam.backgroundColor = new Color(255, 255, 255);
       // p1Renderer.material = black;
        planeForBlackSfx.SetActive(true);
        newFieldOfView = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        //Zoom in
        if (inTransition == true)
        { 
            cameraIsReset = false;
            cameraIsOnInitialPosition = false;
           
            Vector3 camTargetPosition;
            camTargetPosition.x = player.transform.position.x;
            camTargetPosition.y = player.transform.position.y;
            camTargetPosition.z = player.transform.position.z-0.2f;



            //cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, newFieldOfView, ref velocitySmooth, smoothTransition);
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(camTargetPosition.x, camTargetPosition.y, camTargetPosition.z), ref vecVelocitySmooth, smoothTransition);


            if (transform.position.z >= -0.3f)
            {

                ChangePlayerColor();

            }
            else
            {
                //transform.Translate(0, 0, 10 * Time.deltaTime);
            }

        }
        else if (!cameraIsOnInitialPosition)
        {

            if(cameraIsReset == false) ResetCamera();
           // cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, camFieldOfView, ref velocitySmooth, 0.5f);
        }
        else
        {
            cam.transform.position = new Vector3(0, 0, -80);
        }
    }

    public void StartTransition()
    {
        AkSoundEngine.PostEvent("Play_mouth_transition", cam.gameObject);
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
            main.blackT.SetActive(true);
            main.whiteT.SetActive(false);
            main.blackPlBack.SetActive(true);
            main.whitePlBack.SetActive(false);
            cam.backgroundColor = new Color(0, 0, 0);
            // p1Renderer.material = white;
            main.player.gameObject.GetComponent<SpriteRenderer>().sprite = spWhite;
            main.player.gameObject.GetComponent<Animator>().runtimeAnimatorController = contWhite;
            planeForBlackSfx.SetActive(false);
        }
        else
        {
            main.blackT.SetActive(false);
            main.whiteT.SetActive(true);
            main.blackPlBack.SetActive(false);
            main.whitePlBack.SetActive(true);
            cam.backgroundColor = new Color(255, 255, 255);
            //  p1Renderer.material = black;
            main.player.gameObject.GetComponent<SpriteRenderer>().sprite = spBlack;
            main.player.gameObject.GetComponent<Animator>().runtimeAnimatorController = contBlack;
            planeForBlackSfx.SetActive(true);
        }
        inTransition = false;
        player1IsHunter = !player1IsHunter;
        main.TriggerSwap();
    }
}

