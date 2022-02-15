using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAprobado : MonoBehaviour
{
    public GameObject aprovadoObject;
    public GameObject reprobadoObject;
    public void Aprobado(bool value)
    {
        if (value)
        {
            aprovadoObject.SetActive(true);
            reprobadoObject.SetActive(false);
        }
        else
        {
            aprovadoObject.SetActive(false);
            reprobadoObject.SetActive(true);
        }
    }
}
