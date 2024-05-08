using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float Speed;
    public float JumpForce;
    private bool Grounded;
    private Animator Animator;
    public GameObject BulletPrefab;
    private float LastShoot;
    private float LastJump;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>(); // get the component Rigidbody2D
        Animator = GetComponent<Animator>(); // get the component Animator from Unity
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal") * Speed; // GetAxisRaw to capture the movement on x Axis. (1,0 or -1)
        
        if(Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f,1.0f, 1.0f);
        else if(Horizontal > 0.0f) transform.localScale = new Vector3(1.0f,1.0f, 1.0f);
        
        Animator.SetBool("running", Horizontal != 0.0f); // Horizontal == 0 means false.

        //Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red); // draw a red ray same as the raycast
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f)) // Check if its gounded so john can jump
        {
            Grounded = true;
        } else Grounded = false;
        if (Input.GetKey(KeyCode.W) && Grounded && Time.time > LastJump + 0.05f) // Input.GetKey checks key being pressed & hold
        {
            Jump();
            LastJump = Time.time;
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();            
            LastShoot = Time.time;
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);

    }

    void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }
}
