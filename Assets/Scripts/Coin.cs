using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 15);
    }

    void Update()
    {
        transform.Rotate(new Vector2(0, 1) *200* Time.deltaTime);
    }

    public GameObject coin_Object;
    public void On_Sound()
    {
        GameObject go = Instantiate(coin_Object, GameObject.Find("Player").transform);
        Destroy(go, 2f);
    }
}
