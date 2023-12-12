using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Torres_hanoi2 : MonoBehaviour
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
    //                                                           Estados
    //                                                             ||
    //Disco introducido/Accion ->   Big Medium  Small    Coger     \/
    int[,] matrizEstados =       {{   1,     6,    11,      -1}, // 0
                                  {  -1,     2,     4,       0}, // 1
                                  {  -1,    -1,     3,       1}, // 2
                                  {  -1,    -1,    -1,       2}, // 3
                                  {  -1,    -1,    -1,       1}, // 4
                                  {  -1,    -1,    -1,       4}, // 5
                                  {  -1,    -1,     9,       0}, // 6
                                  {  -1,    -1,     8,       6}, // 7
                                  {  -1,    -1,    -1,       7}, // 8
                                  {  -1,    -1,    -1,       6}, // 9
                                  {  -1,    -1,    -1,       9}, //10
                                  {  -1,    -1,    -1,       0}, //11
                                  {  -1,    -1,    -1,      11}, //12
                                  {  -1,    -1,    -1,      12}, //13
                                  {  -1,    -1,    -1,      11}, //14
                                  {  -1,    -1,    -1,      15}};//15

    public VariablesGlobales almacen; //Referancia a un GameObject que contiene variables que son accedidas por varios scripts

    int estadoActual, proximoEstado;

    //Referencias a los discos
    public Item discoGrande;
    public Item discoPequeno;
    public Item discoMediano;

    public Torres_hanoi torreIzda;
    public Torres_hanoi2 torreCentro;
    public Torres_hanoi2 torreDcha;

    //Pila de torres con un Stack Pointer
    private Item[] torre = new Item[3];
    private int SP = 0;

    public bool jugadorEnContacto;

    //Ya no tengo claro que esta variable sea necesaria, pero tengo demasiado sueño para comrpobarlo
    bool control = true;

    //Referencia a el mensaje de interfaz para presionar f
    [SerializeField] private TMP_Text textoPresiona;

    // Referencia al inventario
    public Inventory inventory;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        //Dejamos el puzzle visualmente en el estado 0
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(0).gameObject.SetActive(true);

        estadoActual = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorEnContacto && PlayerPrefs.GetInt("autoRecolect") != 2)
        {
            if (this.CompareTag("TorreFinal"))
            {
                if (estadoActual != 3)
                    transicionar();
            }
            else
            {
                transicionar();
            }
        }
        if (!jugadorEnContacto || Input.GetKeyUp(KeyCode.P))
        {
            timer = 0f;
        }
        if (jugadorEnContacto && PlayerPrefs.GetInt("skipMechanics") == 3 && Input.GetKey(KeyCode.P))
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= 3)
            {
                resolver(0);

                timer = 0f;

            }
        }
    }

    public void resolver(int origin)
    {
        if (!this.CompareTag("TorreFinal"))
        {
            this.transform.GetChild(estadoActual).gameObject.SetActive(false);
            estadoActual = 0;
            this.transform.GetChild(estadoActual).gameObject.SetActive(true);

            SP = 0;
            if (origin == 0)
            {
                torreIzda.resolver(1);
                torreDcha.resolver(1);
            }
        }
        else {
            this.transform.GetChild(estadoActual).gameObject.SetActive(false);
            estadoActual = 3;
            this.transform.GetChild(estadoActual).gameObject.SetActive(true);
            torre[SP] = discoGrande;
            SP++;
            Debug.Log("Introducido disco grande");
            Debug.Log(SP);

            torre[SP] = discoMediano;
            SP++;
            Debug.Log("Introducido disco mediano");
            Debug.Log(SP);

            torre[SP] = discoPequeno;
            SP++;
            Debug.Log("Introducido disco pequeño");
            Debug.Log(SP);
            if (origin == 0)
            {
                torreIzda.resolver(1);
                torreCentro.resolver(1);
            }
            almacen.torresResuelto = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brazo") && PlayerPrefs.GetInt("autoRecolect") == 2 && control)
        {
            control = false;
            Debug.Log("Tengo sueño");
            int pos = inventory.contains("DiscoGrande");
            if (pos != -1)
            {
                inventory.GetItem(pos);
            }
            else
            {
                pos = inventory.contains("DiscoMediano");
                if (pos != -1)
                {
                    inventory.GetItem(pos);
                }
                else
                {
                    pos = inventory.contains("DiscoMediano");
                    if (pos != -1)
                    {
                        inventory.GetItem(pos);
                    }
                }
            }
            if (this.CompareTag("TorreFinal"))
            {
                if (estadoActual != 3)
                    transicionar();
            }
            else
            {
                transicionar();
            }
        }
    }

    //Si el jugador esta a la distancia para interactuar con el objeto
    //entonces se actualiza la UI dependiendo de lo que pueda hacer
    //Usar un objeto con f o clickar para cogerlo
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Brazo"))
        {
            if (SP != 0 && !almacen.discoCogido)
                if (this.CompareTag("TorreFinal"))
                {
                    if (estadoActual != 3)
                        inventory.clickes.SetActive(true);
                }
                else
                {
                    inventory.clickes.SetActive(true);
                }
            if (getColumn() != -1)
            {
                textoPresiona.gameObject.SetActive(true);
            }
            jugadorEnContacto = true;
        }
    }

    //Si el jugador esta fuera de la distancia para interactuar con el objeto
    //entonces se actualiza la UI
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brazo"))
        {
            jugadorEnContacto = false;
            inventory.clickes.SetActive(false);
            textoPresiona.gameObject.SetActive(false);
            control = true;
        }
    }


    //Dependiendo de lo que haya realizado el jugador, entonces transicoinara a un
    // estado segun la matriz de transiciones. Si la matriz devuelve -1 es que no se
    // debe hacer nada. Si no, entonces se debe introducir(getcolumn = 0,1,2) o sacar
    // el disco del stack (getcolumn = 3).
    int transicionar()
    {
        if (almacen.discoCogido)
            inventory.clickes.SetActive(false);
        int columna = -1;
        if (Input.GetKeyDown(KeyCode.F) || PlayerPrefs.GetInt("autoRecolect") == 2 && getColumn() != -1)
        {
            Debug.Log("Pulsada la F");
            columna = getColumn();
            proximoEstado = columna > -1 ? matrizEstados[estadoActual, columna] : -1;
            if (SP < 3 && proximoEstado != -1)
            {
                torre[SP] = inventory.getInventoryItem(inventory.getNowActive());
                torre[SP].transform.position = new Vector3(-37, 15, -129);
                torre[SP].transform.rotation = Quaternion.Euler(0, 0, 0);
                torre[SP].GetComponent<Rigidbody>().useGravity = false;
                inventory.RemoveItem();
                SP++;

                almacen.discoCogido = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && !almacen.discoCogido || PlayerPrefs.GetInt("autoRecolect") == 2 && !almacen.discoCogido)
        {
            Debug.Log("Pulsado el raton");
            columna = 3;
            proximoEstado = columna > -1 ? matrizEstados[estadoActual, columna] : -1;
            if (SP != 0 && proximoEstado != -1)
            {
                SP--;
                torre[SP].inventory.AddItem(torre[SP]);
                torre[SP].GetComponent<Rigidbody>().useGravity = true;
                torre[SP] = null;
                almacen.discoCogido = true;
            }

        }

        proximoEstado = columna > -1 ? matrizEstados[estadoActual, columna] : -1;
        if (proximoEstado != -1)
        {
            this.transform.GetChild(estadoActual).gameObject.SetActive(false);
            this.transform.GetChild(proximoEstado).gameObject.SetActive(true);
            estadoActual = proximoEstado;

            //Cuando consigue resolver entonces tiene que empezar el dialogo con sifo
            if (proximoEstado == 3)
            {
                almacen.torresResuelto = true;
                Debug.Log("torres resueltas");
            }
        }

        return estadoActual;
    }


    //Sirve para obtener la columna de la matriz de transiciones a la que consultar
    //en el caso en el que un objeto sea usado.
    //Devuelve -1 si el objeto no es un disco.
    public int getColumn()
    {
        Item item = inventory.getInventoryItem(inventory.getNowActive());
        if (item == null)
        {
            return -1;
        }
        if (item.CompareTag("DiscoGrande"))
        {
            return 0;
        }
        if (item.CompareTag("DiscoMediano"))
        {
            return 1;
        }
        if (item.CompareTag("DiscoPequeno"))
        {
            return 2;
        }
        else
        {
            return -1;
        }
    }

}
