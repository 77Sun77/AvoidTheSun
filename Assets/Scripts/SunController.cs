using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{ 
    GameObject target;

    public Rigidbody2D myRigid;

    public float speed;
    void Start()
    {
        target = GameObject.Find("Player");

        myRigid = GetComponent<Rigidbody2D>();

        speed = 0.5f;
        limitSpeed = 0.7f;
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

    float limitSpeed;
    public void SizeUp()
    {
        transform.localScale += new Vector3(0.05f, 0.05f);

        if (speed >= 2.5f) return;
        if(transform.localScale.x >= limitSpeed)
        {
            speed += 0.25f;
            limitSpeed += 0.2f;
        }
    }
}
