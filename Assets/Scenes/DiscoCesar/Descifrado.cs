using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Descifrado : MonoBehaviour
{
    [SerializeField] private TMP_Text textoPresiona;

    public GameObject rocaDescifrado;
    public GameObject discoInterno;
    // private Animator animator;
    // private bool giroCompleto;
    private bool discoCogido;
    private bool estaEnRango;

    // Start is called before the first frame update
    void Start()
    {
        rocaDescifrado.SetActive(false);
        //discoInterno.GetComponent<Animator>() = GetComponent<Animator>();
        //discoInterno.GetComponent<Animator>().SetBool(giroCompleto, false);
        discoCogido = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(discoCogido && estaEnRango){
            textoPresiona.gameObject.SetActive(true);
        }
        if (discoCogido && estaEnRango && Input.GetKeyDown(KeyCode.F)/* && !giroCompleto*/){
            discoInterno.GetComponent<Animator>().SetTrigger("Girar");
            //discoInterno.GetComponent<Animator>().SetBool(giroCompleto,true);
            textoPresiona.gameObject.SetActive(false);
            //GiroInteriorCompleto;
        }
    }

    public void GiroInteriorCompleto (){
        gameObject.SetActive(false);
        rocaDescifrado.SetActive(true);
    }

        private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            estaEnRango = true;
            Debug.Log("Se puede poner el disco");
            textoPresiona.gameObject.SetActive(true);
        }
        if (other.CompareTag("DiscoAlbertiPequeno"))
        {
            discoCogido = true;
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player"))
        {
            estaEnRango = false;
            Debug.Log("No se puede poner el disco");
            textoPresiona.gameObject.SetActive(false);
        }
        if (other.CompareTag("DiscoAlbertiPequeno"))
        {
            discoCogido = false;
        }
    }
}
