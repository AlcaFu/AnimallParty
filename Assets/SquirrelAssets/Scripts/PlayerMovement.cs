using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool Grounded = true;
    private bool firstJump = true;
    public bool isDead = false;

    private float fallMultiplier = 2f;
    private float previousYPos = -1000;

    public GameObject gameOverScreen;
    private char lastJump = 'N';
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI _highscoreText;
    private int currentScore = 0;

    void Start()
    {
        Time.timeScale = 2f;
        rb = GetComponent<Rigidbody2D>();
        _highscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();

        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
                SquirrelAudioManager._instance.PlayMusic();
        }
        else
        {
            SquirrelAudioManager._instance._music.Stop();
        }


        if (PlayerPrefs.GetInt("SfxEnabled", 1) == 1)
        {
            SquirrelAudioManager._instance._sfx.mute = false ;
        }
        else
        {
            SquirrelAudioManager._instance._sfx.mute = true;
        }
    }

    void Update()
    {
        if (transform.position.y + 5 < previousYPos)
        {
            if (!isDead)
                Death();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Jump(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Jump(false);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
    }

    public void Jump(bool smallJump)
    {
        firstJump = false;
        if (!Grounded)
            return;

        Grounded = false;

        if (smallJump)
        {
            SquirrelAudioManager._instance.PlaySFX(0);
            lastJump = 'S';
            rb.AddForce(new Vector2(9.8f * 19.4f, 9.8f * 24.7f));
        }
        else
        {
            lastJump = 'B';
            StartCoroutine(longJump());
            SquirrelAudioManager._instance.PlaySFX(0);
        }
    }

    IEnumerator longJump()
    {
        rb.AddForce(new Vector2(0, 9.8f * 32.5f));

        yield return new WaitForSeconds(0.15f);

        rb.AddForce(new Vector2(9.8f * 17.4f, 0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;

        if (lastJump == 'S' && collision.gameObject.tag == "smallTile")
        {

        }
        else if (lastJump == 'B' && collision.gameObject.tag == "bigTile")
        {

        }
        else if (lastJump == 'N')
        {

        }
        else
        {
            Debug.Log("Wrong Jump");
            Death();
            return;
        }

        if (collision.gameObject.tag.Contains("Tile"))
        {

            if (int.TryParse(scoreText.text, out currentScore))
            {
                currentScore += 1;
                scoreText.text = currentScore.ToString();
                
                if (currentScore > PlayerPrefs.GetInt("Highscore", 0))
                {
                    PlayerPrefs.SetInt("Highscore", currentScore);
                    PlayerPrefs.Save();
                }
            }

            previousYPos = transform.position.y;

            Grounded = true;
            rb.velocity = Vector2.zero;
            transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);

            if (firstJump)
                return;
            StartCoroutine(fallTile(collision.gameObject));
        }
    }

    IEnumerator fallTile(GameObject collision)
    {
        yield return new WaitForSeconds(1f);
        if (collision.gameObject != null)
        {
            collision.AddComponent<Rigidbody2D>();
        }

    }

    void Death()
    {
        SquirrelAudioManager._instance.PlaySFX(1);
        SquirrelAudioManager._instance._music.Stop();
        Debug.Log("Death");

        gameOverScreen.SetActive(true);
        gameOverText.text = $"{currentScore}";

        _highscoreText.text = $"Highscore: {PlayerPrefs.GetInt("Highscore", 0)}";
        isDead = true;
    }
}
