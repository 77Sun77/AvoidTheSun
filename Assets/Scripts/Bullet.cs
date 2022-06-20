using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 target, dir;
    bool isMove;

    private void Start()
    {
        Destroy(gameObject, 10);
    }
    void Update()
    {
        if(isMove) transform.position += dir.normalized * 10 * Time.deltaTime;
    }

    public void TargetSetting()
    {
        target = GameObject.Find("Player").transform.position;
        dir = target - transform.position;
        isMove = true;
    }

    public void OnTriggerEnter2D(Collider2D GO)
    {
        if (GO.CompareTag("Player"))
        {
            GameManager.instance.SufferDamage();
            Destroy(gameObject);
        }
    }
}
