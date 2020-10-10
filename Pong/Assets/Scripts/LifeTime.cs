using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeTime : MonoBehaviour
{
    public float lifeTime;
    Text thisText;
    RectTransform thisObject;
    private void Awake()
    {
        thisText = GetComponent<Text>();
        thisObject = GetComponent<RectTransform>();

        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= 1;
        if (lifeTime <= 0) Object.Destroy(gameObject);
        print("hej");
    }

    public void SetText(string text)
    {
        thisText.text = text;
    }
    public void SetLifeTime(float lifetime)
    {
        lifeTime = lifetime;
    }
    public void SetColor(Color color)
    {
        thisText.color = color;
    }
    public void SetPosition(Vector3 position)
    {
        thisObject.position = position;
    }
    public void SetRotation(Quaternion rotation)
    {
        thisObject.rotation = rotation;
    }
    
}
