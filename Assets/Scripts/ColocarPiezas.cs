using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class ColocarPiezas : MonoBehaviour
{
    // Diccionario que asocia cada etiqueta de pieza con su zona delimitada correspondiente
    private Dictionary<string, Collider> diccionarioPiezasZonas = new Dictionary<string, Collider>();

    public Vector3 posicionPieza;

    public bool enRangoMoneda;
    public bool enRangoDisco;
    public bool enRangoEsmeralda;

    // Referencia al mensaje de interfaz para presionar F
    [SerializeField] private TMP_Text textoPresiona;

    // Referencia al inventario
    public Inventory inventario;

    public Item item;

    void Start()
    {
        // Inicializar booleanos
        enRangoMoneda = false;
        enRangoDisco = false;
        enRangoEsmeralda = false;

        // Asociar cada etiqueta de pieza con su zona delimitada correspondiente
        diccionarioPiezasZonas.Add("moneda", zonaDelimitadaMoneda);
        diccionarioPiezasZonas.Add("disco", zonaDelimitadaDisco);
        diccionarioPiezasZonas.Add("esmeralda", zonaDelimitadaEsmeralda);
    }

    void Update()
    {
        // Si esta en rango de la zona de la moneda y se presiona la tecla F
        if (enRangoMoneda && Input.GetKeyDown(KeyCode.F))
        {
            // itera sobre todos los elementos del inventario
            if(buscaPieza("moneda") >= 0){
                // Instancia ese item y lo elimina
                Instantiate(inventario.inventory[buscaPieza("moneda")].gameObject, posicionPieza, Quaternion.identity);
                inventario.RemoveItem();
            }
        }
    }

// Funcion que busca una pieza con un tag en el inventario y devuelve la posicion
    private int buscaPieza(string tag)
    {
        // itera sobre todos los elementos del inventario
        for (int i = 0; i < inventario.inventory.Length; i++)
        {
            // si el elemento actual del inventario no es nulo y tiene el tag buscado
            if (inventario.inventory[i] != null && inventario.inventory[i].gameObject.CompareTag(tag))
            {
                // devuelve el indice del elemento
                return i;
            }
        }
        // si no se encuentra el elemento, devuelve -1
        return -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si esta en rango de la zona de la moneda y el item tiene el tag moneda
        if (other == zonaDelimitadaMoneda && item.gameObject.CompareTag("moneda"))
        {
            enRangoMoneda = true;
            textoPresiona.gameObject.SetActive(true);
            // Código para cuando algo entre en la zona de la moneda
        }
        else if (other == zonaDelimitadaDisco && item.gameObject.CompareTag("disco"))
        {
            enRangoDisco = true;
            textoPresiona.gameObject.SetActive(true);
            // Código para cuando algo entre en la zona del disco
        }
        else if (other == zonaDelimitadaEsmeralda && item.gameObject.CompareTag("esmeralda"))
        {
            enRangoEsmeralda = true;
            textoPresiona.gameObject.SetActive(true);
            // Código para cuando algo entre en la zona de la esmeralda
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == zonaDelimitadaMoneda && item.gameObject.CompareTag("moneda"))
        {
            enRangoMoneda = false;
            textoPresiona.gameObject.SetActive(false);
            // Código para cuando algo salga de la zona de la moneda
        }
        else if (other == zonaDelimitadaDisco && item.gameObject.CompareTag("disco"))
        {
            enRangoDisco = false;
            textoPresiona.gameObject.SetActive(false);
            // Código para cuando algo salga de la zona del disco
        }
        else if (other == zonaDelimitadaEsmeralda && item.gameObject.CompareTag("esmeralda"))
        {
            enRangoEsmeralda = false;
            textoPresiona.gameObject.SetActive(false);
            // Código para cuando algo salga de la zona de la esmeralda
        }

    }

    // Zonas delimitadas específicas para cada tipo de pieza
    public Collider zonaDelimitadaMoneda;
    public Collider zonaDelimitadaDisco;
    public Collider zonaDelimitadaEsmeralda;
}
