using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class JSONReader : MonoBehaviour
{
    private static string jsonPath = Application.streamingAssetsPath + "/estudiantes.json";
    private string jsonContent = File.ReadAllText(jsonPath);//Convertimos el archivo Json en string para posterior manipulacion

    //Clase con la estructura de Estudiante basada en la estructura que se encuentra en el archivo Json
    [System.Serializable]
    public class Estudiante
    {
        public string nombre;
        public string apellido;
        public float edad;
        public string codigo;
        public string email;
        public float nota;
    }

    [System.Serializable]
    public class ListaEstudiantes
    {
        public Estudiante[] estudiante;//Creamos objeto con la estructura del Json
    }

    public ListaEstudiantes myListaEstudiantes = new ListaEstudiantes();
    public bool listaCargada = false;//Variable que hará seguimiento si los datos fueron cargados del archivo Json por primera vez
    public bool necesitaActualizarLista = false;//Variable que hará seguimiento de si es necesario o no actualizar los datos de estudiantes
    public InputField jsonTextUI;


    // Start is called before the first frame update
    void Start()
    {
        myListaEstudiantes = JsonUtility.FromJson<ListaEstudiantes>(jsonContent);//Cargamos los datos de los estudiantes por primera vez en un array
        jsonTextUI.SetTextWithoutNotify(jsonContent);//Actualizamos el texto del editor
        listaCargada = true;
    }

    // Update is called once per frame
    void Update()
    {
        ChecarCambios();//Checamos todo el tiempo si el archivo Json cambia en algun momento
    }

    //Funcion que lee el archivo Json que se encuentra en la carpeta de StreamingAssets en formato string y lo compara con el ya existente en busa de cambios
    //De existir un cambio en el archivo, simplemente reemplaza los datos antiguos con los nuevos
    void ChecarCambios()
    {
        //Try and catch para evitar errores producidos por el tiempo que pueda tomar leer el archivo Json
        try
        {
            string newJsonContent = File.ReadAllText(jsonPath);//Convertimos el archivo Json en string para posterior validacion
            if (newJsonContent != jsonContent)
            {
                Debug.Log("Lista Modificada, se necesita actualizar los datos...");
                necesitaActualizarLista = true;
                jsonContent = newJsonContent;//Actualizamos los datos del Json antiguo con el nuevo
                jsonTextUI.SetTextWithoutNotify(jsonContent);//Actualizamos el texto del editor
                myListaEstudiantes = JsonUtility.FromJson<ListaEstudiantes>(jsonContent);//Cargamos los nuevos datos en el array

            }
        }
        catch
        {
            Debug.Log("Esperando a encontrar el archivo Json....");
        }
    }

    //Guardar el Json que se editó desde la interfaz
    public void SaveJson()
    {
        File.WriteAllText(jsonPath, jsonTextUI.text);
    }

    void OnGUI()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
