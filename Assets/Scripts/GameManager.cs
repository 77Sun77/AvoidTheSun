using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameObject player;
    GameObject sun;

    public bool isPlay;

    [Header("Prefabs")]
    public GameObject coin;
    public GameObject explosion;
    public GameObject bullet;
    public GameObject blackHole;

    [Header("Post-Processing")]
    public GameObject main, gameOver, warning;

    [Header("UI")]
    public TextMeshProUGUI TimerText, SurvivalTime, PointText;

    public int point;
    float timer, coinTimer, survivalTime;
    bool isTimer;
    List<bool> eventBool = new List<bool>();

    public int hp;

    void Awake()
    {
        instance = this;
        player = GameObject.Find("Player");
        sun = GameObject.Find("Sun");
        isTimer = true;
        isPlay = false;
        for (int i=0; i<5; i++) CoinSpawn();
        coinTimer = 10;
        timer = 3.99f;
        survivalTime = 0;

        for (int i = 0; i < 17; i++) eventBool.Add(true); // 조건식은 이벤트 개수 만큼

        hp = 3;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Time.timeScale = 1;

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

        if(survivalTime >= 60 && eventBool[0])
        {
            eventBool[0] = false;
            StartCoroutine(ExplosionSpawn(10));
        }
        if (survivalTime >= 90 && eventBool[1])
        {
            eventBool[1] = false;
            StartCoroutine(ExplosionSpawn(15));
        }
        if (survivalTime >= 120 && eventBool[2])
        {
            eventBool[2] = false;
            StartCoroutine(BulletSpawn(15));
        }
        if (survivalTime >= 150 && eventBool[3])
        {
            eventBool[3] = false;
            BlackHoleSpawn(3);
            StartCoroutine(BulletSpawn(15));
        }
        if (survivalTime >= 210 && eventBool[4])
        {
            eventBool[4] = false;
            BlackHoleSpawn(3);
            StartCoroutine(ExplosionSpawn(15));
            StartCoroutine(BulletSpawn(20));
        }
        if (survivalTime >= 240 && eventBool[5])
        {
            eventBool[5] = false;
            StartCoroutine(ExplosionSpawn(15));
            StartCoroutine(ExplosionSpawn(15));
        }
        if(survivalTime >= 270 && eventBool[6])
        {
            eventBool[6] = false;
            StartCoroutine(BulletSpawn(50));
        }
        if (survivalTime >= 300 && eventBool[7])
        {
            isPlay = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            sun.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            sun.transform.localScale = Vector2.Lerp(sun.transform.localScale, new Vector2(6f, 6f), 1f * Time.deltaTime);
            Vector3 sunPos = new Vector3(sun.transform.position.x, sun.transform.position.y, -10);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, sunPos, 5f * Time.deltaTime);
            sun.GetComponent<SunController>().isAwakening = true;
            sun.GetComponent<SunController>().speed = 0;
            if(Vector2.Distance(sun.transform.localScale, new Vector2(6f, 6f)) < 0.1f)
            {
                eventBool[7] = false;
                isPlay = true;
                Camera.main.transform.localPosition = new Vector3(0, 0, -10);
                sun.GetComponent<SunController>().Awakening();
            }
        }
        if (survivalTime >= 330 && eventBool[8])
        {
            eventBool[8] = false;
            BlackHoleSpawn(3);
            StartCoroutine(ExplosionSpawn(15));
            StartCoroutine(BulletSpawn(20));
        }
        if (survivalTime >= 360 && eventBool[9])
        {
            eventBool[9] = false;
            for(int y=-15; y<=15; y += 30)
            {
                for (int x = -15; x <= 15; x += 5)
                {
                    GameObject GO = Instantiate(bullet);
                    GO.transform.position = new Vector2(x, y);
                    GO.GetComponent<Bullet>().TargetSetting();
                }
            }
            for (int x = -15; x <= 15; x += 30)
            {
                for (int y = -15; y <= 15; y += 5)
                {
                    GameObject GO = Instantiate(bullet);
                    GO.transform.position = new Vector2(x, y);
                    GO.GetComponent<Bullet>().TargetSetting();
                }
            }
        }
        if (survivalTime >= 365 && eventBool[10])
        {
            eventBool[10] = false;
            for (int y = -15; y <= 15; y += 30)
            {
                for (int x = -15; x <= 15; x += 5)
                {
                    GameObject GO = Instantiate(bullet);
                    GO.transform.position = new Vector2(x, y);
                    GO.GetComponent<Bullet>().TargetSetting();
                }
            }
            for (int x = -15; x <= 15; x += 30)
            {
                for (int y = -15; y <= 15; y += 5)
                {
                    GameObject GO = Instantiate(bullet);
                    GO.transform.position = new Vector2(x, y);
                    GO.GetComponent<Bullet>().TargetSetting();
                }
            }
        }
        if (survivalTime >= 370 && eventBool[11])
        {
            eventBool[11] = false;
            for (int y = -15; y <= 15; y += 30)
            {
                for (int x = -15; x <= 15; x += 5)
                {
                    GameObject GO = Instantiate(bullet);
                    GO.transform.position = new Vector2(x, y);
                    GO.GetComponent<Bullet>().TargetSetting();
                }
            }
            for (int x = -15; x <= 15; x += 30)
            {
                for (int y = -15; y <= 15; y += 5)
                {
                    GameObject GO = Instantiate(bullet);
                    GO.transform.position = new Vector2(x, y);
                    GO.GetComponent<Bullet>().TargetSetting();
                }
            }
        }
        if (survivalTime >= 390 && eventBool[12])
        {
            eventBool[12] = false;
            StartCoroutine(ExplosionSpawn(50));
            StartCoroutine(ExplosionSpawn(50));
            StartCoroutine(BulletSpawn(50));
            StartCoroutine(BulletSpawn(50));
        }
        if (survivalTime >= 420 && eventBool[13])
        {
            eventBool[13] = false;
            StartCoroutine(ExplosionSpawn(0));
        }
        if (survivalTime >= 450 && eventBool[14])
        {
            eventBool[14] = false;
            StartCoroutine(BulletSpawn(0));
        }
        if (survivalTime >= 510 && eventBool[15])
        {
            eventBool[15] = false;
            StartCoroutine(ExplosionSpawn(0));
            StartCoroutine(BulletSpawn(0));
        }
        if (survivalTime >= 570 && eventBool[15])
        {
            eventBool[15] = false;
            StartCoroutine(ExplosionSpawn(0));
            StartCoroutine(BulletSpawn(0));
        }
        if(survivalTime >= 600 && eventBool[16])
        {
            eventBool[16] = false;
            for (int y = -15; y <= 15; y += 30)
            {
                for (int x = -15; x <= 15; x += 5)
                {
                    GameObject GO = Instantiate(bullet);
                    GO.transform.position = new Vector2(x, y);
                    GO.GetComponent<Bullet>().TargetSetting();
                }
            }
        }

        if (hp <= 2) hp_Images[0].GetComponent<Image>().color = new Color(0, 0, 0);
        if (hp <= 1) hp_Images[1].GetComponent<Image>().color = new Color(0, 0, 0);
        if (hp <= 0) hp_Images[2].GetComponent<Image>().color = new Color(0, 0, 0);
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
            if (count == 0) i = 0;
        }
    }
    IEnumerator BulletSpawn(int count)
    {
        int i = 0;
        while (i < count)
        {
            GameObject GO = Instantiate(bullet);
            int random = Random.Range(0, 2);
            if(random == 0)
            {
                random = Random.Range(0, 1);
                if(random == 0) GO.transform.position = new Vector2(player.transform.position.x-9.5f, Random.Range(-15f, 15f));
                else GO.transform.position = new Vector2(player.transform.position.x + 9.5f, Random.Range(-15f, 15f));
            }
            if (random == 1)
            {
                random = Random.Range(0, 1);
                if (random == 0) GO.transform.position = new Vector2(Random.Range(-15f, 15f), player.transform.position.y - 5.5f);
                else GO.transform.position = new Vector2(Random.Range(-15f, 15f), player.transform.position.y + 5.5f);
            }
            GO.GetComponent<Bullet>().TargetSetting();
            yield return new WaitForSeconds(0.5f);
            i++;
            if (count == 0) i = 0;
        }

    }
    void BlackHoleSpawn(int count)
    {
        for(int i=0; i<count; i++)
        {
            GameObject GO = Instantiate(blackHole);
            GO.transform.position = new Vector2(Random.Range(-14.5f, 14.5f), Random.Range(-14.5f, 14.5f));
        }
    }

    void CoinSpawn()
    {
        if (GameObject.FindGameObjectsWithTag("Coin").Length >= 30) return;
        GameObject go = Instantiate(coin);
        go.transform.position = new Vector2(Random.Range(-14.5f, 14.5f), Random.Range(-14.5f, 14.5f));
    }


    public void Warning(bool isActive)
    {
        warning.SetActive(isActive);
    }

    public GameObject[] hp_Images;
    public void SufferDamage()
    {
        hp -= 1;
        if (hp <= 0) GameOver();
    }

    public GameObject gameOver_UI;
    public void GameOver()
    {
        print("게임 오버");
        isPlay = false;
        Destroy(player.transform.Find("Core").gameObject);
        gameOver_UI.SetActive(true);
        gameOver_UI.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Score:" + (point + ((int)survivalTime * 10));
        gameOver.SetActive(true);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        sun.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        hp_Images[0].GetComponent<Image>().color = new Color(0, 0, 0);
        hp_Images[1].GetComponent<Image>().color = new Color(0, 0, 0);
        hp_Images[2].GetComponent<Image>().color = new Color(0, 0, 0);
    }
    public void ReStart()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
