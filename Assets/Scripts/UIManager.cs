using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RectTransform UI_Element;
    public RectTransform CanvasRec;
    public Transform trashBinPos;
    public float xOffset;
    public float yOffset;
    public Text coinNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 viewortPos = Camera.main.WorldToViewportPoint(trashBinPos.position);
        Vector2 worldObjectScreenPos = new Vector2((viewortPos.x * CanvasRec.sizeDelta.x) -
                                                   (CanvasRec.sizeDelta.x * 0.5f) + xOffset,
                                                   (viewortPos.y * CanvasRec.sizeDelta.y) -
                                                   (CanvasRec.sizeDelta.y * 0.5f) + yOffset);
        UI_Element.anchoredPosition = worldObjectScreenPos;
    }
}
