using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int spikeDamage;
    private PlayerHealth ph;

    // Start is called before the first frame update
    void Start()
    {
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString()== "UnityEngine.PolygonCollider2D")
        {
            ph.DamagePlayer(this.spikeDamage);
        }
    }
}
