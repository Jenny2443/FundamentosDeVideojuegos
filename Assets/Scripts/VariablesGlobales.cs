using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesGlobales : MonoBehaviour
{

    public bool discoCogido;
    public bool torresResuelto; //Varible global para saber que se ha resuelto las torres de hanoi 
    // Start is called before the first frame update
    public bool monedaCogida;
    public bool recoleccionAutomatica;

    void Start()
    {
        discoCogido = false;
        torresResuelto = false;
        monedaCogida = false;
        recoleccionAutomatica = PlayerPrefs.GetInt("autoRecolect") == 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
