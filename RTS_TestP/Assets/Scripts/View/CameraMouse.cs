using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{   

    private bool fixCamera = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fixCamera = !fixCamera;
        }
    }

    private void FixedUpdate()
    {
        if(!fixCamera)
            transform.position = new Vector3(Input.mousePosition.x * Time.deltaTime, Input.mousePosition.y * Time.deltaTime, -10);    
    }
}
