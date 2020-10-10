using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddel : MonoBehaviour
{
    public KeyCode nerPil;
    public KeyCode uppPil;
   
    private float yLimit = 3.69f;
    
    private Rigidbody2D rb;
    [SerializeField,Range(0f,100f)]
    public float spd = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float direction = 0;
        if(Input.GetKey(nerPil))
        {
            direction -= 1;
        }
        if (Input.GetKey(uppPil))
        {
            direction += 1;
        }
        rb.velocity = new Vector2(0,direction * spd);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -yLimit, yLimit), 0);
    }
}
