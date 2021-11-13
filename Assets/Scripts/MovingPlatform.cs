using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform[] movingPos;
    public float movingSpeed;
    public float waitTime;

    //pointer
    private int i;
    private Transform playerDefaultTransform;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        playerDefaultTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        moving();


    }

    void moving()
    {
        this.transform.position = Vector2.MoveTowards
            (this.transform.position, movingPos[i].position, movingSpeed * Time.deltaTime);
        if (Vector2.Distance(this.transform.position, movingPos[i].position) < 0.005f)
        {
            if (waitTime < 0.0f)
            {
                if (i == 0) { i = 1; }
                else if (i == 1) { i = 0; }

                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D standObject)
    {

        if (standObject.CompareTag("Player") && standObject.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            standObject.gameObject.transform.parent = this.gameObject.transform;
        }        
    }

    private void OnTriggerExit2D(Collider2D standObject)
    {
        if (standObject.CompareTag("Player") && standObject.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            standObject.gameObject.transform.parent = playerDefaultTransform;
        }
    }
}
