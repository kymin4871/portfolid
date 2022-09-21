using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject switchTrigger;

    public bool onSwitch;
    public bool onAnim;
    public bool aa;

    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            onSwitch = !onSwitch;
            if(onSwitch)
            {
                onAnim = true;
            }
    
            if (onSwitch)
            {
                switchTrigger.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else
            {
                aa = true;
                switchTrigger.GetComponent<MeshRenderer>().material.color = Color.red;
            }

            Destroy(other.gameObject);
        }
    }


}
