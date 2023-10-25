using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Lugar de almacenamiento de los items
    public Item[] inventory = new Item[10];
    
    //Lugar de almacenamiento de los sprites de los items, así como su aparicion por pantalla
    public GameObject[] ui_inventory = new GameObject[10];

    //Para a aparicion de los numeros y saber sobre cual celda del inventario estás
    public GameObject[] ui_inventory_active = new GameObject[10];
    private int nowActive = 0;

    private Color transparencia;
    public GameObject clickes, InventarioLleno;

    public ItemEnMano enMano;

    void Start()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = null;

            ui_inventory_active[i].SetActive(false);
        }
        transparencia.a = 255;
        transparencia.r = 255;
        transparencia.g = 255;
        transparencia.b = 255;
        clickes.SetActive(false);
        InventarioLleno.SetActive(false);
    }

    void Update()
    {
        this.GetItem();
    }

    //Llama a esta funcion cuando pulsas una tecla del inventario. Enciende el numero de la celda de sobre la que estás
    // y aparece dicho item en tu "mano"
    public void GetItem ()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[1].SetActive(true);
            nowActive = 1;
            enMano.PonerEnMano(inventory[1]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[2].SetActive(true);
            nowActive = 2;
            enMano.PonerEnMano(inventory[2]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[3].SetActive(true);
            nowActive = 3;
            enMano.PonerEnMano(inventory[3]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[4].SetActive(true);
            nowActive = 4;
            enMano.PonerEnMano(inventory[4]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[5].SetActive(true);
            nowActive = 5;
            enMano.PonerEnMano(inventory[5]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[6].SetActive(true);
            nowActive = 6;
            enMano.PonerEnMano(inventory[6]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[7].SetActive(true);
            nowActive = 7;
            enMano.PonerEnMano(inventory[7]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[8].SetActive(true);
            nowActive = 8;
            enMano.PonerEnMano(inventory[8]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
        {
            ui_inventory_active[nowActive].SetActive(false);
            ui_inventory_active[9].SetActive(true);
            nowActive = 9;
            enMano.PonerEnMano(inventory[9]);
        }
        /*if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            ui_inventory_active[nowActive].SetActive(false);
            nowActive = 0;
            StartCoroutine(Avisos(ui_inventory_active[0]));
        }*/
    }

    //Recibe un item, y lo mete al array de inventario[], la primera posicion libre.
    //Además, dibuja en el ui, en la celda correspondiente, el sprite del item.
    public void AddItem (Item item)
    {
        bool metido = false;
        for(int i = 1; i < inventory.Length && !metido; i++)
        {
            if (inventory[i] == null)
            {
                // Meter el item al inventario
                inventory[i] = item;
                enMano.PonerEnMano(inventory[i]);

                //Meter la imagen al inventario
                ui_inventory[i].GetComponent<Image>().color = transparencia;
                ui_inventory[i].GetComponent<Image>().sprite = item.itemIcon;

                metido = true;

                ui_inventory_active[nowActive].SetActive(false);
                ui_inventory_active[i].SetActive(true);
                nowActive = i;
            }
        }
        if (!metido) {
            StartCoroutine(Avisos(InventarioLleno));
        }
    }

    //Aún no está hecho.... Para la tarea de poner objetos
    public void RemoveItem (int index)
    {
        if(index > 0 && index < inventory.Length)
        {
            inventory[index] = null;
        }
        else
        {
            Debug.Log("Index no valido");
        }
    }

    //Para el mensaje de NullPointerException e Inventario lleno.
    private IEnumerator Avisos(GameObject other)
    {
        other.SetActive(true);
        yield return new WaitForSeconds(1f);
        other.SetActive(false);
    }

    public int getNowActive()
    {
        return nowActive;
    }

    public Item getInventoryItem(int i)
    {
        return inventory[i];
    }
}
