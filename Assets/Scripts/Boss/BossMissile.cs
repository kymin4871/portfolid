using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : Bullet
{
    public Transform target;
    public GameObject boss;
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        boss = GameObject.Find("Enemy D(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.GetComponent<Boss>().isDead)
        {
            Destroy(this.gameObject);
            return;
        }
        nav.SetDestination(target.position);
    }
}
