using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PuzleBola : MonoBehaviour
{
    [SerializeField] private TMP_Text textoGanador;

    public Item itemRecompensa; // Asigna el objeto de recompensa 
    bool fin = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Esmeralda") && !fin)
        {
            Debug.Log("Esmeralda en hueco");
            //textoGanador.gameObject.SetActive(true);
            itemRecompensa.inventory.AddItem(itemRecompensa);
            fin = true;
            // TODO Volver a la escena principal
        }
    }
}