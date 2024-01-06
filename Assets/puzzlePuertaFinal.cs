using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PuzzlePuertaFinal : MonoBehaviour
{
    // Mensaje de interfaz, puzzle completado
    [SerializeField] private TMP_Text textoCompletado;
    private bool primeraVez = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && primeraVez)
        {
            primeraVez = false;
            textoCompletado.gameObject.SetActive(true);
            Invoke("Final", 3f);
        }
    }

    private void Final()
    {
        textoCompletado.gameObject.SetActive(false);
        SceneManager.LoadScene("Menu final");
    }
}
