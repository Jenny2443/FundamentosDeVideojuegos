using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    // Tamaño del rectángulo de colisión, se modifica desde el inspector
    [SerializeField]
    private Vector2 rectangleSize = new Vector2(1f, 1f);
    // Variables para el arrastre
    private Vector3 offset, posInicial, screenSpace;
    // Variable para saber si se está arrastrando
    private bool isDragging = false;

    void OnMouseDown()
    {
        // Obtener la posición inicial del objeto en la pantalla y configurar variables para el arrastre
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        posInicial = transform.position;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        // Calcular la nueva posición del objeto durante el arrastre
        Vector3 posicion = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        Vector3 curScreenSpace = posicion;
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        // Limitar el movimiento a horizontal o vertical
        if (Mathf.Abs(posInicial.x - curPosition.x) > Mathf.Abs(posInicial.y - curPosition.y))
        {
            curPosition.y = posInicial.y;
        }
        else
        {
            curPosition.x = posInicial.x;
        }

        // Detectar colisiones con otros objetos
        Collider2D[] colliders = Physics2D.OverlapBoxAll(curPosition, rectangleSize, 0f);

        foreach (Collider2D collider in colliders)
        {
            // Verificar si hay colisión con objetos específicos
            if (collider != null && collider.gameObject != gameObject &&
               (collider.CompareTag("Ficha") || collider.CompareTag("Marco") || collider.CompareTag("Esmeralda")))
            {
                // Aplicar fuerza opuesta si hay colisión con ciertos objetos
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 oppositeForce = (posInicial - curPosition).normalized;

                    // Aplicar fuerza solo en los ejes X o Y para evitar movimiento diagonal
                    rb.AddForce(new Vector2(oppositeForce.x, oppositeForce.y) * 5f, ForceMode2D.Impulse);
                }

                Debug.Log("contacto");
                return;
            }
        }

        // Alinear posición solo en los ejes X o Y para evitar movimiento diagonal
        transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
    }

    void OnMouseUp()
    {
        // Finalizar el arrastre cuando se suelta el mouse
        isDragging = false;
    }
}