using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuOpciones;

    public GameObject recolAutoActivadaPausa;
    public GameObject recolAutoDesactivadaPausa;
    
    private bool juegoPausado = false;

    private void Start(){

        bool autoRec = PlayerPrefs.GetInt("autoRecolect") == 2;

        if(autoRec)
        {
            recolAutoActivadaPausa.SetActive(true);
            recolAutoDesactivadaPausa.SetActive(false);
        }
        else
        {
            recolAutoActivadaPausa.SetActive(false);
            recolAutoDesactivadaPausa.SetActive(true);
        }
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(juegoPausado){
                Reanudar();
            }else{
                Pausa();
            }
        }
    }
    public void Pausa()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Escenario Pruebas");
    }

    public void Opciones()
    {
        menuPausa.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void atras()
    {
        menuPausa.SetActive(true);
        menuOpciones.SetActive(false);
    }

    public void ActivarAuto(){
        PlayerPrefs.SetInt("autoRecolect", 2);
        recolAutoActivadaPausa.SetActive(true);
        recolAutoDesactivadaPausa.SetActive(false);
    }

    public void DesactivarAuto(){
        PlayerPrefs.SetInt("autoRecolect", 0);
        recolAutoActivadaPausa.SetActive(false);
        recolAutoDesactivadaPausa.SetActive(true);
    }
}