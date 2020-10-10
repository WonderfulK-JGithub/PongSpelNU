using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject textIM;
    public GameObject textLM;

    public GameObject fuckYou;

    public Vector3[] posList;
    public float[] rotationList;
    public string[] randomQuotes;

    int bounceCount;


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(ballSpeed, ballSpeed);
        ljudgrej = FindObjectOfType<AudioManager>();
        
        transform.position = new Vector3(100, 100, 0);
       
        StartCoroutine(ResetBall(1));

        
    }

    // Update is called once per frame
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Paddel")
        {
           
            ljudgrej.ljud3.Play();
            rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
            SpawnRandomText();


            bounceCount++;
            if(bounceCount == 10)
            {
                
                bounceCount = 0;
                if(rb.velocity.x < 18f)
                {
                    SpawnImportantText("The ball is speeding up!", Color.gray);
                    rb.velocity = new Vector2(rb.velocity.x + Mathf.Sign(rb.velocity.x) * 2f, rb.velocity.y + Mathf.Sign(rb.velocity.y) * 1f);
                }
            }
            
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
            SpawnRandomText();
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
            bounceCount = 0;
        }
        
        
       
    }

    IEnumerator WinScreen(string whoWon)
    {
        yield return new WaitForSeconds(4);

        winText.enabled = true;
        winText.text = whoWon + winText.text;

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(0);
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
                scoreScript.text1.enabled = false;
                scoreScript.text2.enabled = false;
            }
            else
            {
                if (!ljudgrej.ljud2.isPlaying)
                {
                    ljudgrej.ljud2.Play();
                }

                if (scoreScript.score1 > scoreScript.score2)
                {
                    if(scoreScript.text2.color != Color.red)
                    {
                        scoreScript.text2.color = Color.red;
                        SpawnImportantText("Player 1 is way ahead!", Color.red);
                    }
                   
                }
                else
                {
                    if (scoreScript.text1.color != Color.red)
                    {
                        scoreScript.text1.color = Color.red;
                        SpawnImportantText("Player 2 is way ahead!", Color.red);
                    }
                        
                }
            }
            

        }
        else
        {
            if (ljudgrej.ljud2.isPlaying) ljudgrej.ljud2.Stop();
            if(dif > 3)
            {
                scoreScript.text2.color = Color.white;
                
            }
            else if(dif < -3)
            {
                
                scoreScript.text1.color = Color.white;
            }
            else if(dif == 0)
            {
                SpawnImportantText("The game is even", Color.gray);
            }
        }

        
    }

    void SpawnImportantText(string text, Color color)
    {
        
        GameObject hej = Instantiate(textIM,Vector3.zero,Quaternion.identity);
        hej.transform.SetParent(fuckYou.transform);
        LifeTime script = hej.GetComponent<LifeTime>();

        script.SetPosition(new Vector3(0f + fuckYou.transform.position.x, 0f + fuckYou.transform.position.y, 0f));


        //INTE NICE MEN JAG HAR KVAR DET ÄNDÅ
        
        script.SetText(text);
        script.SetColor(color);
        script.SetLifeTime(240f);
       
        


        
    }

    void SpawnRandomText()
    {
        GameObject hej = Instantiate(textLM, Vector3.zero, Quaternion.identity);
        hej.transform.SetParent(fuckYou.transform);
        LifeTime script = hej.GetComponent<LifeTime>();

        int index = Random.Range(0, posList.Length);
        Vector3 extra = posList[index];

        script.SetPosition(new Vector3(0f + fuckYou.transform.position.x, 0f + fuckYou.transform.position.y, 0f) + extra);

        index = Random.Range(0, rotationList.Length);
        float rotation = rotationList[index];
        script.SetRotation(Quaternion.Euler(0f, 0f, rotation));

        index = Random.Range(0, randomQuotes.Length);
        script.SetText(randomQuotes[index]);

        script.SetLifeTime(70f);
    }
}
