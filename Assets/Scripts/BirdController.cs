using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdController : MonoBehaviour
{
    public int score;
    float gameTimer;
    float preTimer;
    float postTimer;
    float timerDisplay;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    bool isPre = true;
    bool isGame = false;
    bool isPost = false;
    bool isOver = false;
    AudioSource audioSource;
    public GameObject ScoreUp;
    public GameObject ScoreDown;
    public AudioSource Music;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip bgm;
    public AudioClip intro;
    public TMP_Text ScoreText;
    public TMP_Text EndText;
    public TMP_Text Timer;
    public ParticleSystem bugGet;
    public ParticleSystem frisbeeHit;
    Vector3 mousePosition;


        // Start is called before the first frame update
        void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            score = 0;
            ScoreText.text = "Score: " + score.ToString();
            EndText.text = "";
            Timer.text ="";
            gameTimer = 10.0f;
            preTimer = 2.0f;
            postTimer = 2.0f;
            Music.clip = intro;
            Music.Play();
            Music.loop = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPre == true)
            {
                if(preTimer > 0.0f)
                {
                    EndText.text = "Use the mouse to control the bird and collect bugs! You need a score of 5 to win, bugs add a point, and frisbees take off a point.";
                    preTimer -= Time.deltaTime;
                    hide();
                    disable();
                    
                }
                if(preTimer <= 0.0f && gameTimer > 0.0f && isPost != true)
                {
                    EndText.text = "";
                    show();
                    enable();
                    Music.Stop();
                    Music.clip = bgm;
                    isPre = false;
                    isGame = true;
                    Music.Play();
                }
            }

            if (isGame == true)
            {
                if(gameTimer > 0.0f)
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
                    transform.position = mousePosition;
                    gameTimer -= Time.deltaTime;
                    timerDisplay = Mathf.FloorToInt (gameTimer % 60);
                    Timer.text = "Time: " + timerDisplay.ToString();
                    
                }
                if(gameTimer <= 0.0f && postTimer > 0.0f)
                {
                    Timer.text = "";
                    hide();
                    ScoreText.text = "";
                    isGame = false;
                    isPost = true;
                    Music.Stop();
                    if (score >= 5)
                    {
                        Music.clip = win;
                        Music.Play();
                    }
                    else
                    {
                        Music.clip = lose;
                        Music.Play();
                    }
                }
            }

            if (isPost == true)
            {
                if (postTimer > 0.0f)
                {
                    postTimer -= Time.deltaTime;
                    if (score >= 5)
                    {
                        EndText.text = "Congratulations! You Won!";
                    }
                    else
                    {
                        EndText.text = "Sorry! You Lost!";
                    }
                }
                if (postTimer <= 0.0f && preTimer <= 0.0f && gameTimer <= 0.0f)
                {
                    EndText.text = "Thanks for playing! Game by Joseph Donnelly. Press R to restart!";
                    isOver = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKey(KeyCode.R))
            {
                if (isOver == true)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene
                }
            }   
        }
    

        void FixedUpdate()
        {
         
        }

        public void ChangeScore(int difScore)
        {
            if(difScore == 1)
            {
                Instantiate(bugGet, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            }
            if(difScore != 1)
            {
                Instantiate(frisbeeHit, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            }
            score += difScore;
            ScoreText.text = "Score: " + score.ToString();
            
        }

        public void hide()
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }

        public void show()
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }

        public void disable()
        {
            ScoreUp.SetActive(false);
            ScoreDown.SetActive(false);
        }

        public void enable()
        {
            ScoreUp.SetActive(true);
            ScoreDown.SetActive(true);
        }
        
        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
}