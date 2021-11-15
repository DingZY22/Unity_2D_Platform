using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public float flyingSpeed;
    public int damage;
    public float rotateSpeed;
    public float tuning;

    private Rigidbody2D rb2d;
    private Transform playerTransform;
    private Vector2 initialSpeed;
    private CameraShake camShake;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * flyingSpeed;
        initialSpeed = rb2d.velocity;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, rotateSpeed);
        float y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);
        transform.position = new Vector2(transform.position.x, y);
        rb2d.velocity = rb2d.velocity - initialSpeed * Time.deltaTime;

        if (Mathf.Abs(this.transform.position.x - playerTransform.position.x) < 0.5f)
        {
            Destroy(this.gameObject);
            SickleHit.canShoot = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(this.damage);
        }
    }

}
