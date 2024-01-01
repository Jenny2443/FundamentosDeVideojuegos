using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicButton : MonoBehaviour
{
    float timer;

    bool enRango;
    bool resuelto;

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
        if (PlayerPrefs.GetInt("skipMechanics") == 3)
        {
            this.gameObject.SetActive(true);
        }
        else {
            this.gameObject.SetActive(false);
        }


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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brazo"))
        {
            Debug.Log("enRango = true");
            enRango = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brazo"))
        {
            Debug.Log("enRango = false");
            enRango = false;
        }
    }

    private void resolver() {
        Debug.Log("resuelto");
        animator.SetTrigger("TriggerGirar");
        resuelto = true;
    }
}
