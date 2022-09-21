using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance = null;

    //private void Awake()
    //{
    //    if(instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if(instance != this) { Destroy(gameObject); }
    //}
    public Player player;
    public Weapon weapon;
    public Boss boss;
    public Bullet bullet;

    public static int enemyCount = 0;
    public Text playerAmmoTxt;
    public Text playerCoinTxt;
    public Text enemyATxt;

    public int AttackUpgradeCost;
    public int SpeedUpgrdaeCost;

    public Text UpgradeUiCoinTxt;
    public Text AttackUpgradeCostTxt;
    public Text SpeedUpgradeCostTxt;
    public GameObject bossUI;
    public RectTransform bossHealthBar;

    public Image AttackGauge;
    public Image SpeedGauge;

    void Awake()
    {
        AttackUpgradeCost = bullet.damage;
        SpeedUpgrdaeCost = player.speed;


        AttackGauge.fillAmount = (float)bullet.damage / 100f;
        SpeedGauge.fillAmount = (float)player.speed / 100f;
    }

    void LateUpdate()
    {
        AttackUpgradeCost = bullet.damage;
        SpeedUpgrdaeCost = player.speed;

        AttackGauge.fillAmount = (float)bullet.damage/100f;
        SpeedGauge.fillAmount = (float)player.speed / 100f;

        playerCoinTxt.text = string.Format("{0:n0}", Player.coin);
        playerAmmoTxt.text = weapon.curAmmo + " / " + weapon.maxAmmo;
        enemyATxt.text = "x " + enemyCount.ToString();
        AttackUpgradeCostTxt.text = string.Format("{0:n0}", AttackUpgradeCost);
        SpeedUpgradeCostTxt.text = string.Format("{0:n0}", SpeedUpgrdaeCost);
        UpgradeUiCoinTxt.text = string.Format("{0:n0}", Player.coin);


        if (SpawnBoss.bossRound)
        {
            bossUI.SetActive(true);
            bossHealthBar.localScale = new Vector3((float)boss.curHealth / boss.maxHealth, 1, 1);
        }
    }
}
