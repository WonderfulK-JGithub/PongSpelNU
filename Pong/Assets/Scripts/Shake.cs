using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    Vector3 startPosition;
    public float magnitude;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + new Vector3(Random.Range(-1f,1f) * magnitude, Random.Range(-1f, 1f) * magnitude,0f);
    }
}
