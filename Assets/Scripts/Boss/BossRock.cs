using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : Bullet
{
    Rigidbody rigid;
    float angularPower = 2;
    float scaleValue = 0.1f;
    bool isShoot;
    public GameObject boss;

    void Awake()
    {
        boss = GameObject.Find("Enemy D(Clone)");
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    void Update()
    {
        if (boss.GetComponent<Boss>().isDead)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(1.5f);
        isShoot = true;
    }

    IEnumerator GainPower()
    {
        while (!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.005f;
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration); ;
            yield return null;
        }
    }


    
}
