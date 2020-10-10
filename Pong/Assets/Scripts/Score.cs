using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score1 = 0;
    public int score2 = 0;

    public Text text1;
    public Text text2;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text1.text = score1 + "";
        text2.text = score2 + "";
    }
}
