using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text[] texts;
    public Text title;
    int menuCount = 0;
    float redValue = 0;
    bool redValueUp = true;
    public AudioSource snSelect;
    public AudioSource snMove;
    public AudioSource theme;
    public GameObject info;
    public GameObject panel;

    bool menuControll = true;
   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(menuControll)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                menuCount--;
                if (menuCount < 0) menuCount = texts.Length - 1;
                snMove.Play();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                menuCount++;
                if (menuCount > texts.Length - 1) menuCount = 0;
                snMove.Play();
            }

            for (int i = 0; i < texts.Length; i++)
            {
                if (i == menuCount) texts[i].color = Color.green;
                else texts[i].color = Color.white;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                menuControll = false;
                switch (texts[menuCount].text)
                {
                    case "Play":
                        StartCoroutine(LoadGame());
                        break;
                    case "Quit":
                        Application.Quit();
                        break;
                    case "Bonus content":
                        print("lol");
                        theme.Stop();
                        panel.SetActive(true);
                        break;
                }
            }




            if (redValueUp)
            {
                redValue += 0.25f / 60f;
                if (redValue >= 0.9f) redValueUp = false;
            }
            else
            {
                redValue -= 0.25f / 60f;
                if (redValue <= 0.1f) redValueUp = true;
            }
            title.color = new Color(redValue, 0f, 0f);
        }
        
    }

    IEnumerator LoadGame()
    {
        title.enabled = false;
        foreach (var item in texts)
        {
            item.enabled = false;
        }
        info.SetActive(true);
        yield return new WaitForSeconds(4);
       
        SceneManager.LoadScene(1);
        

    }
}
