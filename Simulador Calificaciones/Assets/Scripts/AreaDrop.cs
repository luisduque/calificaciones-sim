using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AreaDrop : MonoBehaviour, IDropHandler
{
    public GameObject[] areas;

    //Enumerable para definir que tipo de area es
    public enum TipoArea { SinCalificar, Aprobado, Reprobado, Invalido}
    public TipoArea tipoArea;


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Se soltó un objeto dentro de " + transform.name);
        if (tipoArea == TipoArea.Invalido)//En el caso de que el estudiante esté en el área de estudiantes aprobados
        {
            eventData.pointerDrag.GetComponent<DragAndDrop>().dentroDeUnArea = false;
        }
        if (eventData.pointerDrag != null)
        {           
            foreach (GameObject area in areas)
            {
                if (area.GetComponent<AreaController>().hasAnObject)
                {
                    Debug.Log("Ya hay un objeto aquí");
                }
                else
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = area.GetComponent<RectTransform>().localPosition;//Acomodar el objeto de estudiante en una de las casillas libres del area dropeable
                    if(tipoArea == TipoArea.Aprobado)//En el caso de que el estudiante esté en el área de estudiantes aprobados
                    {
                        eventData.pointerDrag.GetComponent<DragAndDrop>().dentroDeUnArea = true;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().calificado = true;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().aprobado = true;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().sinCalificar = false;
                    }
                    else if (tipoArea == TipoArea.Reprobado)//En el caso de que el estudiante esté en el área de estudiantes reprobados
                    {
                        eventData.pointerDrag.GetComponent<DragAndDrop>().dentroDeUnArea = true;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().calificado = true;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().aprobado = false;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().sinCalificar = false;
                    }
                    else if (tipoArea == TipoArea.SinCalificar)//En el caso de que el estudiante esté en el área de estudiantes sin calificar
                    {
                        eventData.pointerDrag.GetComponent<DragAndDrop>().dentroDeUnArea = true;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().calificado = false;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().aprobado = false;
                        eventData.pointerDrag.GetComponent<DragAndDrop>().sinCalificar = true;
                    }
                    area.GetComponent<AreaController>().SetValue(true);//Indicar que el area ya contiene un objeto(Para prevenir que se coloque otro objeto en el mismo sitio)
                    area.GetComponent<AreaController>().SetObjectInside(eventData.pointerDrag.gameObject);//Indicar cual es el objeto contenido en el area
                    break;
                }
            }
        }
    }
}
