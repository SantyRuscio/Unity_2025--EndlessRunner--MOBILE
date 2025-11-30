using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class BotonBrilloo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Material materialInstancia;
    private float valorActual = 0f;
    private float objetivo = 0f;

    [Header("Velocidad del Fade")]
    [SerializeField] private float velocidad = 10f;

    void Start()
    {
        Image imagen = GetComponent<Image>();
        if (imagen != null)
        {

            materialInstancia = new Material(imagen.material);

            imagen.material = materialInstancia;

            materialInstancia.SetFloat("_HoverPower", 0);
        }
    }

    void Update()
    {
        if (Mathf.Abs(valorActual - objetivo) > 0.001f)
        {
            valorActual = Mathf.Lerp(valorActual, objetivo, Time.deltaTime * velocidad);
            if (materialInstancia != null)
            {
                materialInstancia.SetFloat("_HoverPower", valorActual);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        objetivo = 1f; 
    }

    // Cuando sale el mouse
    public void OnPointerExit(PointerEventData eventData)
    {
        objetivo = 0f; 
    }
}
