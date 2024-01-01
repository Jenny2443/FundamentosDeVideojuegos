using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicButton : MonoBehaviour
{
    float timer;

    bool enRango;
    bool resuelto;

    public VariablesGlobales almacen;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        enRango = false;
        resuelto = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(string.Concat(enRango, PlayerPrefs.GetInt("skipMechanics") == 3, Input.GetKey(KeyCode.P)));
        if (enRango && PlayerPrefs.GetInt("skipMechanics") == 3 && Input.GetKey(KeyCode.P) && !resuelto)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= 3)
            {
                resolver();

                timer = 0f;

            }
        }
        if(enRango && PlayerPrefs.GetInt("skipMechanics") == 3 && Input.GetKeyUp(KeyCode.P))
        {
            timer = 0f;
        }
        Debug.Log(enRango);
        if(enRango)
            almacen.CirculoP.fillAmount = timer / 3f;
        if(resuelto && enRango)
            almacen.p.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Brazo"))
        {
            Debug.Log("enRango = true");
            enRango = true;
            if (PlayerPrefs.GetInt("skipMechanics") == 3)
            {
                if(almacen.cameraLocked)
                    almacen.p.SetActive(false);
                else
                    almacen.p.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brazo"))
        {
            Debug.Log("enRango = false");
            enRango = false;
            if (PlayerPrefs.GetInt("skipMechanics") == 3) {
                almacen.p.SetActive(false);
                timer = 0;
                almacen.CirculoP.fillAmount = timer / 3f;
            }
        }
    }

    private void resolver() {
        Debug.Log("resuelto");
        animator.SetTrigger("TriggerGirar");
        resuelto = true;
    }
}
