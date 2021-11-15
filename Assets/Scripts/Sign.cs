using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public Image dialogueBox;
    public Text dialogueText;
    public string signText;
    
    private bool isTouched;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        showDialogue();
    }

    private void showDialogue()
    {
        if (isTouched && Input.GetKeyDown(KeyCode.E))
        {
            dialogueText.text = signText;
            dialogueBox.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isTouched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isTouched = false;
            dialogueBox.gameObject.SetActive(false);
        }
    }
}
