using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Vector2 initialSpeed;
    public Rigidbody2D rb2D;
    public Animator anim;
    public ExplosionRange explosionRange;
    public float delayExplodeTime;
    public float destroyBoomTime;
    public float hitBoxTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb2D.velocity = this.transform.right * initialSpeed.x + this.transform.up * initialSpeed.y;
        Invoke("Explode", delayExplodeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        anim.SetTrigger("Explode");
        Invoke("GenExplosionRan", hitBoxTime);
        Invoke("Destroy", destroyBoomTime);
        
    }

    void GenExplosionRan()
    {
        Vector3 initPos = new Vector3(transform.position.x, transform.position.y - 0.6f, 0.0f);
        Instantiate(explosionRange, initPos, Quaternion.identity);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
        ThrowBomb.canThrow = true;
    }
}
