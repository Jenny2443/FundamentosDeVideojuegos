using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Inventory inventory;
    public Sprite itemIcon;
    private bool jugadorEnContacto;

    void Start()
    {
        jugadorEnContacto = false;
    }

    void Update()
    {
        Add();
    }

    private void Add()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && jugadorEnContacto == true)
        {
            this.gameObject.SetActive(false);
            inventory.clickes.SetActive(false);
            inventory.AddItem(this);
            jugadorEnContacto = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.clickes.SetActive(true);
            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        jugadorEnContacto = false;
        inventory.clickes.SetActive(false);
    }

}