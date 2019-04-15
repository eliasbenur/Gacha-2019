using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Play : MonoBehaviour
{
    // Start is called before the first frame update

    private int sceneIndex = 0;

    public void changeScene()
    {
        sceneIndex += 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public void quitGame()
    {
        Debug.Log("G KITTE LAPLIQACION");
        Application.Quit();
    }
}
