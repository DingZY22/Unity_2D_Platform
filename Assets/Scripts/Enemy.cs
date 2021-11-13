using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public GameObject bloodEffect;

    public float flashTime;

    public SpriteRenderer sr;
    public Color originalColor;

    private PlayerHealth ph;
    

    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    public void Update()
    {
        Death();
    }

    void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int d)
    {
        health -= d;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        //GameController.camShake.Shake();
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    private void ResetColor()
    {
        sr.color = originalColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&other.GetType().ToString()=="UnityEngine.CapsuleCollider2D")
        {
            if (ph != null) 
            {
                ph.DamagePlayer(this.damage);
            }
            
        }
        
    }

}
