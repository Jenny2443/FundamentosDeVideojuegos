using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzleBola2 : MonoBehaviour
{
    [SerializeField] private TMP_Text textoGanador;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Esmeralda"))
        {
            Debug.Log("Esmeralda en hueco");
            textoGanador.gameObject.SetActive(true);

            // TODO Volver a la escena principal
        }
    }
}
