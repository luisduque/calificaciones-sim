using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    public bool hasAnObject = false;
    public GameObject objectInside = null;

    public void SetValue(bool value)
    {
        hasAnObject = value;
    }

    public bool GetValue()
    {
        return hasAnObject;
    }

    public GameObject GetObjectInside()
    {
        return objectInside;
    }

    public void SetObjectInside(GameObject objeto)
    {
        objectInside = objeto;
    }

    private void Update()
    {
        if(objectInside != null)
        {
            if(objectInside.GetComponent<RectTransform>().anchoredPosition != GetComponent<RectTransform>().anchoredPosition)
            {
                objectInside = null;
                hasAnObject = false;
            }
        }
        else
        {
            hasAnObject = false;
        }
    }
}
