using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashBinCoin : MonoBehaviour
{
    public Text coinText;
    public static int currentCoin;
    public static int maxCoin;

    private Image trashBinBar;
    
    // Start is called before the first frame update
    void Start()
    {
        trashBinBar = GetComponent<Image>();
        maxCoin = 20;
        currentCoin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        trashBinBar.fillAmount = (float)currentCoin / (float)maxCoin;
        coinText.text = currentCoin.ToString() + "/" + maxCoin.ToString();
    }
}
