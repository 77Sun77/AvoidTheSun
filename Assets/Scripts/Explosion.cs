using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    bool PlayerOnTop;

    SpriteRenderer mySprite;

    float timer;

    float alpha;
    bool alpha_Bool;
    void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
        timer = 1f;
        alpha_Bool = true;
        alpha = 100;
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (PlayerOnTop) GameManager.instance.GameOver();
            GameObject GO = Instantiate(explosion);
            GO.transform.position = transform.position;
            Destroy(GO, 2f);
            Destroy(gameObject);
        }


        if (alpha_Bool)
        {
            Color color = mySprite.color;
            color.a = alpha / 255f;
            alpha -= 200 * Time.deltaTime;
            if (alpha <= 0)
            {
                alpha_Bool = false;
                color.a = 0 / 255f;
            }
            mySprite.color = color;
        }
        else
        {
            Color color = mySprite.color;
            color.a = alpha / 255f;
            alpha += 200 * Time.deltaTime;
            if (alpha >= 100)
            {
                alpha_Bool = true;
                color.a = 100 / 255f;
            }
            mySprite.color = color;
        }
    }

    public void OnTriggerEnter2D(Collider2D GO)
    {
        if (GO.CompareTag("Player")) PlayerOnTop = true;
    }
    public void OnTriggerExit2D(Collider2D GO)
    {
        if (GO.CompareTag("Player")) PlayerOnTop = false;
    }
}
