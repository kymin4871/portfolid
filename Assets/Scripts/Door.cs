using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject doorBlue;
    public GameObject doorRed;

    public Animator anim;

    private void Awake()
    {
    }

    void Update()
    {
        if(GameManager.enemyCount > 0)
        {
            doorRed.SetActive(true);
            doorBlue.SetActive(false);
        }
        else
        {
            doorRed.SetActive(false);
            doorBlue.SetActive(true);
            anim.SetTrigger("Open");
        }
    }
}
