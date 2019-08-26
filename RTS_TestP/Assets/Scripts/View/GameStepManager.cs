using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStepManager : MonoBehaviour
{ 

    private BaseScript baseScriptPlayer;

    private void Start()
    {
        baseScriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseScript>();
        StartCoroutine(GameStep());
    }

    IEnumerator GameStep()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(4);
            baseScriptPlayer.GameStep();
        }
    }


}
