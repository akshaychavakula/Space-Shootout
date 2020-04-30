using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogoDigital.Lipsync;

public class Speach : MonoBehaviour
{
    private LipSync ls;
    public LipSyncData[] lsAudio;
    // Sta[rt is called before the first frame update
    void Start()
    {
        ls = GameObject.Find("remy").GetComponent<LipSync>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ls)
        {
            ls = GameObject.Find("remy").GetComponent<LipSync>();
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
