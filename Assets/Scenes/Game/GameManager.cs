using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public const int GAME_STATUS_STARTED  = 0;
    public const int GAME_STATUS_FINISHED = 1;

    public const int NIL2_TYPE_NONE    = 0;
    public const int NIL2_TYPE_RED     = 1;
    public const int NIL2_TYPE_BLUE    = 2;
    public const int NIL2_TYPE_GREEN   = 3;
    public const int NIL2_TYPE_YELLOW  = 4;
    public const int NIL2_TYPE_PURPLE  = 5;
    public const int NIL2_TYPE_ORANGE  = 6;
    public const int NIL2_TYPE_RAINBOW = 7;
    public const int NIL2_TYPE_WHITE   = 8;
    public const int NIL2_TYPE_GRAY    = 9;
    public const int NIL2_TYPE_BLACK   = 10;

    public Animator fade;
    public float sceneTransitionSec;
    public AudioClip gameBGM;
    public AudioClip hitSE;
    public AudioClip dropSE;
    public GameObject ishitsumiko;
    public GameObject nil2Prefab;
    public GameObject scoreUpdatedLabel;
    public GameObject restartGuideLabel;
    public GameObject quitGuideLabel;
    public Text lifeText;
    public Text scoreText;
    public int initialLife;
    public float introSec;
    public float spawnClockSec;
    public float spawnClockSecMin;
    public float accerateSpawnClockClockSec;
    public float accerateSpawnClockSec;
    public int nil2RedScore;
    public int nil2BlueScore;
    public int nil2GreenScore;
    public int nil2YellowScore;
    public int nil2PurpleScore;
    public int nil2OrangeScore;
    public int nil2RainbowScore;
    public int nil2WhiteScore;
    public int nil2GrayScore;
    public int nil2BlackScore;
    public int nil2RedBorder;
    public int nil2BlueBorder;
    public int nil2GreenBorder;
    public int nil2YellowBorder;
    public int nil2PurpleBorder;
    public int nil2OrangeBorder;
    public int nil2RainbowBorder;
    public int nil2WhiteBorder;
    public int nil2GrayBorder;
    public int nil2BlackBorder;

    private Vector3 screenMinPos;
    private Vector3 screenMaxPos;

    private SoundBehaviour sound;
    private int gameStatus;
    private int life;
    private int score;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        screenMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        screenMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        gameStatus     = GAME_STATUS_STARTED;
        life           = initialLife;
        lifeText.text  = life.ToString();
        score          = 0;
        scoreText.text = score.ToString();

        InvokeRepeating("SpawnNil2",          introSec+spawnClockSec,              spawnClockSec);
        InvokeRepeating("AccerateSpawnClock", introSec+accerateSpawnClockClockSec, accerateSpawnClockClockSec);

        sound = SoundBehaviour.FindInstance();
        sound.PlayBGMWithRestart(gameBGM);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || (gameStatus == GAME_STATUS_FINISHED && Input.GetKeyDown(KeyCode.X)))
        {
            BackToStartMenu();
        }
    }

    void SpawnNil2()
    {
        Instantiate(nil2Prefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    void AccerateSpawnClock()
    {
        spawnClockSec = Mathf.Max(spawnClockSec - accerateSpawnClockSec, spawnClockSecMin);

        CancelInvoke("SpawnNil2");
        InvokeRepeating("SpawnNil2", spawnClockSec, spawnClockSec);
    }

    void FinishGame()
    {
        Destroy(ishitsumiko.GetComponent<PlayerController>());
        Destroy(ishitsumiko.GetComponent<IshitsumikoBehaviour>());

        gameStatus = GAME_STATUS_FINISHED;
        restartGuideLabel.SetActive(true);
        quitGuideLabel.SetActive(true);

        var date          = DateTime.Today.ToString("yyyy/MM/dd");
        var scoreRecorded = SaveData.RecordScore(10, score, date);
        if (scoreRecorded)
        {
            scoreUpdatedLabel.SetActive(true);
        }
    }

    void RestartGame()
    {
        MoveScene("GameScene");
    }

    void BackToStartMenu()
    {
        MoveScene("StartMenuScene");
    }

    void MoveScene(string dstSceneName)
    {
        fade.SetBool("fade", true);
        StartCoroutine(WaitLoadScene(dstSceneName, sceneTransitionSec));
    }

    IEnumerator WaitLoadScene(string dstSceneName, float waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        SceneManager.LoadScene(dstSceneName);
    }

    int GetNil2Score(int nil2Type)
    {
        if      (nil2Type == NIL2_TYPE_RED)     return nil2RedScore;
        else if (nil2Type == NIL2_TYPE_BLUE)    return nil2BlueScore;
        else if (nil2Type == NIL2_TYPE_GREEN)   return nil2GreenScore;
        else if (nil2Type == NIL2_TYPE_YELLOW)  return nil2YellowScore;
        else if (nil2Type == NIL2_TYPE_PURPLE)  return nil2PurpleScore;
        else if (nil2Type == NIL2_TYPE_ORANGE)  return nil2OrangeScore;
        else if (nil2Type == NIL2_TYPE_RAINBOW) return nil2RainbowScore;
        else if (nil2Type == NIL2_TYPE_WHITE)   return nil2WhiteScore;
        else if (nil2Type == NIL2_TYPE_GRAY)    return nil2GrayScore;
        else if (nil2Type == NIL2_TYPE_BLACK)   return nil2BlackScore;
        else return 0;
    }

    public int GetNil2TypeRandom()
    {
        var nil2TypeSeed = UnityEngine.Random.Range(0, 100);
        if      (nil2TypeSeed < nil2RedBorder)     return NIL2_TYPE_RED;
        else if (nil2TypeSeed < nil2BlueBorder)    return NIL2_TYPE_BLUE;
        else if (nil2TypeSeed < nil2GreenBorder)   return NIL2_TYPE_GREEN;
        else if (nil2TypeSeed < nil2YellowBorder)  return NIL2_TYPE_YELLOW;
        else if (nil2TypeSeed < nil2PurpleBorder)  return NIL2_TYPE_PURPLE;
        else if (nil2TypeSeed < nil2OrangeBorder)  return NIL2_TYPE_ORANGE;
        else if (nil2TypeSeed < nil2RainbowBorder) return NIL2_TYPE_RAINBOW;
        else if (nil2TypeSeed < nil2WhiteBorder)   return NIL2_TYPE_WHITE;
        else if (nil2TypeSeed < nil2GrayBorder)    return NIL2_TYPE_GRAY;
        else if (nil2TypeSeed < nil2BlueBorder)    return NIL2_TYPE_BLACK;
        else return NIL2_TYPE_NONE;
    }

    public bool IsAvailable()
    {
        return gameStatus == GAME_STATUS_STARTED;
    }

    public void OnNil2HitEvent(int nil2Type)
    {
        if (gameStatus != GAME_STATUS_STARTED)
        {
            return;
        }

        score += GetNil2Score(nil2Type);
        scoreText.text = score.ToString();
        sound.PlaySE(hitSE, 0.5f);
    }

    public void OnNil2DropEvent()
    {
        if (gameStatus != GAME_STATUS_STARTED)
        {
            return;
        }

        life--;
        lifeText.text = life.ToString();
        sound.PlaySE(dropSE);

        if (life <= 0)
        {
            FinishGame();
        }
    }
}
