using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    public GameObject player;
    public GameObject lifeUI;

    public int count = 0;

    public int maxHealth; 
    private void Awake()
    {
    }
    void Update()
    {
        Transform[] lifes = lifeUI.GetComponentsInChildren<Transform>(true);

        if (count <= Player.life)
        {
            lifes[count].gameObject.SetActive(true);
        }
        count++;

        lifeUI.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3((float)-3.4, (float)5.5, 0));
    }
}
