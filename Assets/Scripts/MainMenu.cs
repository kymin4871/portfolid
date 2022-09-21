using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneFader fader;

    public string loadToScene = "Main";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {

        fader.FadeTo(loadToScene);
    }

    public void Option()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
