using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogoDigital.Lipsync;
public class VHSpeech : MonoBehaviour
{
    public LipSync ls;
    public LipSyncData[] lsAudio;
    private float timer = 0.0f;
    private bool played = false;
    // Sta[rt is called before the first frame update
    void Start()
    {
        ls = GameObject.Find("remy").GetComponent<LipSync>();
        ls.defaultClip = lsAudio[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!ls)
        {
            ls = GameObject.Find("remy").GetComponent<LipSync>();
           
           // ls.Play(lsAudio[1]);
        }
        timer += Time.deltaTime;
        if(timer > 24 && played == false)
        {
            ls.Play(lsAudio[2]);
            played = true;
        }
      

    }

    public void sayHello()
    {
        ls.Play(lsAudio[0]);

    }

    public void howAreU()
    {
        ls.Play(lsAudio[1]);
    }

    public void itlab()
    {
        ls.Play(lsAudio[2]);
    }
}
