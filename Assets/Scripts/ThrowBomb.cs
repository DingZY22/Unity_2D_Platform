using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    public GameObject bomb;
    public static bool canThrow;
    
    // Start is called before the first frame update
    void Start()
    {
        canThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && canThrow)
        {

            Instantiate(bomb, transform.position, transform.rotation);
            canThrow = false;
        }
    }
}
