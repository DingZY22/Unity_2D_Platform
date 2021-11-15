using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRange : MonoBehaviour
{
    public int damgae;
    public float destroyTime;
    private PlayerHealth ph;

    // Start is called before the first frame update
    void Start()
    {
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") &&
           other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (ph != null)
            {
                Debug.Log("Hit");
                ph.DamagePlayer(this.damgae);
            }
        }

        else if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(this.damgae);
        }
    }
}
