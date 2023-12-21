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
    public bool cameraLocked;
    public bool puckResuelto;
    public bool sifoResuelto;
    public bool bolaResuelto;
    public bool cifraResuelto;
    public bool enDialogo;
    public bool pieza1Colocada;
    public bool pieza2Colocada;
    public bool pieza3Colocada;

    void Start()
    {
        discoCogido = false;
        torresResuelto = false;
        monedaCogida = false;
        enDialogo = false;
        cameraLocked = false;
        recoleccionAutomatica = PlayerPrefs.GetInt("autoRecolect") == 2;
        puckResuelto = false;
        pieza1Colocada = false;
        pieza2Colocada = false;
        pieza3Colocada = false;
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
