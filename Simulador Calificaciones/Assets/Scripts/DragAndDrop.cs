using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public RectTransform canvasRecTransform;
    public bool calificado = false;
    public bool sinCalificar = true;
    public bool aprobado = false;
    public Vector2 originalPosition;
    public bool dentroDeUnArea = true;
    //[SerializeField]private RectTransform panel;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasRecTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Hizo click en el objeto " + transform.name);
        originalPosition = GetComponent<RectTransform>().localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Está arrastrando el objeto " + transform.name);
        rectTransform.anchoredPosition += eventData.delta / (1.3f* canvasRecTransform.localScale.x);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Empezo a arrastrar el objeto " + transform.name);
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Termino de arrastrar el objeto " + transform.name);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    private void Update()
    {
        //Cuando el objecto arrastable se coloca en área invalida, regresa a la posicion que tenía antes de ser arrastrado
        if (!dentroDeUnArea)
        {
            GetComponent<RectTransform>().localPosition = originalPosition;
            dentroDeUnArea = true;
        }
    }
}
