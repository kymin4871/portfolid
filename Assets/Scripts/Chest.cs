using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    new Animation animation;
    BoxCollider box;
    public GameObject coin;
    public GameObject chestBody;
    public int coinCount;

    // Start is called before the first frame update
    void Awake()
    {
        animation = GetComponent<Animation>();
        box = GetComponent<BoxCollider>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animation.Play("ChestOpenAnim");
            box.enabled = false;
            StartCoroutine(CoinReleae(chestBody, coinCount));
        }
    }

    IEnumerator CoinReleae(GameObject obj, int coinCounts)
    {
        for (int i = 0; i < coinCounts; i++)
        {
            int x = Random.Range(-20, 20);
            int z = Random.Range(-20, 20);
            GameObject coinClone = Instantiate(coin, obj.transform.position, Quaternion.identity);
            Rigidbody rigid = coinClone.GetComponent<Rigidbody>();
            rigid.AddForce(new Vector3(x, 40, z), ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
