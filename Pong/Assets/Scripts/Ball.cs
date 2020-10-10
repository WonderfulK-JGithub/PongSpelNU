using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float ballSpeed;

    public Score scoreScript;
    public AudioManager ljudgrej;
    public SpriteRenderer jumpScare;
    public GameObject world;
    public Text winText;
    bool gameHasEnded = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(ballSpeed, ballSpeed);
        ljudgrej = FindObjectOfType<AudioManager>();


    }

    // Update is called once per frame
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Paddel")
        {
            rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
            ljudgrej.ljud3.Play();
        }
        else if(collision.tag == "Goal")
        {
            if(Mathf.Sign(rb.velocity.x) == 1)
            {
                scoreScript.score1++;
                
            }
            else
            {
                scoreScript.score2++;
            }
            CheckScore();
            StartCoroutine(ResetBall(2));
            
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);
            ljudgrej.ljud3.Play();
        }
        
        
    }

    IEnumerator ResetBall(float time)
    {
        yield return new WaitForSeconds(time);
        if(!gameHasEnded)
        {
            float[] newV = { ballSpeed, -ballSpeed };
            transform.position = new Vector3(0, 0, 0);
            int randomindex = Random.Range(0, 2);
            rb.velocity = new Vector2(newV[randomindex], newV[randomindex]);
        }
        
        
       
    }

    IEnumerator WinScreen(string whoWon)
    {
        yield return new WaitForSeconds(1);

        winText.enabled = true;
        winText.text = whoWon + winText.text;
    }

    void CheckScore()
    {
        float dif = scoreScript.score1 - scoreScript.score2;
        if (dif > 4 || dif < -4)
        {
            if(dif > 9 || dif < -9)
            {
                gameHasEnded = true;
                ljudgrej.ljud2.Stop();
                ljudgrej.ljud1.Play();
                jumpScare.enabled = true;
                world.SetActive(false);
                if (scoreScript.score1 > scoreScript.score2)
                {
                    StartCoroutine(WinScreen("Player 1"));
                }
                else
                {
                    StartCoroutine(WinScreen("Player 2"));
                }
                scoreScript.text1.text = "";
                scoreScript.text2.text = "";
            }
            else
            {
                if (!ljudgrej.ljud2.isPlaying)
                {
                    ljudgrej.ljud2.Play();
                }

                if (scoreScript.score1 > scoreScript.score2)
                {
                    scoreScript.text2.color = Color.red;
                }
                else
                {
                    scoreScript.text1.color = Color.red;
                }
            }
            

        }
        else
        {
            if (ljudgrej.ljud2.isPlaying) ljudgrej.ljud2.Stop();
        }

        
    }
}
