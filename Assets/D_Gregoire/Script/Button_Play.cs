using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Play : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    private int sceneIndex = 0;

    public void Start()
    {
        AkSoundEngine.SetState("Music_state", "Menu");
        AkSoundEngine.PostEvent("Play_Music", this.gameObject);
        AkSoundEngine.PostEvent("Play_Amb", this.gameObject);
    }
    public void changeScene()
    {
        sceneIndex += 1;
        // SceneManager.LoadScene(sceneIndex);
        StartCoroutine(loadSceneCustomByPierro(sceneIndex));
    }

    public void quitGame()
    {
        Debug.Log("G KITTE LAPLIQACION");
        Application.Quit();
    }
    IEnumerator loadSceneCustomByPierro(int sceneIndex)
    {
        slider.gameObject.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        while (!async.isDone)
        {
            float pros = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = pros;
            yield return null;
        }
    }
}
