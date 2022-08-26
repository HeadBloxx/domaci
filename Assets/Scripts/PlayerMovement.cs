using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// da koristim snake_case
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float move_speed;
    [SerializeField] private float jump_strength;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private CapsuleCollider2D cc2D;
    [SerializeField] private BoxCollider2D ground_collider;
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bullet_prefab;
    [SerializeField] private GameObject fire_point; 
    [SerializeField] private GameObject bullet_container;
    [SerializeField] private float fire_rate;
    [SerializeField] private float climbing_speed;
    [SerializeField] private LayerMask climbing_layer;
    private float next_time;
    private float dirX = 0;
    private float dirY = 0;
    private float _starting_gravity_scale = 0;

    private void Start() {
         _starting_gravity_scale = rb2D.gravityScale;
    }
    private void Update() {
        Jump();
        fire();
    }
    private void FixedUpdate() {
        Move();
        Climb();
        flip();
    }

    private void Move(){
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");
        animate();
        Vector2 velocity = new Vector2(dirX*move_speed*Time.deltaTime,rb2D.velocity.y);
        rb2D.velocity = velocity;
        
    }
    private void Jump(){
        if (Input.GetButtonDown("Jump") && ground_collider.IsTouchingLayers(ground_layer))
        {
            Debug.Log(2);
            Vector2 velocity = new Vector2(0,jump_strength);
            rb2D.velocity = velocity;
        }
    }
    private void animate(){
        if (Mathf.Abs(dirX)>0)
        {
            animator.SetBool("is_running",true);
            return;
        }
        animator.SetBool("is_running",false);
    }
    private void flip(){
        bool is_player_moving = Mathf.Abs(rb2D.velocity.x)>Mathf.Epsilon;
        if (is_player_moving && rb2D.velocity.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2D.velocity.x),transform.localScale.y);
        }
    }
    private void fire(){
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > next_time)
        {
            next_time = Time.time + fire_rate;
            animator.SetTrigger("fire");
            GameObject bullet = Instantiate(bullet_prefab,fire_point.transform.position,fire_point.transform.rotation);
            bullet.transform.SetParent(bullet_container.transform);
            bullet.GetComponent<Bullet>().speed*=transform.localScale.x;
        }
    }

    private void Climb(){
        if (!cc2D.IsTouchingLayers(climbing_layer)){return;}else{rb2D.gravityScale = _starting_gravity_scale;}
        Vector2 climb_velocity = new Vector2(rb2D.velocity.x,dirY * climbing_speed);
        rb2D.velocity = climb_velocity;
        rb2D.gravityScale = 0;
    }
}
