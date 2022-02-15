using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListaEstudiantesDrag : MonoBehaviour
{
    public Transform contenedorDragAndDrop;
    public Transform slotsPorCalificar;
    public Transform templateDragAndDrop;//Objeto que tiene la estructura visual para mostrar cada estudiante en el drag and drop
    public bool dragAndDropCargado = false;
    public JSONReader dataEstuadiantes;
    public GameObject mensajeError;//El mensaje de error
    public GameObject mensajeFelicitacion;//El mensaje de success
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (!dragAndDropCargado)
        {
            CargarDragAndDrop();
        }

        if (dataEstuadiantes.necesitaActualizarLista)
        {
            //Hay que actualizar la lista
            RefrescarTabla();
        }
    }

    public void CargarDragAndDrop()
    {
        int i = 0;
        foreach (JSONReader.Estudiante estudiante in GetComponent<TablaEstudiantesAuxiliar>().dataEstuadiantes.myListaEstudiantes.estudiante)
        {
            Transform template = Instantiate(templateDragAndDrop, contenedorDragAndDrop);
            template.Find("Nombre").GetComponent<Text>().text = estudiante.nombre;
            template.Find("Apellido").GetComponent<Text>().text = estudiante.apellido;
            template.GetComponent<RectTransform>().anchoredPosition = slotsPorCalificar.GetComponent<AreaDrop>().areas[i].GetComponent<RectTransform>().anchoredPosition;
            slotsPorCalificar.GetComponent<AreaDrop>().areas[i].GetComponent<AreaController>().hasAnObject = true;
            slotsPorCalificar.GetComponent<AreaDrop>().areas[i].GetComponent<AreaController>().objectInside = template.gameObject;
            i++;
            Debug.Log(i.ToString());
        }
        dragAndDropCargado = true;
        dataEstuadiantes.necesitaActualizarLista = false;
    }

    public void RefrescarTabla()
    {
        GetComponent<TablaEstudiantesAuxiliar>().RefrescarTabla();//Se refresca la tabla auxiliar con los nuevos datos
        GameObject[] estudiantesParaBorrar = GameObject.FindGameObjectsWithTag("EstudianteDrag");//Se buscan todos los objetos arrastrables de cada estudiante
        //Se elimina cada objeto arrastrable
        foreach (GameObject estudiante in estudiantesParaBorrar)
        {
            Destroy(estudiante);
        }
        CargarDragAndDrop();//Se vuelven a cargar los objetos arrastrables con los nuevos datos
    }

    public void VerificarAprobados()
    {
        GameObject[] estudiantesDrag = GameObject.FindGameObjectsWithTag("EstudianteDrag");
        int i = 0;
        int errores = 0;
        int sinCalificar = 0;
        foreach(GameObject estudiante in estudiantesDrag)
        {
            //Si el estudiante ha sido ubicado en una zona de calificacion APROBADO/REPROBADO
            if (estudiante.GetComponent<DragAndDrop>().calificado) { 
                //Si la calificacion del estudiante coincide con el área en donde se dropeo
                if(estudiante.GetComponent<DragAndDrop>().aprobado == GetComponent<TablaEstudiantesAuxiliar>().aprobacionEstudiantes[i])
                {
                    Debug.Log("Área correcta, estudiante " + estudiante.transform.Find("Nombre").GetComponent<Text>().text + " " + estudiante.transform.Find("Apellido").GetComponent<Text>().text);
                }
                else
                {
                    Debug.Log("Área incorrecta, estudiante " + estudiante.transform.Find("Nombre").GetComponent<Text>().text + " " + estudiante.transform.Find("Apellido").GetComponent<Text>().text);
                    errores += 1;
                }
            }
            else
            {
                Debug.Log("No se ha calificado aún al estudiante " + estudiante.transform.Find("Nombre").GetComponent<Text>().text + " " + estudiante.transform.Find("Apellido").GetComponent<Text>().text);
                sinCalificar += 1;
            }
            i++;
        }

        if(errores == 0 && sinCalificar == 0)
        {
            Debug.Log("¡Excelente! Ha calificado de manera correcta a los estudiantes");
            mensajeFelicitacion.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Tiene errores en la calificacion de " + errores.ToString() + " estudiante(s) y tiene " + sinCalificar.ToString() + " estudiantes sin calificar.");
            mensajeError.transform.Find("MensajeDeError").GetComponent<Text>().text = "POR FAVOR VERIFIQUE LOS DATOS INGRESADOS, ACTUALMENTE HAY " + errores.ToString() + " ESTUDIANTE(S) CON CALIFICACION ERRONEA Y " + sinCalificar.ToString() + " ESTUDIANTE(S) SIN CALIFICAR.";
            mensajeError.SetActive(true);//Se activa el mensaje de error
        }

    }
}
