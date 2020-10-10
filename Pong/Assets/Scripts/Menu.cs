using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text[] texts;
    public Text title;
    int menuCount = 0;
    float redValue = 0;
    bool redValueUp = true;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            menuCount--;
            if (menuCount < 0) menuCount = texts.Length - 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            menuCount++;
            if (menuCount > texts.Length - 1) menuCount = 0;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            if (i == menuCount) texts[i].color = Color.green;
            else texts[i].color = Color.white;
        }

        if(redValueUp)
        {
            redValue += 0.5f / 60f;
            if (redValue >= 0.9f) redValueUp = false;
        }
        else
        {
            redValue -= 0.5f / 60f;
            if (redValue <= 0.1f) redValueUp = true;
        }
        title.color = new Color(redValue, 0f, 0f);
    }
}
