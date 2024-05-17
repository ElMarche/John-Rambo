using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject John; // John will be passed from the Unity editor
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // checking if there is a John
        if (John != null)
        {
            Vector3 position = transform.position;
            position.x = John.transform.position.x;
            transform.position = position;
        }
        
    }
}
