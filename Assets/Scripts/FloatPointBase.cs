using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPointBase : MonoBehaviour
{
    public float destoryTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
