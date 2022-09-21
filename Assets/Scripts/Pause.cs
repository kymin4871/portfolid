using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public SceneFader fader;
    public string loadToScene;

    public GameObject pause;
    void Update()
    {
        Toogle();
    }


    public void Toogle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(!pause.activeSelf);
            if (pause.activeSelf)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
            else
            {
                Time.timeScale = 1f;
                //Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
        }
    }

    public void Continue()
    {
        pause.SetActive(!pause.activeSelf);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("메인으로");
        fader.FadeTo(loadToScene);
        Application.Quit();
    }
}
    