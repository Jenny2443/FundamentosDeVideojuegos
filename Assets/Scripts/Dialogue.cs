using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : MonoBehaviour
{
    // Referencia al panel de diálogo para activarlo y desactivarlo
    [SerializeField] private GameObject panelDialogo;
    // Referencia al panel de diálogo para activarlo y desactivarlo
    [SerializeField] private GameObject spritePersonaje;
    // Referencia al texto del diálogo para modificarlo
    [SerializeField] private TMP_Text textoDialogo;
    // Referencia el texto Presiona F
    [SerializeField] private TMP_Text textoPresiona;
    
    [SerializeField] private GameObject imagenSifo;
    [SerializeField] private GameObject imagenAmy;
    private GameObject personajeActual;


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
    
    //VArs para cambio de imagenes
    private int indiceCambioPersonajesDialogoSifo1 = 1;
    private bool dialogoSifo1Terminado = false;

    public GameObject player;
    public GameObject sifo;
  

    void Update(){
        // Calcula la dirección del vector desde la posición de sifo hasta la posición del jugador
        Vector3 direccionAlJugador = player.transform.position - sifo.transform.position;

        // Calcula la rotación necesaria para que sifo mire en la dirección del jugador (en el plano horizontal)
        Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(direccionAlJugador.x, 0, direccionAlJugador.z));

        // Aplica la rotación a sifo
        sifo.transform.rotation = rotacionDeseada;
        //almacen.torresResuelto = false;
        // Si el jugador está en rango y si se presiona la tecla F y si el panel de diálogo no está activo
        if (estaEnRango && Input.GetKeyDown(KeyCode.F))
        {
            if (!dialogoEmpezado && !almacen.torresResuelto)
            {
                textoPresiona.gameObject.SetActive(false);
                // Iniciar diálogo
                EmpezarDialogo();
            }
            else if (!almacen.torresResuelto && textoDialogo.text == lineasDialogo[indice]) //Si ha mostrado toda la línea pasa a la siguiente
            { 
                SiguienteLinea();
            }
            else if (!almacen.torresResuelto)// Adelantar líneas
            {
                StopAllCoroutines();
                textoDialogo.text = lineasDialogo[indice]; // Se muestra la linea completa
            }

            else if(!dialogoEmpezado && almacen.torresResuelto){
                textoPresiona.gameObject.SetActive(false);
                EmpezarDialogo();     
            } 
            else if (almacen.torresResuelto && textoDialogo.text == dialogoDespuesDeResuelto[indice2]){
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
        // Activar el sprite del personaje
        //spritePersonaje.SetActive(true);

        if (!dialogoSifo1Terminado)
        {
            personajeActual = imagenAmy;
        }
        else
        {
            personajeActual = imagenSifo;
        }
        
       
        personajeActual.SetActive(true);
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
        if (!almacen.torresResuelto){
            indice++;
        }else{
            terminado = indice2 == dialogoDespuesDeResuelto.Length;
            indice2 = terminado ? indice2: indice2 + 1;
        }

        if (indice < lineasDialogo.Length && !almacen.torresResuelto)
        {
            personajeActual.SetActive(false);
            Debug.Log("Antes de checkear siguiente personaje");
            checkPersonajeActual();

            personajeActual.SetActive(true);
            // Iniciar la corrutina para mostrar el texto letra por letra
            StartCoroutine(MostrarLinea());
        } else if (indice2 < dialogoDespuesDeResuelto.Length && almacen.torresResuelto){
            // Iniciar la corrutina para mostrar el texto letra por letra
            StartCoroutine(MostrarLinea());
        } else {
            // Desactivar el panel de diálogo
            panelDialogo.SetActive(false);
            // Desactivar el sprite del personaje
            spritePersonaje.SetActive(false);
            personajeActual.SetActive(false);
            dialogoSifo1Terminado = true;
            // Escala de tiempo a 1 para reanudar el movimiento del jugador
            Time.timeScale = 1f;
            // Desbloquear la camara cuando se finaliza un dialogo
            GameObject.Find("Player").GetComponent<FPSCamera>().enabled = true;
            if (almacen.torresResuelto){
                itemRecompensa.inventory.AddItem(itemRecompensa);
            }else{
                // El diálogo ha terminado
                dialogoEmpezado = false;
            }
        }
    }

    private void checkPersonajeActual()
    {
        if(indiceCambioPersonajesDialogoSifo1 == 2 || indiceCambioPersonajesDialogoSifo1 == 3 || indiceCambioPersonajesDialogoSifo1 == 5 || indiceCambioPersonajesDialogoSifo1 == 7 || 
           indiceCambioPersonajesDialogoSifo1 == 10 || indiceCambioPersonajesDialogoSifo1 == 12 || indiceCambioPersonajesDialogoSifo1 == 19 || indiceCambioPersonajesDialogoSifo1 == 22)
        {
            personajeActual = imagenAmy;
        }
        else
        {
            personajeActual = imagenSifo;
        }

        Debug.Log("Personaje actual cambiado a: " + personajeActual);
        indiceCambioPersonajesDialogoSifo1++;
    }

    private IEnumerator MostrarLinea(){
        textoDialogo.text = string.Empty;
        if(!almacen.torresResuelto){
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
