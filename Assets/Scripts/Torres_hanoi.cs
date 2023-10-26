using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torres_hanoi : MonoBehaviour
{
    /*La tabla mostrada a continuacion es la tabla de transiciones de un
     Automata finito determinista que se comparta de la misma forma que
    el puzzle de las torres de Hanoy.    
    *Las columnas representan la accion
    que puede realizar el jugador: Meter el disco grande, meter el mediano,
    meter el pequeno o sacar el de arriba.
    *Las filas representan las distintas combinaciones de discos que puede haber
    *Cada estado de este automata tiene un gameobject asociado en el editor
    *-1 indica que es imposible realizar esa accion y por tanto no se debe transicionar
    */
    //Disco introducido/Accion   Big Medium  Small    Coger
    int[,] matrizEstados = {  {   1,     6,    11,      -1}, // 0
                           {  -1,     2,     4,       0}, // 1
                           {  -1,    -1,     3,       1}, // 2
                           {  -1,    -1,    -1,       2}, // 3
                           {  -1,     5,    -1,       1}, // 4
                           {  -1,    -1,    -1,       4}, // 5
                           {   7,    -1,     9,       0}, // 6
                           {  -1,    -1,     8,       6}, // 7
                           {  -1,    -1,    -1,       7}, // 8
                           {  10,    -1,    -1,       6}, // 9
                           {  -1,    -1,    -1,       9}, //10
                           {  12,    14,    -1,       0}, //11
                           {  -1,    13,    -1,      11}, //12
                           {  -1,    -1,    -1,      12}, //13
                           {  15,    -1,    -1,      11}, //14
                           {  -1,    -1,    -1,      15}};//15

    int estadoActual, proximoEstado;

    public Item discoGrande;
    public Item discoPequeno;
    public Item discoMediano;

    private Item[] torre = new Item[3];
    private int SP = 0;

    public bool jugadorEnContacto;

    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        //Dejamos el puzzle visualmente en el estado 8 para poder coger los 3 discos
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(8).gameObject.SetActive(true);
        torre[SP] = discoMediano;
        SP = SP < 2 ? SP + 1 : SP;
        Debug.Log("Introducido disco mediano");
        Debug.Log(SP);

        torre[SP] = discoGrande;
        SP = SP < 2 ? SP + 1 : SP;
        Debug.Log("Introducido disco grande");
        Debug.Log(SP);

        torre[SP] = discoPequeno;
        SP = SP < 2 ? SP + 1 : SP;
        Debug.Log("Introducido disco pequeño");
        Debug.Log(SP);

        estadoActual = 8;

    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorEnContacto)
            transicionar();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.clickes.SetActive(true);
            jugadorEnContacto = true;
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.clickes.SetActive(true);
            jugadorEnContacto = true;
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        jugadorEnContacto = false;
        inventory.clickes.SetActive(false);
    }

    int transicionar()
    {
        int columna = -1;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pulsada la F");
            columna = getColumn();
            if (columna != -1)
            {
                Debug.Log(inventory.getInventoryItem(inventory.getNowActive()));
                SP = SP < 2 ? SP + 1 : SP;
                torre[SP] = inventory.getInventoryItem(inventory.getNowActive());
                torre[SP].transform.position = new Vector3(-37, 15, -129);
                torre[SP].transform.rotation = new Quaternion(0,0,0,0);
                torre[SP].GetComponent<Rigidbody>().useGravity = false;
                inventory.RemoveItem();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Pulsado el raton");
            columna = 3;
            Debug.Log(torre[SP].tag);
            torre[SP].inventory.AddItem(torre[SP]);
            torre[SP].GetComponent<Rigidbody>().useGravity = true;
            torre[SP] = null;
            SP = SP > 0 ? SP - 1 : SP;
            Debug.Log("Recogido el disco de arriba");
            Debug.Log(SP);

        }

        proximoEstado = columna > -1 ? matrizEstados[estadoActual, columna] : -1;
        if (proximoEstado != -1)
        {
            this.transform.GetChild(estadoActual).gameObject.SetActive(false);
            this.transform.GetChild(proximoEstado).gameObject.SetActive(true);
            estadoActual = proximoEstado;
        }

        return estadoActual;
    }

    public int getColumn()
    {
        if (inventory.getInventoryItem(inventory.getNowActive()).CompareTag("DiscoGrande"))
        {
            return 0;
        }
        if (inventory.getInventoryItem(inventory.getNowActive()).CompareTag("DiscoMediano"))
        {
            return 1;
        }
        if (inventory.getInventoryItem(inventory.getNowActive()).CompareTag("DiscoPequeno"))
        {
            return 2;
        }
        else
        {
            return -1;
        }
    }

}
