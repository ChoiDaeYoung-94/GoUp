#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject PGround;
    public GameObject PGround50;
    public GameObject PGround100;
    public GameObject PBomb;
    public GameObject PBomb1;
    public GameObject PPortion;

    private float TotalHp;
    private float NowHp;
    public Transform HpBarImage;

    CameraController CameraUp;

    private int Score = 0;
    public TextMesh ScoreText;

    public bool Ready = true;
    public bool End = false;

    public GameObject Camera;
    public GameObject Player;
    public GameObject ReadyImage1;
    public GameObject ReadyImage2;

    public GameObject GameOverImage;
    public GameObject FinalWindow;
    public GameObject ScoreNew;
    public GameObject RestartB;

    public TextMesh FinalScoreText;
    public TextMesh BestScoreText;

    static float a = 0.25f;

    AudioSource audio;
    public AudioClip Deaths;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(700, 1280, true);
    }

    void Start()
    {
        Score = 0;
        SetScore();
        SetHp(100);
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.anyKeyDown && Ready == true)
        {
            Ready = false;
            CameraController.Start = true;
            iTween.FadeTo(ReadyImage1, iTween.Hash("alpha", 0, "time", 0.5f));
            iTween.FadeTo(ReadyImage2, iTween.Hash("alpha", 0, "time", 0.5f));
            InvokeRepeating("MakeGround", 0, 2.5f);
            InvokeRepeating("MakeGround50", 50, 1.5f);
            InvokeRepeating("MakeGround100", 100, 1);
            InvokeRepeating("MakeBomb", 0, 1);
            InvokeRepeating("MakeBomb1", 0, 1);
            InvokeRepeating("MakePortion", 0, 1);
            InvokeRepeating("GetScore", 0, 1);
            InvokeRepeating("HPdown", 0, 1);
            Player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        if (End == true)
            return;
        End = true;

        audio.PlayOneShot(Deaths, 0.7F);
        Player.SetActive(false);
        RestartB.SetActive(true);
        CameraController.Start = false;
        CancelInvoke("MakeBomb");
        CancelInvoke("MakeBomb1");
        CancelInvoke("MakePortion");
        CancelInvoke("GetScore");
        CancelInvoke("HPdown");
        iTween.ShakePosition(gameObject, iTween.Hash("x", 0.2, "y", 0.2, "time", 0.5));
        iTween.FadeTo(GameOverImage, iTween.Hash("alpha", 255, "deley", 1, "time", 0.5f));
        iTween.FadeTo(FinalWindow, iTween.Hash("alpha", 255, "deley", 1, "time", 0.5f));

        if (Score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", Score);
            ScoreNew.SetActive(true);
        }
        else if (Score <= PlayerPrefs.GetInt("BestScore"))
        {
            ScoreNew.SetActive(false);
        }
        FinalScoreText.text = Score.ToString();
        BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
    }


    public void SetHp(int hp)
    {
        NowHp = TotalHp = hp;
    }

    public void Hit(float damage)
    {
        NowHp -= damage;

        if (NowHp > 100)
        {
            NowHp = 100;
        }
        if (NowHp <= 0)
        {
            NowHp = 0;
            GameOver();
        }

        HpBarImage.transform.localScale = new Vector3((NowHp / TotalHp), 1, 1);
    }

    public void GetScore()
    {
        Score += 1;
        SetScore();
    }

    public void PortionScore()
    {
        Score += 10;
        SetScore();
    }

    public void SetScore()
    {
        ScoreText.text = "Score " + Score.ToString();
    }

    void MakeGround()
    {
        Instantiate(PGround);
        if (Score > 50)
            CancelInvoke("MakeGround");
    }

    void MakeGround50()
    {
        Instantiate(PGround50);
        if (Score > 150)
            CancelInvoke("MakeGround50");
    }

    void MakeGround100()
    {
        Instantiate(PGround100);
        if (Score > 200)
            CancelInvoke("MakeGround100");
    }

    void MakeBomb()
    {
        Instantiate(PBomb);
    }

    void MakeBomb1()
    {
        Instantiate(PBomb1);
    }

    void MakePortion()
    {
        Instantiate(PPortion);
    }

    void HPdown()
    {
        Hit(0.5f);
    }
}
