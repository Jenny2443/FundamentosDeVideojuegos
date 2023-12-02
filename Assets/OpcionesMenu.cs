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

    // Start is called before the first frame update
    void Start()
    {
        recolAutoActivada = GameObject.FindGameObjectWithTag("ActivadaRecAuto");
        recolAutoDesactivada = GameObject.FindGameObjectWithTag("DesactivadaRecAuto");

        bool autoRec = PlayerPrefs.GetInt("autoRecolect") == 2;

        if(autoRec)
        {
            recolAutoActivada.SetActive(true);
            recolAutoDesactivada.SetActive(false);
        }
        else
        {
            recolAutoActivada.SetActive(false);
            recolAutoDesactivada.SetActive(true);
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

    public void atras(){
        SceneManager.LoadScene("Menú Principal");
    }
}
