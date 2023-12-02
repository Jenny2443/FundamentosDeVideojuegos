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

    // Start is called before the first frame update
    void Start()
    {
        recolAutoActivada = GameObject.FindGameObjectWithTag("ActivadaRecAuto");
        recolAutoDesactivada = GameObject.FindGameObjectWithTag("DesactivadaRecAuto");
        recolAutoActivada.SetActive(false);
        recolAutoDesactivada.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarAuto(){
        almacen.recoleccionAutomatica = true;
        recolAutoActivada.SetActive(true);
        recolAutoDesactivada.SetActive(false);
    }

    public void DesactivarAuto(){
        almacen.recoleccionAutomatica = false;
        recolAutoActivada.SetActive(false);
        recolAutoDesactivada.SetActive(true);
    }

    public void atras(){
        SceneManager.LoadScene("Menú Principal");
    }
}
