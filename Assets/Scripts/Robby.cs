using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robby : MonoBehaviour
{
    Animator anim;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(Behave());
    }

    IEnumerator Behave()
    {
        float num = Random.Range(0, 4);
        if (num == 0)
        {
            anim.SetTrigger("doShot");
            yield return new WaitForSeconds(0.5f);
            anim.SetTrigger("doShot");
            yield return new WaitForSeconds(0.5f);
            anim.SetTrigger("doShot");
        }
        else if (num == 1)
        {
            anim.SetBool("isRun", false);
        }
        else if(num ==2)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetTrigger("doReload");
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Behave());
    }

}
