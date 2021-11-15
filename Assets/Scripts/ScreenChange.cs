using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChange : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public float changeTime;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetBool("ChangeToWhite", true);
            Invoke("changeImage", changeTime);
        }
    }

    void changeImage()
    {
        image1.SetActive(true);
        image2.SetActive(false);
    }
}
