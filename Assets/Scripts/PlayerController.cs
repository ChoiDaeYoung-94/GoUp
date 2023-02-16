#pragma warning disable 0108

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public float mInput = 0;

    public bool Jump = false;
    public bool DoubleJump = false;
    public bool LastJump = false;
    public bool ccoong = false;
    public bool MoveIn = true;

    public GameManager GM;

    AudioSource audio;
    public AudioClip HpUp;
    public AudioClip HpDown;

    public GameObject Player;
    public GameObject PlayerRed;
    public GameObject coong;

    int JumpC = 0;
    float PlayerRedCount = 0;
    float coongCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        Player.SetActive(true);
        PlayerRed.SetActive(false);
    }

    void Update()
    {

        if(Input.GetKey(KeyCode.Q))
        {
            Time.timeScale = 10;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Time.timeScale = 1;
        }

        move();
        Move(mInput);
        if (JumpC >= 4)
        {
            Player.SetActive(false);
            PlayerRed.SetActive(true);
            JumpC = 4;
            PlayerRedCount += Time.deltaTime;
        }
        else
        {
            Player.SetActive(true);
            PlayerRed.SetActive(false);
        }
        if (JumpC >= 4 && PlayerRedCount >= 5)
        {
            JumpC = 0;
            PlayerRedCount = 0;
            Player.SetActive(true);
            PlayerRed.SetActive(false);
        }
        if (ccoong == true)
        {
            coongCount += Time.deltaTime;
            if (coongCount >= 0.2)
            {
                coong.SetActive(false);
                coongCount = 0;
                ccoong = false;
            }
        }
    }

    void move()
    {
        float xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        this.transform.Translate(new Vector2(xMove, 0), Space.World);

        if (Jump == false && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, 4);
            Jump = true;
            DoubleJump = true;
            GetComponent<AudioSource>().Play();
            JumpC += 1;
        }
        else if (DoubleJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, 4);
            DoubleJump = false;
            LastJump = true;
            GetComponent<AudioSource>().Play();
        }
        else if (LastJump == true && Input.GetKeyDown(KeyCode.Space) && JumpC == 4)
        {
            rb.velocity = new Vector2(0, 5);
            LastJump = false;
            GetComponent<AudioSource>().Play();
            JumpC = 0;
        }
    }

    void Move(float horizontalInput)
    {
      //  if (!MoveIn)
        //    return;
        //Vector2 moveV = rb.velocity;
        //moveV.x = horizontalInput * 2.5f;

        //rb.velocity = moveV;

        float xMove = horizontalInput * speed * Time.deltaTime;
        this.transform.Translate(new Vector2(xMove, 0), Space.World);
    }

    public void MoveStart(float horizontalInput)
    {
        mInput = horizontalInput;
    }

    public void MobileJump()
    {
        if (Jump == false)
        {
            rb.velocity = new Vector2(0, 4);
            Jump = true;
            DoubleJump = true;
            GetComponent<AudioSource>().Play();
            JumpC += 1;
        }
        else if (DoubleJump == true)
        {
            rb.velocity = new Vector2(0, 4);
            DoubleJump = false;
            LastJump = true;
            GetComponent<AudioSource>().Play();
        }
        else if (LastJump == true && JumpC == 4)
        {
            rb.velocity = new Vector2(0, 5);
            LastJump = false;
            GetComponent<AudioSource>().Play();
            JumpC = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Jump = false;
        }
        else if (col.gameObject.tag == "Death")
        {
            GM.Hit(100);

            ccoong = true;
            coong.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bomb")
        {
            audio.PlayOneShot(HpDown, 0.7F);
            GM.Hit(10);

            ccoong = true;
            coong.SetActive(true);
        }
        else if (target.tag == "Portion")
        {
            audio.PlayOneShot(HpUp, 0.7F);
            GM.Hit(-5);
            GM.PortionScore();
        }
        else if (target.tag == "Finish")
        {
            GM.GameOver();
        }
    }
}
