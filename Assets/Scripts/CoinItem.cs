using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            AudioManager.PlayPickCoin();
            CoinUI.currentCoinQunatity += 1;
            Destroy(this.gameObject);
        }
    }
}
