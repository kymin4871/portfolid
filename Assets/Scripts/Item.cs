using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type
    {
         Coin, Grenade, Heart
    }

    public Type type;
    public int value;

    private void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }

}
