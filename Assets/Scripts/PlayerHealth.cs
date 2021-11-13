using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    private Renderer myRenderer;
    private Animator myAnim;
    private ScreenFlash sf;
    private Rigidbody2D self2D;
    private PolygonCollider2D polygonCollider2D;

    public int blinks;
    public float blinkTime;
    public float dieDelay;
    public float HitBoxCDTime;
    void Start()
    {
        HealthBar.maxHealth = health;
        HealthBar.currentHealth = health;
        myRenderer = GetComponent<Renderer>();
        myAnim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        self2D = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int d)
    {
        sf.FlashScreen();
        health -= d;
        if (health < 0)
        {
            health = 0;
        }
        HealthBar.currentHealth = health;
        if (health <= 0)
        {
            self2D.velocity = new Vector2(0, 0);
            self2D.gravityScale = 0.0f;
            myAnim.SetTrigger("Die");
            Invoke("Die", dieDelay);
        }

        BlinkPlayer(this.blinks, this.blinkTime);
        polygonCollider2D.enabled = false;
        StartCoroutine(EnablePlayerHitBox());
    }

    IEnumerator EnablePlayerHitBox()
    {
        yield return new WaitForSeconds(this.HitBoxCDTime);
        polygonCollider2D.enabled = true;
    }



    void Die()
    {
        Destroy(this.gameObject);
    }


    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            myRenderer.enabled = !myRenderer.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRenderer.enabled = true;
    }
}
