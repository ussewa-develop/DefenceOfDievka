using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptOrderLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> scriptList;

    public static event Action AllScriptsLoaded;

    private void Start()
    {
        foreach (GameObject script in scriptList)
        {
            script.GetComponent<IStaticScript>().Inst();
        }
        AllScriptsLoaded?.Invoke();
    }
}
