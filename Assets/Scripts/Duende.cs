using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Duende : MonoBehaviour
{

    // Referencia al panel de diálogo para activarlo y desactivarlo
    [SerializeField] private GameObject panelDialogo;
    // Referencia al texto del diálogo para modificarlo
    [SerializeField] private TMP_Text textoDialogo;
    // Referencia el texto Presiona F
    [SerializeField] private TMP_Text textoPresiona;

    // string de las lineas de dialogo, textArea(min y max espacio vertical a mostrar): min 4 líneas, max: 6 líneas
    [SerializeField, TextArea(4,6)] private string[] lineasDialogo;

    [SerializeField, TextArea(4,6)] private string[] dialogoDespuesDeResuelto;

    // variable para el tiempo de tipado
    [SerializeField] private float tiempoEntreLetras = 0.05f;

    // variable para saber si el jugador está en rango
    private bool estaEnRango;
    // variable para saber si el dialogo ha empezado
    private bool dialogoEmpezado;
    // variable para terminar conversación con Sifo
    private bool terminado;
    //indice para lineasDialogo
    private int indice;
    //indice para dialogoDespuesDeResuelto
    private int indice2;

    public Item itemRecompensa; // Asigna el objeto de recompensa 
    public VariablesGlobales almacen;
    public Inventory inventory;

    void Update(){
        //almacen.monedaCogida = false;
        // Si el jugador está en rango y si se presiona la tecla F y si el panel de diálogo no está activo
        if (estaEnRango && Input.GetKeyDown(KeyCode.F))
        {
            if (!dialogoEmpezado && !almacen.monedaCogida)
            {
                textoPresiona.gameObject.SetActive(false);
                // Iniciar diálogo
                EmpezarDialogo();
            }
            else if (!almacen.monedaCogida && textoDialogo.text == lineasDialogo[indice]) //Si ha mostrado toda la línea pasa a la siguiente
            { 
                SiguienteLinea();
            }
            else if (!almacen.monedaCogida)// Adelantar líneas
            {
                StopAllCoroutines();
                textoDialogo.text = lineasDialogo[indice]; // Se muestra la linea completa
            }

            else if(!dialogoEmpezado && almacen.monedaCogida){
                textoPresiona.gameObject.SetActive(false);
                EmpezarDialogo();     
            } 
            else if (almacen.monedaCogida && textoDialogo.text == dialogoDespuesDeResuelto[indice2]){
                SiguienteLinea();
            } else {
                StopAllCoroutines();
                textoDialogo.text = dialogoDespuesDeResuelto[indice2];
            }
        }
    }

    public void EmpezarDialogo(){
        // El diálogo ha empezado
        dialogoEmpezado = true; 
        // El diálogo no ha terminado
        terminado = false;
        // Activar el panel de diálogo
        panelDialogo.SetActive(true);
        // Indice a 0
        indice = 0;
        indice2 = 0;
        // Escala de tiempo a 0 para evitar movimiento del jugador
        Time.timeScale = 0f;
        // Bloquear la camara cuando se inicia un dialogo
        GameObject.Find("Player").GetComponent<FPSCamera>().enabled = false;
        // Iniciar la corrutina para mostrar el texto letra por letra
        StartCoroutine(MostrarLinea());
    }

    private void SiguienteLinea(){      
        if (!almacen.monedaCogida){
            indice++;
        }else{
            terminado = indice2 == dialogoDespuesDeResuelto.Length;
            indice2 = terminado ? indice2: indice2 + 1;
        }

        if (indice < lineasDialogo.Length && !almacen.monedaCogida)
        {
            // Iniciar la corrutina para mostrar el texto letra por letra
            StartCoroutine(MostrarLinea());
        } else if (indice2 < dialogoDespuesDeResuelto.Length && almacen.monedaCogida){
            // Iniciar la corrutina para mostrar el texto letra por letra
            StartCoroutine(MostrarLinea());
        } else {
            // Desactivar el panel de diálogo
            panelDialogo.SetActive(false);
            // Escala de tiempo a 1 para reanudar el movimiento del jugador
            Time.timeScale = 1f;
            // Desbloquear la camara cuando se finaliza un dialogo
            GameObject.Find("Player").GetComponent<FPSCamera>().enabled = true;
            if (almacen.monedaCogida){
                itemRecompensa.inventory.AddItem(itemRecompensa);
            }else{
                // El diálogo ha terminado
                dialogoEmpezado = false;
            }
        }
    }

    private IEnumerator MostrarLinea(){
        textoDialogo.text = string.Empty;
        if(!almacen.monedaCogida){
            foreach(char ch in lineasDialogo[indice])
            {
                // se escribe el caracter
                textoDialogo.text += ch;
                // se espera un tiempo
                yield return new WaitForSecondsRealtime(tiempoEntreLetras);
            }
        } else {
            foreach(char ch in dialogoDespuesDeResuelto[indice2])
            {
                // se escribe el caracter
                textoDialogo.text += ch;
                inventory.DestroyItem();
                // se espera un tiempo
                yield return new WaitForSecondsRealtime(tiempoEntreLetras);
            }
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            estaEnRango = true;
            Debug.Log("Se puede iniciar un dialogo");
            textoPresiona.gameObject.SetActive(true);
        }
        if (other.CompareTag("Moneda"))
        {
            almacen.monedaCogida = true;
            //inventory.RemoveItem();
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player"))
        {
            estaEnRango = false;
            Debug.Log("No se puede iniciar un dialogo");
            textoPresiona.gameObject.SetActive(false);
        }
    }
}

