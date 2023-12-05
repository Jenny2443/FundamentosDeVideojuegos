using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Duende : MonoBehaviour
{

    // Referencia al panel de diálogo para activarlo y desactivarlo
    [SerializeField] private GameObject panelDialogo;
    // Referencia al panel de diálogo para activarlo y desactivarlo
    [SerializeField] private GameObject spritePersonaje;
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
    
    private int indiceCambioPersonajesDialogoPuck = 1;
    private int indiceCambioPersonajesDialogoPuck2 = 1;
    private bool dialogoPuckTerminado = false;
    [SerializeField] private GameObject imagenPuck;
    private GameObject personajeActual;
    [SerializeField] private GameObject imagenAmy;
    
    public GameObject player;
    public GameObject puck;
    
    //Variables para comprobar si estamos esperando respuesta del jugador
    private bool esperandoRespuesta = false;


    void Update(){
 
        if (estaEnRango)
        {
            // Calcula la dirección del vector desde la posición de sifo hasta la posición del jugador
            Vector3 direccionAlJugador = player.transform.position - puck.transform.position;

            // Calcula la rotación necesaria para que sifo mire en la dirección del jugador (en el plano horizontal)
            Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(direccionAlJugador.x, 0, direccionAlJugador.z));

            // Aplica la rotación a sifo
            puck.transform.rotation = rotacionDeseada;   
        }
        //almacen.monedaCogida = false;
        // Si el jugador está en rango y si se presiona la tecla F y si el panel de diálogo no está activo
        if (estaEnRango && Input.GetKeyDown(KeyCode.F) && !esperandoRespuesta)
        {
            almacen.enDialogo = true;
            panelDialogo.SetActive(true);
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
        }else if (esperandoRespuesta)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("Ha presionado N");
                esperandoRespuesta = false;
                StopAllCoroutines();
                SiguienteLinea();
            }else if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log("Ha presionado Y");
                esperandoRespuesta = false;
                indice = 9;
                indiceCambioPersonajesDialogoPuck = 10;
                StopAllCoroutines();
                SiguienteLinea();
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
        personajeActual = imagenPuck;
        personajeActual.SetActive(true);
        // Indice a 0
        indice = 0;
        indice2 = 0;
        // Escala de tiempo a 0 para evitar movimiento del jugador
        Time.timeScale = 0f;

        esperandoRespuesta = false;
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
        
 
        Debug.Log("Indice: " + indice);
        Debug.Log("Lineas dialogo length: " + lineasDialogo.Length);

        if (indice < lineasDialogo.Length && !almacen.monedaCogida)
        {
            personajeActual.SetActive(false);
            checkPersonajeActual();
            personajeActual.SetActive(true);
            // Iniciar la corrutina para mostrar el texto letra por letra
            StartCoroutine(MostrarLinea());
            if (indice == 11)
            {
                esperandoRespuesta = true;
                return;
            }
        } else if (indice2 < dialogoDespuesDeResuelto.Length && almacen.monedaCogida){
            personajeActual.SetActive(false);
            checkPersonajeActualResuelto();
            personajeActual.SetActive(true);
            // Iniciar la corrutina para mostrar el texto letra por letra
            StartCoroutine(MostrarLinea());
        } else {
            // Desactivar el panel de diálogo
            panelDialogo.SetActive(false);
            // Desactivar el sprite del personaje
            //spritePersonaje.SetActive(false);
            personajeActual.SetActive(false);
            almacen.enDialogo = false;
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
    private void checkPersonajeActual()
    {
        if(indiceCambioPersonajesDialogoPuck == 1 || indiceCambioPersonajesDialogoPuck == 3 || indiceCambioPersonajesDialogoPuck == 6 || indiceCambioPersonajesDialogoPuck == 8 || 
           indiceCambioPersonajesDialogoPuck == 12)
        {
            personajeActual = imagenAmy;
        }
        else
        {
            personajeActual = imagenPuck;
        }

        Debug.Log("Personaje actual cambiado a: " + personajeActual + "por indice: " + indiceCambioPersonajesDialogoPuck);
        indiceCambioPersonajesDialogoPuck++;
    }

    private void checkPersonajeActualResuelto()
    {
        if (indiceCambioPersonajesDialogoPuck2 == 2 || indiceCambioPersonajesDialogoPuck2 == 6)
        {
            personajeActual = imagenAmy;
        }
        else
        {
            personajeActual = imagenPuck;
        }

        indiceCambioPersonajesDialogoPuck2++;
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

    private void OnTriggerStay(Collider other){
        if (other.CompareTag("Brazo"))
        {
            estaEnRango = true;
            Debug.Log("Se puede iniciar un dialogo");
            textoPresiona.gameObject.SetActive(true);
        }
        if (other.CompareTag("Moneda"))
        {
            almacen.monedaCogida = true;
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Brazo"))
        {
            estaEnRango = false;
            Debug.Log("No se puede iniciar un dialogo");
            textoPresiona.gameObject.SetActive(false);
        }
        if (other.CompareTag("Moneda"))
        {
            almacen.monedaCogida = false;
        }
    }
}

