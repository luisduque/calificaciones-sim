using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUnity : MonoBehaviour
{
    public void CloseApp()
    {
        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

        #endif

        Application.Quit();
    }
}
