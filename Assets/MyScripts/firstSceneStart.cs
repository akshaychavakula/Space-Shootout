using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class firstSceneStart : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
   ;        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 4)
        {
            SceneManager.LoadScene(1);
        }
    }
}
