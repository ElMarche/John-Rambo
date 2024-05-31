using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float Speed; // public can be worked on from Unity editor.
    private Vector2 Direction;
    public AudioClip Sound;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnMovement John = collision.GetComponent<JohnMovement>();
        GruntScript Grunt = collision.GetComponent<GruntScript>();

        if (John != null) John.Hit();
        if (Grunt != null) Grunt.Hit();

        DestroyBullet();
    }
}
