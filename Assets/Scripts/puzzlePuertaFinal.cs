using UnityEngine;
using TMPro;

public class PuzzlePuertaFinal : MonoBehaviour
{
    // Mensaje de interfaz, puzzle completado
    [SerializeField] private TMP_Text textoCompletado;

    public VariablesGlobales almacen;

    // Variables de las piezas del puzzle obtenidas de ColocarPiezas
    /*public ColocarPiezas1 colocarPiezas1;
    public ColocarPiezas2 colocarPiezas2;
    public ColocarPiezas3 colocarPiezas3;*/

    private void Start()
    {
        // Buscar las instancias de las clases ColocarPiezas
        /*colocarPiezas1 = FindObjectOfType<ColocarPiezas1>();
        colocarPiezas2 = FindObjectOfType<ColocarPiezas2>();
        colocarPiezas3 = FindObjectOfType<ColocarPiezas3>();*/
    }

    private void Update()
    {
        // Verificar si el puzzle está completado
        PuzzleCompletado();
    }

    private void PuzzleCompletado()
    {
        // Verificar si todas las piezas del puzzle están colocadas
        if (almacen.pieza1Colocada && almacen.pieza2Colocada && almacen.pieza3Colocada)
        {
            // Imprimir un mensaje en la consola
            Debug.Log("¡JUEGO COMPLETADO!");

            // Activar el mensaje de interfaz, puzzle completado
            textoCompletado.gameObject.SetActive(true);
        }
    }
}
