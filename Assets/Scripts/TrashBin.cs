using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private bool playerInTrashBin;
    
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (playerInTrashBin)
            {
                if (CoinUI.currentCoinQunatity > 0)
                {
                    AudioManager.ThrowPickCoin();
                    TrashBinCoin.currentCoin++;
                    CoinUI.currentCoinQunatity--;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
           
            playerInTrashBin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {

            playerInTrashBin = false;
        }
    }
}
