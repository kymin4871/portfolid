using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{

    Animator anim;
    public GameObject switchTrigger;
    public bool spikeOnSwitch;
    public bool spikeOnAnim;
    public GameObject[] spikeTraps;



    public bool aaa;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        
    }

    private void Update()
    {
        bool onSwitch = switchTrigger.GetComponent<Switch>().onSwitch;
        bool onAnim = switchTrigger.GetComponent<Switch>().onAnim;

        spikeOnSwitch = onSwitch;
        spikeOnAnim = onAnim;
        
        if (onSwitch)
        {
            if (onAnim)
            {
                StartCoroutine(SpikeOpen());
                switchTrigger.GetComponent<Switch>().onAnim = false;
            }
        }
        else if(!spikeOnSwitch && !spikeOnAnim && switchTrigger.GetComponent<Switch>().aa)
        {
            StartCoroutine(SpikeClose());
            switchTrigger.GetComponent<Switch>().aa = false;
        }
    }

    IEnumerator SpikeClose()
    {
        foreach(GameObject spikeTrap in spikeTraps)
        {
            spikeTrap.GetComponent<Renderer>().material.color = Color.red;
            spikeTrap.GetComponent<Animator>().SetTrigger("open");
        }
        //anim.SetTrigger("open");

        //switchTrigger.GetComponent<Switch>().aa = false;
        yield return new WaitForSeconds(0.2f);

        //anim.SetInteger("Spike", 0);
    }

    IEnumerator SpikeOpen()
    {
        foreach (GameObject spikeTrap in spikeTraps)
        {
            spikeTrap.GetComponent<Renderer>().material.color = Color.blue;
            spikeTrap.GetComponent<Animator>().SetTrigger("close");
        }
        //anim.SetTrigger("close");
        yield return new WaitForSeconds(0.2f);

    }


}
