using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesGlobales : MonoBehaviour
{

    public bool discoCogido;
    public bool torresResuelto; //Varible global para saber que se ha resuelto las torres de hanoi 
    // Start is called before the first frame update
    public bool monedaCogida;

    void Start()
    {
        discoCogido = false;
        torresResuelto = false;
        monedaCogida = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
