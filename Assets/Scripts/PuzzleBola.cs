using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PuzleBola : MonoBehaviour
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