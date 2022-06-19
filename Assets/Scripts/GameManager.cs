using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameObject player;

    public bool isPlay;

    [Header("Prefabs")]
    public GameObject coin;
    public GameObject explosion;

    [Header("Post-Processing")]
    public GameObject main, gameOver, warning;

    [Header("UI")]
    public TextMeshProUGUI TimerText, SurvivalTime, PointText;

    public int point;
    float timer, coinTimer, survivalTime;
    bool isTimer;
    List<bool> explosionBool = new List<bool>();

    void Awake()
    {
        instance = this;
        player = GameObject.Find("Player");
        isTimer = true;
        isPlay = false;
        for (int i=0; i<5; i++) CoinSpawn();
        coinTimer = 10;
        timer = 3.99f;
        survivalTime = 0;

        for (int i = 0; i < 5; i++) explosionBool.Add(true); // 조건식은 이벤트 개수 만큼
    }

    
    void Update()
    {
        if (isTimer)
        {
            timer -= Time.deltaTime;
            TimerText.text = ((int)timer).ToString();
            if (timer <= 1)
            {
                isPlay = true;
                isTimer = false;
                TimerText.gameObject.SetActive(false);
            }
        }
        

        if (isPlay)
        {
            survivalTime += Time.deltaTime;
            if(survivalTime%60 < 10) SurvivalTime.text = (int)(survivalTime / 60) + ":0" + (int)(survivalTime % 60);
            else SurvivalTime.text = (int)(survivalTime / 60) + ":" + (int)(survivalTime % 60);
            PointText.text = point.ToString();


            coinTimer -= Time.deltaTime;
            if(coinTimer <= 0)
            {
                for(int i=0; i<5; i++) CoinSpawn();
                coinTimer = 5;
            }
        }

        if(survivalTime >= 60 && explosionBool[0])
        {
            explosionBool[0] = false;
            StartCoroutine(ExplosionSpawn(10));
        }
        if (survivalTime >= 90 && explosionBool[1])
        {
            explosionBool[1] = false;
            StartCoroutine(ExplosionSpawn(15));
        }

    }
    IEnumerator ExplosionSpawn(int count)
    {
        int i = 0;
        while (i < count)
        {
            GameObject GO = Instantiate(explosion);
            Vector2 vec = player.transform.position;
            GO.transform.position = new Vector2(Random.Range(vec.x + 2, vec.x - 2), Random.Range(vec.y + 2, vec.y - 2));
            yield return new WaitForSeconds(0.5f);
            i++;
        }
    }

    void CoinSpawn()
    {
        GameObject go = Instantiate(coin);
        go.transform.position = new Vector2(Random.Range(-14.5f, 14.5f), Random.Range(-14.5f, 14.5f));
    }


    public void Warning(bool isActive)
    {
        warning.SetActive(isActive);
    }

    public void GameOver()
    {
        print("게임 오버");
    }
}
