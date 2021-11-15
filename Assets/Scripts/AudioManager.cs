using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip pickCoin;
    public static AudioClip throwCoin;
    public static AudioSource audioSrc;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pickCoin = Resources.Load<AudioClip>("PickCoin");
        throwCoin = Resources.Load<AudioClip>("ThrowCoin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayPickCoin()
    {
        audioSrc.PlayOneShot(pickCoin);
    }
    public static void ThrowPickCoin()
    {
        audioSrc.PlayOneShot(throwCoin);
    }


}
