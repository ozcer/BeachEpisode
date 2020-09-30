using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject patronPf;
    public GameObject itemPf;
    public List<Zone> spawnZones = new List<Zone>();
    public Zone dineZone;
    public Player player;
    public Storage storage;
    public Text timeText;
    public Text moneyText;
    public Text endMoneyText;
    public Text highscoreText;
    public Fader endScreen;
    public Sounder sounder;

    [Header("Spawning Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;
    public int minSpawnSize;
    public int maxSpawnSize;

    [Header("Time Settings")]
    public float timeScale = 1f;
    [Range(0, 1)]
    public float timeOfDay;
    public float gameDuration;
    public float timeLeft;
    public bool doubled = false;
    public bool gameOver = false;

    public int money = 0;
    public int highScore = 0;

    public bool isTitle = false;
    public float minGrayscale = 0.2f;
    public float maxGrayscale = 0.7f;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static GameManager Get() { return instance; }
    #endregion


    private void Start()
    {
        timeLeft = gameDuration;

        StartCoroutine(RandomSpawn(0f));
    }
    // Update is called once per frame
    void Update()
    {
        if (isTitle) return;

        if (Input.GetKeyDown("r"))
        {
            Restart();
        }

        // Time
        Time.timeScale = timeScale;
        timeOfDay = timeLeft / gameDuration;
        if (timeLeft > 0) timeLeft -= Time.deltaTime;
        else GameOver();

        timeText.text = timeLeft + "";
        moneyText.text = money + "";

    }

    IEnumerator RandomSpawn()
    {
        
        float minDelay = minSpawnDelay;
        float maxDelay = maxSpawnDelay;
        if (timeLeft < gameDuration / 2)
        {
            minDelay /= 2;
            maxDelay /= 2;
        }
        float delay = Random.Range(minDelay, maxDelay);
        StartCoroutine(RandomSpawn(delay));
        yield return new WaitForSeconds(0);
    }

    IEnumerator RandomSpawn(float delay)
    {
        print(delay + " seconds before next wave");
        yield return new WaitForSeconds(delay);
        int size = Random.Range(minSpawnSize, maxSpawnSize + 1);
        print("Here comes " + size + " mandems");
        for (int i = 0; i < size; i++)
        {
            SpawnPatron();
        }
        if (!gameOver) StartCoroutine(RandomSpawn());
        
    }

    void SpawnPatron()
    {
        // Spawn patron with random color
        GameObject patronGo = Instantiate(patronPf, RandomSpawnZone().RandomPoint(), Quaternion.identity);
        SpriteRenderer patronRenderer = patronGo.transform.Find("Logic/Graphics").GetComponent<SpriteRenderer>();
        Color c = Helper.RandomColor();
        while (c.grayscale < minGrayscale || c.grayscale > maxGrayscale)
        {
            c = Helper.RandomColor();
        }
        patronRenderer.color = c;
    }

    public Zone RandomSpawnZone()
    {
        int i = Random.Range(0, spawnZones.Count);
        return spawnZones[i];
    }

    public void GameOver()
    {
        gameOver = true;
        endMoneyText.text = money + "";
        highscoreText.text = "best " + highScore;
        endScreen.FadeIn();
        StopAllCoroutines();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Patron");
        foreach (GameObject go in gos)
        {
            Patron p = go.transform.Find("Logic").Find("Graphics").GetComponent<Patron>();
            p.SetSpeed(10);
            p.Leave();
        }

        foreach (Transform child in player.transform.Find("Head"))
        {
            Destroy(child.gameObject);
        }

        storage.orders.Clear();
    }

    public void Restart()
    {
        print("Restarting");
        GameOver();
        gameOver = false;
        endScreen.FadeOut();
        
        money = 0;

        Start();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
