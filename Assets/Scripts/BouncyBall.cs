using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;           // Minimum Y before reset
    public float maxVelocity = 10f;      // Max allowed velocity


    Rigidbody2D rb;

    public LevelGenerator levelScript;

    public int score = 0;
    public int lives = 5;
    public TextMeshProUGUI livestext;
    public TextMeshProUGUI brickText;
    public int BricksHitCount;

    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI countdownTxt;

    public GameObject[] livesImages;

    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public GameObject playerStats;
    public GameObject timer;
    public GameObject position;
    int brickCount;

    public AudioSource blockPop;
    public AudioSource deadSound;
    public AudioSource backgroundMusic;
    public AudioSource failSound;
    public AudioSource winSound;
    private int start = 0;

    // Called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        brickCount = FindFirstObjectByType<LevelGenerator>().transform.childCount;
        
    }

    // Called once per frame
    void Update()
    {
        // Reset position if the ball falls below minY
        if (transform.position.y < minY)
        {
            if ((lives <= 0))
            {
                Debug.Log("called");
                failSound.Play();
                GameOver();
            }
            else
            {
                
                StartCoroutine(startBall());
                if (!deadSound.isPlaying)
                {
                    deadSound.Play();
                }
                transform.position = Vector3.zero;
                lives--;
                livestext.text = lives.ToString();
                livesImages[lives].SetActive(false);
            }
        }

        // Clamp the velocity if it exceeds the maximum
        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
        }

        bool anyKey = Input.anyKey;
            
            if (anyKey && start == 0)
            {
                
                StartCoroutine(startBall());
            }else if (start <= 0)
            {
                transform.position = Vector3.zero;
            }
        

    }

    

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score += 10;
            scoreTxt.text = score.ToString("00000");
            BricksHitCount++;
            brickText.text = BricksHitCount.ToString();
            brickCount--;

            if (!blockPop.isPlaying)
            {
                blockPop.Play();
                
            }
            if (brickCount <= 0)
            {
                playerStats.SetActive(true);
                youWinPanel.SetActive(true);
                timer.transform.position = position.transform.position;
                Destroy(backgroundMusic);
                winSound.Play();
                levelScript.StartCoroutine(levelScript.countdownending());
                Time.timeScale = 0;
                
            }
        }
    }

    private void GameOver()
    {
        playerStats.SetActive(true);
        gameOverPanel.SetActive(true);
        timer.transform.position = position.transform.position;
        levelScript.StartCoroutine(levelScript.countdownending());
        Time.timeScale = 0;
        Destroy(gameObject);
        Destroy(backgroundMusic);
        


    }
    public IEnumerator startBall()
    {
        start++;
        int i = 3;
        maxVelocity = 0;
        while (i != 0)
        {
            countdownTxt.text = i.ToString();
            i--;
            
            yield return new WaitForSeconds(1);
        }
        maxVelocity = 10;
        rb.linearVelocity = Vector2.down * 10f;
        countdownTxt.text = "".ToString();
        
    }



}
