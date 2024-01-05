using UnityEngine;
using TMPro;

public class PuzzlePuertaFinal : MonoBehaviour
{
    // Mensaje de interfaz, puzzle completado
    [SerializeField] private TMP_Text textoCompletado;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoCompletado.gameObject.SetActive(true);
        }
    }
}
