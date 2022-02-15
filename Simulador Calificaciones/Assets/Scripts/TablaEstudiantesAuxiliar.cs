using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablaEstudiantesAuxiliar : MonoBehaviour
{
    private Transform contenedorTabla;
    public Transform templateTabla;//Objeto que tiene la estructura visual para mostrar los datos en la tabla
    public JSONReader dataEstuadiantes;
    public bool[] aprobacionEstudiantes;//Arreglo booleano para almacenar aprobado/reprobado true/false
    public float newItemHeight = 30f;//Variable publica para especificar la distancia entre cada fila de la tabla
    public float heightCompensacion = 100f;//Para que la lista auxiliar empieze desde mas arriba
    public bool listaCargada = false;

    private void Awake()
    {
        contenedorTabla = transform.Find("TemplateEstudianteContainer");
    }

    private void Update()
    {
        if (dataEstuadiantes.listaCargada && !listaCargada)
        {
            CargarTabla();
            
        }
        if (dataEstuadiantes.necesitaActualizarLista)
        {
            //Hay que actualizar la lista
            RefrescarTabla();
        }
    }

    //Funcion para Cargar los datos en la tabla
    public void CargarTabla()
    {
        int i = 1;
        aprobacionEstudiantes = new bool[dataEstuadiantes.myListaEstudiantes.estudiante.Length];//Se inicializa el arreglo para almacenar el valor numerico de cada nota
        foreach (JSONReader.Estudiante estudiante in dataEstuadiantes.myListaEstudiantes.estudiante)
        {
            Transform template = Instantiate(templateTabla, contenedorTabla);
            //Resetear la posicion en eje Y del primer Item (para mantener el orden de los items)
            if (i == 1)
            {
                template.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            }
            RectTransform newItemTransform = templateTabla.GetComponent<RectTransform>();
            newItemTransform.anchoredPosition = new Vector2(0, -newItemHeight * i);//Se calcula la ubicacion del nuevo estudiante en la tabla
            template.Find("NombreEstudiante").GetComponent<Text>().text = estudiante.nombre;//Se transcribe el nombre del estudiante en la plantilla
            template.Find("ApellidoEstudiante").GetComponent<Text>().text = estudiante.apellido;//Se transcribe el apellido del estudiante en la plantilla
            template.Find("EdadEstudiante").GetComponent<Text>().text = estudiante.edad.ToString();//Se transcribe la edad del estudiante en la plantilla
            template.Find("CodigoEstudiante").GetComponent<Text>().text = estudiante.codigo;//Se transcribe el codigo del estudiante en la plantilla
            template.Find("EmailEstudiante").GetComponent<Text>().text = estudiante.email;//Se transcribe el email del estudiante en la plantilla
            template.Find("NotaEstudiante").GetComponent<Text>().text = estudiante.nota.ToString("N1");//Se transcribe la nota del estudiante en la plantilla en el formato de un solo decimal
            //Validacion si el estudiante aprobó o reprobó y almacenamiento del resultado en el arreglo
            if (estudiante.nota >= 3.0f)
            {
                aprobacionEstudiantes[i - 1] = true;//El estudiante aprobó
            }
            else
            {
                aprobacionEstudiantes[i - 1] = false;//El estudiante reprobó
            }
            i++;
            template.GetComponent<RectTransform>().localPosition += new Vector3(0, heightCompensacion, 0);

        }
        listaCargada = true;
        //dataEstuadiantes.necesitaActualizarLista = false;
    }

    

        //Funcion para destruir todos los elementos y luego llamar la funcion para cargar los datos nuevos en la tabla
        public void RefrescarTabla()
    {
        GameObject[] estudiantesParaBorrar = GameObject.FindGameObjectsWithTag("Estudiante");
        foreach (GameObject estudiante in estudiantesParaBorrar) {
            Destroy(estudiante);
        }
        CargarTabla();
    }
}
