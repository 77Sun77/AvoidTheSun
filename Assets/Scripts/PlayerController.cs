using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SunController sun;

    public Rigidbody2D myRigid;

    public float speed;

    void Start()
    {
        sun = GameObject.Find("Sun").GetComponent<SunController>();
        myRigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Move();

        float dist = Vector2.Distance(sun.transform.position, transform.position) - sun.transform.localScale.x;
        if(dist <= 0.35f) GameManager.instance.Warning(true);
        else GameManager.instance.Warning(false);
    }

    void Move()
    {
        if (GameManager.instance.isPlay)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            myRigid.velocity = new Vector2(h, v) * speed;
        }
    }

    public void OnTriggerEnter2D(Collider2D GO)
    {
        if (GO.CompareTag("Coin"))
        {
            GameManager.instance.point += 100;
            sun.SizeUp();
            GO.GetComponent<Coin>().On_Sound();
            Destroy(GO.gameObject);
        }

        if (GO.CompareTag("Sun"))
        {
            GameManager.instance.GameOver();
        }

        
    }

    public void OnTriggerStay2D(Collider2D GO)
    {
        if (GO.CompareTag("BlackHole"))
        {
            speed = 1.5f;
        }
    }

    public void OnTriggerExit2D(Collider2D GO)
    {
        if (GO.CompareTag("BlackHole"))
        {
            speed = 5;
        }
    }

}
