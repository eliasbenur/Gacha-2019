using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Canvas : MonoBehaviour
{
    // Start is called before the first frame update
    EventSystem m_eventSystem;
    void OnEnable()
    {
        m_eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO Faire le script de la navigation 
}
