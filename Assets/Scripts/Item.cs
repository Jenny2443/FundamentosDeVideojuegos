using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Inventory inventory;
    public Sprite itemIcon;
    public bool jugadorEnContacto, sujeto;
    void Start()
    {
        jugadorEnContacto = false;
        sujeto = false;
    }

    void Update()
    {
        Add();
    }

    public void Add()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && jugadorEnContacto == true)
        {  
            inventory.clickes.SetActive(false);
            inventory.AddItem(this);
            jugadorEnContacto = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.GetInt("autoRecolect") == 2)
        {
            if (other.CompareTag("Brazo") && !sujeto)
            {
                Debug.Log("Holasssss");
                inventory.AddItem(this);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Brazo") && sujeto == false)
        {
            inventory.clickes.SetActive(true);
            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brazo"))
        {
            jugadorEnContacto = false;
            inventory.clickes.SetActive(false);
        }
    }

}