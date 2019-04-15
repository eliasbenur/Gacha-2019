using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Play : MonoBehaviour
{
    // Start is called before the first frame update

    private int sceneIndex = 0;
    public Image image_credit;
    public Button button_ret;

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

    public void Credit()
    {
        image_credit.gameObject.SetActive(true);
        button_ret.gameObject.SetActive(true);
    }

    public void Exit_Credit()
    {
        image_credit.gameObject.SetActive(false);
        button_ret.gameObject.SetActive(false);
    }

}
