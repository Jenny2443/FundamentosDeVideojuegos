using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpcionesMenu : MonoBehaviour
{
    public GameObject recolAutoActivada;
    public GameObject recolAutoDesactivada;
    public GameObject saltarMecanicasActivada;
    public GameObject saltarMecanicasDesactivada;

    // Start is called before the first frame update
    void Start()
    {
        recolAutoActivada = GameObject.FindGameObjectWithTag("ActivadaRecAuto");
        recolAutoDesactivada = GameObject.FindGameObjectWithTag("DesactivadaRecAuto");

        saltarMecanicasActivada = GameObject.FindGameObjectWithTag("ActivadaSkipMec");
        saltarMecanicasDesactivada = GameObject.FindGameObjectWithTag("DesactivadaSkipMec");

        bool autoRec = PlayerPrefs.GetInt("autoRecolect") == 2;
        bool skipMec = PlayerPrefs.GetInt("skipMechanics") == 3;

        if (autoRec)
        {
            recolAutoActivada.SetActive(true);
            recolAutoDesactivada.SetActive(false);
        }
        else
        {
            recolAutoActivada.SetActive(false);
            recolAutoDesactivada.SetActive(true);
        }

        if (skipMec)
        {
            saltarMecanicasActivada.SetActive(true);
            saltarMecanicasDesactivada.SetActive(false);
        }
        else{
            saltarMecanicasDesactivada.SetActive(false);
            saltarMecanicasActivada.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarAuto(){
        PlayerPrefs.SetInt("autoRecolect", 2);
        recolAutoActivada.SetActive(true);
        recolAutoDesactivada.SetActive(false);
    }

    public void DesactivarAuto(){
        PlayerPrefs.SetInt("autoRecolect", 0);
        recolAutoActivada.SetActive(false);
        recolAutoDesactivada.SetActive(true);
    }

    public void ActivarSkip()
    {
        PlayerPrefs.SetInt("skipMechanics", 3);
        saltarMecanicasDesactivada.SetActive(false);
        saltarMecanicasActivada.SetActive(true);
    }

    public void DesactivarSkip()
    {
        PlayerPrefs.SetInt("skipMechanics", 0);
        saltarMecanicasDesactivada.SetActive(true);
        saltarMecanicasActivada.SetActive(false);
    }

    public void atras(){
        SceneManager.LoadScene("Menú Principal");
    }
}
