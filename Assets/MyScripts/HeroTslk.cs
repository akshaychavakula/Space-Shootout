using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTslk : MonoBehaviour
{
    private float timer = 0.0f;
    public AudioSource audioPlayer;
    public AudioClip sound;
    private bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
         Debug.Log(timer.ToString());
        if(timer > 15 && played == false)
        {
            audioPlayer.clip = sound;
            audioPlayer.Play();
            played = true;
        }
        
    }
}
