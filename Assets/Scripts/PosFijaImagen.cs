using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosFijaImagen : MonoBehaviour
{
    // Establece la posición relativa al fondo de la pantalla
    public Vector2 posicionRelativa = new Vector2(0.5f, 0.1f);

    // Referencia al RectTransform del objeto
    private RectTransform rectTransform;
    
    // Referencia al RectTransform de la caja de diálogo
    public RectTransform cajaDialogo;

    // Posición relativa a la caja de diálogo
    public Vector2 offsetDesdeCaja = new Vector2(0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        // Obtén el RectTransform del objeto
        rectTransform = GetComponent<RectTransform>();

        // Llama a la función para actualizar la posición
        ActualizarPosicion();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarPosicion();
    }
    
    // Función para actualizar la posición del objeto en base a las coordenadas normalizadas
    void ActualizarPosicion()
    {
        // Asegúrate de tener una referencia a la caja de diálogo
        if (cajaDialogo != null)
        {
            // Obtén la posición de la caja de diálogo en el espacio del mundo
            Vector3 posicionCaja = cajaDialogo.position;

            // Ajusta la posición en base al offset relativo a la caja de diálogo
            Vector3 nuevaPosicion = new Vector3(posicionCaja.x + offsetDesdeCaja.x, posicionCaja.y + offsetDesdeCaja.y, 0f);

            // Convierte la posición en el espacio del mundo a la posición local en el RectTransform
            rectTransform.position = nuevaPosicion;
        }
        else
        {
            Debug.LogError("La referencia a la caja de diálogo no está asignada.");
        }
    }
}
