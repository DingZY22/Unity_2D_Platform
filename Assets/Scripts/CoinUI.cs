using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int initialCoinQuantity;
    public static int currentCoinQunatity;
    public Text coinQuantity;

    // Start is called before the first frame update
    void Start()
    {
        currentCoinQunatity = initialCoinQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        coinQuantity.text = currentCoinQunatity.ToString();
    }
}
