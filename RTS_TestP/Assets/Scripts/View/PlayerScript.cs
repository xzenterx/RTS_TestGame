using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public GameObject UIPlayer;


    private BaseScript baseScript;

    private void Awake()
    {
        baseScript = GetComponent<BaseScript>();
    }


}
