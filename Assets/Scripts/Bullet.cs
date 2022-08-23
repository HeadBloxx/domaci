using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb2D.velocity = Vector2.right * speed;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     //rb2D.velocity = new Vector2(speed,0);
    // }
}
