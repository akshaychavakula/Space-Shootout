using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float virticleSpeed = 2.0f;
    public float horizontalSpeed = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= virticleSpeed * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
