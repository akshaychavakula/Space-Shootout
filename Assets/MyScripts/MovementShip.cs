using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementShip : MonoBehaviour
{
    public float speed = 500;
    private float timer = 0.0f;
    public AudioSource aud;
    public AudioClip swoosh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>= 3){
            aud.clip = swoosh;
            aud.Play();
        }

        if (timer >= 6f)
        {

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }
        if (timer >= 7)
        {
            SceneManager.LoadScene(3);
        }


        Debug.Log(timer.ToString());
        
    }
}
