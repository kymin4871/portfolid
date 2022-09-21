using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameManager gameManager;
    public Transform player;
    public GameObject[] enemySpawnZones;
    public GameObject[] enemies;
    public float spawntimer = 0.8f;
    GameObject instantEnemy;

    public static bool bossRound;

    BoxCollider box;
    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    IEnumerator EnemySpawn()
    {
        for(int i = 0; i<enemySpawnZones.Length; i++)
        {
            GameManager.enemyCount++;
            enemySpawnZones[i].SetActive(true);
            yield return new WaitForSeconds(spawntimer);
            instantEnemy = Instantiate(enemies[i], enemySpawnZones[i].transform.position, enemySpawnZones[i].transform.rotation);

            instantEnemy.GetComponent<Enemy>().target = player.transform;
            gameManager.boss = instantEnemy.GetComponent<Boss>();
            enemySpawnZones[i].SetActive(false);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            box.enabled = false;
            bossRound = true;
            StartCoroutine(EnemySpawn());
            
        }
    }
    
}
