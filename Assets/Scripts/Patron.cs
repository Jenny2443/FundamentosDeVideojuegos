using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Patron : MonoBehaviour
{
    [SerializeField] private TMP_Text textoPresiona;
    private bool estaEnRango; 
    private Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        //this.transform.GetChild(0).GetChild(0).SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        myLight = GameObject.FindGameObjectWithTag("light").GetComponent<Light>();
        myLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(estaEnRango && Input.GetKeyDown(KeyCode.F)){
            //this.transform.GetChild(0).GetChild(0).SetActive(true);
            myLight.enabled = true;
            this.transform.GetChild(1).gameObject.SetActive(true);
            
/*        }else if (estaEnRango && !Input.GetKeyDown(KeyCode.F)){
            luz.disable();
            this.transform.GetChild(1).gameObject.SetActive(false);
*/        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            estaEnRango = true;
            textoPresiona.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player"))
        {
            estaEnRango = false;
            textoPresiona.gameObject.SetActive(false);
        }
    }
}
