using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public GameManager gm;

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AttackUpgrade()
    {
        if(gm.AttackUpgradeCost <= Player.coin)
        {
            gm.bullet.damage += 10;
            Player.coin -= gm.AttackUpgradeCost;
        }
        else 
        {
            Debug.Log("코인이 부족합니다");
        }
    }

    public void SpeedUpgrade()
    {
        if(gm.SpeedUpgrdaeCost < Player.coin)
        {
            gm.player.speed += 10;
            Player.coin -= gm.SpeedUpgrdaeCost;
        }
        else
        {
            Debug.Log("코인이 부족합니다");
        }
    }

    public void Clear()
    {
        Debug.Log("다음 라운드 실행");
    }

}
