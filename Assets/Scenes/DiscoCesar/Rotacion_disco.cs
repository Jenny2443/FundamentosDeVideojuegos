// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Rotacion_disco : MonoBehaviour
// {
//     private bool estaEnRango;
//     private float grados = 30f;

//     private float anguloTotalRotado = 0f;

//     public VariablesGlobales almacen;
    
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(estaEnRango){
//             girar();
//         }
//     }

//     void girar(){
//         if(estaEnRango){
//             if(Input.GetKeyDown(KeyCode.F) && !almacen.giroCompleto && !almacen.primeraVez){
//                 gameObject.transform.Rotate(0, 0, Time.deltaTime * grados);
//                 anguloTotalRotado += Time.deltaTime * grados;
//                 if(anguloTotalRotado >= 90f){
//                     almacen.giroCompleto = true;
                    
//                 }
//             } 
//         }
//         Debug.Log("almacen es null");
//     }

    
//     private void OnTriggerStay(Collider other)
//     {
//         if (other.CompareTag("Roca"))
//         {
//             estaEnRango = true;
//         }
//     }

//     private void OnTriggerExit(Collider other){
//         if(other.CompareTag("Roca")){
//             estaEnRango = false;
//         }
//     }

// }
