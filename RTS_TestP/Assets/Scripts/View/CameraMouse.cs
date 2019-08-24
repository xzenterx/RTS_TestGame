using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = new Vector3(Input.mousePosition.x * Time.deltaTime, Input.mousePosition.y * Time.deltaTime, -10);    
    }
}
