using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : MonoBehaviour
{
    // Referencia al panel de diálogo para activarlo y desactivarlo
    [SerializeField] private GameObject panelDialogo;
    // Referencia al texto del diálogo para modificarlo
    [SerializeField] private TMP_Text textoDialogo;
    // Referencia el texto Presiona F
    [SerializeField] private TMP_Text textoPresiona;

    // string de las lineas de dialogo, textArea(min y max espacio vertical a mostrar): min 4 líneas, max: 6 líneas
    [SerializeField, TextArea(4,6)] private string[] lineasDialogo;

    // variable para el tiempo de tipado
    [SerializeField] private float tiempoEntreLetras = 0.05f;

    // variable para saber si el jugador está en rango
    public bool estaEnRango = false;
    // variable para saber si el dialogo ha empezado
    public bool dialogoEmpezado;
    private int indice;

    void Update(){
        // Si el jugador está en rango y si se presiona la tecla F y si el panel de diálogo no está activo
        if (estaEnRango && Input.GetKeyDown(KeyCode.F))
        {
            if (!dialogoEmpezado)
            {
                textoPresiona.gameObject.SetActive(false);
                // Iniciar diálogo
                EmpezarDialogo();
            }
            else if (textoDialogo.text == lineasDialogo[indice]) //Si ha mostrado toda la línea pasa a la siguiente
            { 
                SiguienteLinea();
            }
            else // Adelantar líneas
            {
                StopAllCoroutines();
                textoDialogo.text = lineasDialogo[indice]; // Se muestra la linea completa
            }
        }
    }

    public void EmpezarDialogo(){
        // El diálogo ha empezado
        dialogoEmpezado = true; 
        // Activar el panel de diálogo
        panelDialogo.SetActive(true);
        // Indice a 0
        indice = 0;
        // Escala de tiempo a 0 para evitar movimiento del jugador
        Time.timeScale = 0f;
        // Bloquear la camara cuando se inicia un dialogo
        GameObject.Find("Player").GetComponent<FPSCamera>().enabled = false;
        // Iniciar la corrutina para mostrar el texto letra por letra
        StartCoroutine(MostrarLinea());
    }

    private void SiguienteLinea(){
        indice++;
        if (indice < lineasDialogo.Length)
        {
            // Iniciar la corrutina para mostrar el texto letra por letra
            StartCoroutine(MostrarLinea());
        } else {
            // Desactivar el panel de diálogo
            panelDialogo.SetActive(false);
            // El diálogo ha terminado
            dialogoEmpezado = false;
            // Escala de tiempo a 1 para reanudar el movimiento del jugador
            Time.timeScale = 1f;
            // Desbloquear la camara cuando se finaliza un dialogo
            GameObject.Find("Player").GetComponent<FPSCamera>().enabled = true;
        }
    }

    private IEnumerator MostrarLinea(){
        textoDialogo.text = string.Empty;
        foreach(char ch in lineasDialogo[indice])
        {
            // se escribe el caracter
            textoDialogo.text += ch;
            // se espera un tiempo
            yield return new WaitForSecondsRealtime(tiempoEntreLetras);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            estaEnRango = true;
            Debug.Log("Se puede iniciar un dialogo");
            textoPresiona.gameObject.SetActive(true);
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
