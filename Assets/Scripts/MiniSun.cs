using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSun : MonoBehaviour
{
    GameObject target;

    public Rigidbody2D myRigid;

    public float speed;
    void Start()
    {
        target = GameObject.Find("Player");

        myRigid = GetComponent<Rigidbody2D>();

        speed = 4f;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (GameManager.instance.isPlay)
        {
            if (target != null)
            {
                Vector2 vec = target.transform.position - transform.position;
                myRigid.velocity = vec.normalized * speed;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D GO)
    {
        if (GO.collider.CompareTag("Player"))
        {
            print("»ç¸Á");
        }
    }
}
