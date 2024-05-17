using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject John;
    private float LastShoot;
    public GameObject BulletPrefab;
    private int Health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       // Calculating for the enemy srpite to face the player
        if (John == null) return;
        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f) 
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        } else { transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }
        
        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);
        if (distance < 1.0f && Time.time > LastShoot + 0.35f)
        {
            Shoot();
            LastShoot = Time.time;
        }
        
    }

    private void Shoot()
    {
        //Debug.Log("Shooting");        
        Vector3 Shootdirection;
        if (transform.localScale.x == 1.0f) Shootdirection = Vector2.right;
        else Shootdirection = Vector2.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + Shootdirection * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(Shootdirection);
        
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
