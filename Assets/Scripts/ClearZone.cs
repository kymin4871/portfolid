using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearZone : MonoBehaviour
{

    public GameObject UpgradeUI;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Å¬¸®¾î");
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            UpgradeUI.SetActive(true);
        }
    }
}
