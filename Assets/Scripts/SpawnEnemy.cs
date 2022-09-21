using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject[] enemySpawnZones;
    public GameObject[] enemies;
    public float spawntimer = 0.8f;

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
            GameObject instantEnemy = Instantiate(enemies[i], enemySpawnZones[i].transform.position, enemySpawnZones[i].transform.rotation);

            instantEnemy.GetComponent<Enemy>().target = player.transform;

            enemySpawnZones[i].SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            box.enabled = false;
            StartCoroutine(EnemySpawn());
        }
    }

}
