using UnityEngine;
using TMPro;

public class ColocarPiezas1 : MonoBehaviour
{
    // Booleano para saber si la pieza 1 está colocada
    public bool pieza1Colocada;

    // Mensaje de interfaz para presionar F
    [SerializeField] private TMP_Text textoPresiona;

    // Referencia al inventario
    public Inventory inventario;

    private Item item;

    private bool enRango;

    // Variable para saber si una pieza está colocada
    private bool unaPiezaColocada;

    void Start()
    {
        // Inicializar booleanos
        pieza1Colocada = false;
        enRango = false;
        unaPiezaColocada = false;

        // Desactivar los textos
        textoPresiona.gameObject.SetActive(false);
    }

    void Update()
    {
        // Obtener el item actual en la mano
        item = inventario.getInventoryItem(inventario.getNowActive());

        // Si está en rango, hay un item en la mano, no hay una pieza colocada y se pulsa la tecla F, se instancia el item
        if (Input.GetKeyDown(KeyCode.F) && enRango && item != null && !unaPiezaColocada)
        {
            // Instanciar el item en el mundo
            item = GameObject.Instantiate(inventario.inventory[inventario.nowActive]);

            // Verificar si la pieza es una recompensa específica (ej. "Recompensa1")
            if (item.gameObject.CompareTag("Recompensa1"))
            {
                pieza1Colocada = true;
                Debug.Log("Pieza 1 colocada en su sitio");
            }

            // Destruir el item del inventario
            inventario.DestroyItem();

            // Configurar la escala y la posición del item en el mundo
            item.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            item.transform.position = transform.position + new Vector3(0, 0.3f, 0);

            // Desactivar el mensaje de presionar F
            textoPresiona.gameObject.SetActive(false);

            // Marcar que una pieza ha sido colocada
            unaPiezaColocada = true;
        }

        // Si está en rango, hay una pieza colocada y se pulsa el click izquierdo(coger objeto), 
        // se pone a falso unaPiezaColocada
        if (enRango && unaPiezaColocada && Input.GetKeyDown(KeyCode.Mouse0))
        {
            unaPiezaColocada = false;
        }
    }

    // Método que se ejecuta cuando el jugador entra en el rango de la ranura
    private void OnTriggerStay(Collider other)
    {
        // Si el jugador colisiona con una ranura
        if (other.CompareTag("Brazo"))
        {
            enRango = true;
            if (!unaPiezaColocada)
            {
                // Mostrar el mensaje de presionar F
                textoPresiona.gameObject.SetActive(true);
            }
        }
    }

    // Método que se ejecuta cuando el jugador sale del rango de la ranura
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brazo"))
        {
            // El jugador sale del rango, desactivar el mensaje de presionar F
            enRango = false;
            textoPresiona.gameObject.SetActive(false);
        }
    }
}
