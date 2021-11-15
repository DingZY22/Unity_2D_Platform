using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private enum TresureBoxState { Closed, Opened };
    [SerializeField] TresureBoxState state;
    private Animator anim;
    private bool openByPlayer;

    public GameObject Treasure;
    public int numOfTreasure;


    // Start is called before the first frame update
    void Start()
    {
        state = TresureBoxState.Closed;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Open();
    }

    void Open()
    {
        if (openByPlayer && state == TresureBoxState.Closed && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Opening");
            state = TresureBoxState.Opened;
            Invoke("dropTreasure", 0.5f);
        }
    }

    void dropTreasure()
    {
        Vector2 dropPos = new Vector2(transform.position.x, transform.position.y);
        for (int i = 0; i < numOfTreasure; i++)
        {
            dropPos.x = dropPos.x + Random.Range(-2.0f, 2.0f);
            Instantiate(Treasure, dropPos, Quaternion.identity);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            openByPlayer = true;
        }        
    }

}
