using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ItemEnMano : MonoBehaviour
{
    public GameObject handPoint;
    private Item pickedObject = null;
    private Vector3 posInit;

    /* El problema de cómo lo he hecho reside en que el array del inventario, solo recoge la referencia
     * al item. Cuando hago inventory[i] = item, sólo está marcando el lugar en memoria de donde se encuentra
     * el item, en vez de crearse una copia y guardarlo. Por ello, aqui, si lo quiero poner en la mano,
     * debo usar el item en sí, jugando con su posicion, y si cojo otra cosa, pongo lo que tenia en la mano en 
     * otro lugar, y lo desactivo*/
    public void PonerEnMano(Item item)
    {
        //Si tenemos algo en la mano, lo "quitamos" (Lo metemos a otro lugar, desactivandole)
        if (pickedObject != null)
        {
            pickedObject.transform.position = posInit;
            //pickedObject.GetComponent <Rigidbody>().isKinematic = false;
            pickedObject.transform.SetParent(null);
            pickedObject.gameObject.SetActive(false);
            pickedObject.sujeto = false;
            pickedObject = null;
        }

        //Si el item pasado es nulo (cuando estamos en una celda vacia el inventario)
        if(item == null)
        {
            pickedObject = null;
        }
        else  //Si no, activamos el item y lo cogemos en la mano
        {
            item.gameObject.SetActive(true);
            item.sujeto = true;
            pickedObject = item;
            //pickedObject.GetComponent<Rigidbody>().isKinematic = true;
            pickedObject.transform.position = handPoint.transform.position;
            pickedObject.transform.SetParent(handPoint.transform);
        }
    }
}