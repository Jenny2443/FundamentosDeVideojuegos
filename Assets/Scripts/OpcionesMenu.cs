using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpcionesMenu : MonoBehaviour
{
    public VariablesGlobales almacen;
    public GameObject recolAutoActivada;
    public GameObject recolAutoDesactivada;

    public bool recoleccionAutomatica;

    // Start is called before the first frame update
    void Start()
    {
        recolAutoActivada = GameObject.FindGameObjectWithTag("ActivadaRecAuto");
        recolAutoDesactivada = GameObject.FindGameObjectWithTag("DesactivadaRecAuto");

        // Obtén el objeto persistente
        //DatosCompartidos datosCompartidos = GameObject.FindObjectOfType<DatosCompartidos>();

        // Accede a la variable booleana
        recoleccionAutomatica = almacen.recoleccionAutomatica; //datosCompartidos.autorecoleccion;

        if(recoleccionAutomatica){
            recolAutoActivada.SetActive(true);
            recolAutoDesactivada.SetActive(false);
        } else{
            recolAutoActivada.SetActive(false);
            recolAutoDesactivada.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarAuto(){
        recoleccionAutomatica = true;
        recolAutoActivada.SetActive(true);
        recolAutoDesactivada.SetActive(false);
    }

    public void DesactivarAuto(){
        recoleccionAutomatica = false;
        recolAutoActivada.SetActive(false);
        recolAutoDesactivada.SetActive(true);
    }

    public void atras(){
        SceneManager.LoadScene("Menú Principal");
    }
}
