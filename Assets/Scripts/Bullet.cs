using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] public float speed;
    [SerializeField] private CircleCollider2D cc2D;
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb2D.velocity = Vector2.right * speed;

        Destroy(gameObject,2.5f);
    }

    // prilicno sam siguran da je bolje za performansu da se privjerava samo u slucaju kolizije
    // void Update()
    // {
    //     if (cc2D.IsTouchingLayers(ground_layer)){
    //         Destroy(gameObject);
    //     }
    // }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<EnemyHandler>()){
            other.gameObject.GetComponent<EnemyHandler>().increment_health(-damage);
            Destroy(gameObject);
        }
        if (cc2D.IsTouchingLayers(ground_layer)){
            Destroy(gameObject);
        }
    }
}
