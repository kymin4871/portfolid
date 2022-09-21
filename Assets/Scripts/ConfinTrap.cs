using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinTrap : MonoBehaviour
{
    Animator anim;

    public bool a = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.enemyCount <= 0)
        {
            if (a)
            {
                anim.SetTrigger("Open");
                a = false;
            }
        }
        else if (GameManager.enemyCount > 0)
        {
            if (!a)
            {
                anim.SetTrigger("Close");
                a = true;
            }

        }
    }
}
