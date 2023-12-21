using UnityEngine;
using TMPro;

public class ColocarPiezasF : MonoBehaviour
{
    // Mensaje de interfaz para presionar F
    [SerializeField] private TMP_Text textoPresiona;

    // Referencia al inventario
    public Inventory inventario;

    public VariablesGlobales almacen;

    public Item recompensa1;
    public Item recompensa2;
    public Item recompensa3;

    public int id;

    public ColocarPiezasF[] pilares = new ColocarPiezasF[5];

    private Item item;

    private bool enRango;

    private float timer;


    // Variable para saber si una pieza está colocada
    private bool unaPiezaColocada;

    public void resolver(int callingID) {
        if (callingID == id) {
            for (int i = 0; i < pilares.Length; i++) {
                if (i != id) {
                    pilares[i].resolver(id);
                }
            }
        }
        if (unaPiezaColocada) {
            unaPiezaColocada = false;
            item.transform.position = item.transform.position + new Vector3(0, 0, 1.3f);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            item = null;
        }
        if (this.CompareTag("Pedestal1"))
        {
            int pos = inventario.contains("Recompensa1");
            if (pos != -1) {
                item = inventario.RemoveItem(pos);
            }
            else
            {
                item = recompensa1;
            }
            item.transform.position = transform.position + new Vector3(0, 1.3f, 0);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            textoPresiona.gameObject.SetActive(false);
            item.gameObject.SetActive(true);
            unaPiezaColocada = true;
        }
        if (this.CompareTag("Pedestal2"))
        {
            int pos = inventario.contains("Recompensa2");
            if (pos != -1)
            {
                item = inventario.RemoveItem(pos);
            }
            else
            {
                item = recompensa2;
            }
            item.transform.position = transform.position + new Vector3(0, 1.3f, 0);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            textoPresiona.gameObject.SetActive(false);
            item.gameObject.SetActive(true);
            unaPiezaColocada = true;
        }
        if (this.CompareTag("Pedestal3"))
        {
            int pos = inventario.contains("Recompensa3");
            if (pos != -1)
            {
                item = inventario.RemoveItem(pos);
            }
            else
            {
                item = recompensa3;
            }
            item.transform.position = transform.position + new Vector3(0, 1.3f, 0);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            textoPresiona.gameObject.SetActive(false);
            item.gameObject.SetActive(true);
            unaPiezaColocada = true;
        }


    }

    void checkPiezas() {
        if (this.CompareTag("Pedestal1")) {
            if(item.CompareTag("Recompensa1")) {
                almacen.pieza1Colocada = true;
            }
        }
        if (this.CompareTag("Pedestal2"))
        {
            if (item.CompareTag("Recompensa2"))
            {
                almacen.pieza2Colocada = true;
            }
        }
        if (this.CompareTag("Pedestal3"))
        {
            if (item.CompareTag("Recompensa3"))
            {
                almacen.pieza3Colocada = true;
            }
        }
    }


    void Start()
    {
        // Inicializar booleanos
        enRango = false;
        unaPiezaColocada = false;

        // Desactivar el texto
        textoPresiona.gameObject.SetActive(false);
    }

    void colocarObjeto() {
            item = inventario.getInventoryItem(inventario.getNowActive());

            // Destruir el item del inventario
            inventario.RemoveItem();

            // Configurar la escala y la posición del item en el mundo
            item.transform.position = transform.position + new Vector3(0, 1.3f, 0);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

            // Desactivar el mensaje de presionar F
            textoPresiona.gameObject.SetActive(false);

            // Marcar que una pieza ha sido colocada
            unaPiezaColocada = true;
    }

    void recogerObjeto() {
        unaPiezaColocada = false;
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        item = null;
    }

    void Update()
    {
        if (enRango && PlayerPrefs.GetInt("skipMechanics") == 3 && Input.GetKey(KeyCode.P)
            && almacen.sifoResuelto && almacen.bolaResuelto && almacen.cifraResuelto)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= 3)
            {
                resolver(id);

                timer = 0f;

            }
        }

        if (Input.GetKeyDown(KeyCode.F) && enRango && !unaPiezaColocada)
            colocarObjeto();

        if (enRango && unaPiezaColocada && Input.GetKeyDown(KeyCode.Mouse0)) {
            recogerObjeto();
        }

        checkPiezas();
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
            if (unaPiezaColocada) {
                item.interactuable = true;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brazo") && PlayerPrefs.GetInt("autoRecolect") == 2)
        {
            enRango = true;
            if (!unaPiezaColocada)
            {
                if (enRango && !unaPiezaColocada)
                {
                    colocarObjeto();
                    item.interactuable = false;
                }
            }
            else
            {
                if (enRango && unaPiezaColocada) {
                    recogerObjeto();
                }
            }
        }
    }
}