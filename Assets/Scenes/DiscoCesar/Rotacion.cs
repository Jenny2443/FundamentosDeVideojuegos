    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class Rotacion : MonoBehaviour
    {
        [SerializeField] private TMP_Text textoPresiona;

        public Item discoInterno;
        public Item recompensa3;
        private bool discoCogido;
        private bool estaEnRango;

        private float anguloTotalRotado = 0f;
        private float grados = 360f;

        private bool giroCompleto = false;
        private bool primeraVez = true;
        private bool recompensaCogida = false;

        public Inventory inventory;
        public VariablesGlobales almacen;

        float timer = 0f;

        

        // Start is called before the first frame update
        void Start()
        {
            //Dejamos unicamente visual el estado 0 (roca cifrada)
            //ocultamos estado 1 (disco interno), estado 2 (disco externo) y estado 3 (roca descifrada)
            for (int i = 1; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(estaEnRango){
            rotar();
            }
        }

        void rotar(){
            // if(discoCogido){
            //     Debug.Log("discoCogido");
                
            //     if(inventory != null)
            //         inventory.clickes.SetActive(false);
            //     else
            //         Debug.Log("inventory es null");
            // }

            //Si es la primera vez que pulsa F, se destruye el disco interno y se muestra el disco interno (estado 2)
            if(Input.GetKeyDown(KeyCode.F) && primeraVez){
                Debug.Log("F pulsada");
                primeraVez = false;
                //discoCogido = false;
                // if (discoInterno == null){
                //     Debug.Log("discoInterno es null");
                // } else if (discoInterno.inventory == null){
                //     Debug.Log("discoInterno.inventory es null");
                // }else{
                discoInterno.inventory.DestroyItem();
                transform.GetChild(1).gameObject.SetActive(true);
                    
                // }
                    
            }

            //Si no es la primera vez que pulsa F, se gira el disco interno
            if(Input.GetKeyDown(KeyCode.F) && !giroCompleto && !primeraVez){
                gameObject.transform.GetChild(1).Rotate(0, 0, Time.deltaTime * grados);
                anguloTotalRotado += Time.deltaTime * grados;
                if(anguloTotalRotado >= 60f){
                    giroCompleto = true;
                    
                }
            } 
            //Si ha dado el giro completo, se muestra la roca descifrada (estado 3)
            if(giroCompleto && !recompensaCogida){
                Debug.Log("giroCompleto");
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(true);
                recompensa3.inventory.AddItem(recompensa3);
                recompensaCogida = true;

            }
            // if (Input.GetKeyDown(KeyCode.Mouse0) && !discoCogido)
            // {
            //     Debug.Log("Mouse0 pulsado");
            //     discoInterno.inventory.AddItem(discoInterno);
            //     discoCogido = true;
            // }
        }

        void resolver() {
            transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).Rotate(0, 0, 60f);
            Debug.Log("giroCompleto");
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            recompensa3.inventory.AddItem(recompensa3);
            recompensaCogida = true;
            int pos = inventory.contains("DiscoAlbertiPequeno");
            if (pos != -1)
                inventory.RemoveItem(pos);
            discoInterno.gameObject.SetActive(false);
    }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Brazo"))
            {
                if (almacen.puckResuelto && PlayerPrefs.GetInt("skipMechanics") == 3 && Input.GetKey(KeyCode.P))
                {
                    timer += Time.deltaTime;
                    Debug.Log(timer);
                    if (timer >= 3)
                    {
                        resolver();

                        timer = 0f;

                    }
                }
            }
            //     if (other.CompareTag("Player"))
            //     {
            //         // if (!discoCogido)
            //         //     inventory.clickes.SetActive(true);
            //         if (discoCogido && !almacen.giroCompleto) { 
            //             textoPresiona.gameObject.SetActive(true); 
            //         }
            //         // if (inventory != null && inventory.getInventoryItem(inventory.getNowActive()) != null)
            //         // {
            //         //     if (inventory.getInventoryItem(inventory.getNowActive()).CompareTag("DiscoAlbertiPequeno"))
            //         //     {
            //         //         discoCogido = true;
            //         //     }
            //         // }
            //         estaEnRango = true;
            //     }
            // }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Item currentItem = inventory.getInventoryItem(inventory.getNowActive());

            if (other.CompareTag("DiscoAlbertiPequeno"))
            {
                discoCogido = true;
            }
            if (other.CompareTag("Player"))
            {
                if (!giroCompleto) { 
                    textoPresiona.gameObject.SetActive(true); 
                }
                estaEnRango = true;
            }
        }

        private void OnTriggerExit(Collider other){
            // Item currentItem = inventory.getInventoryItem(inventory.getNowActive());
            discoCogido = false;
            estaEnRango = false;
            Debug.Log("No se puede dejar el disco");
            // inventory.clickes.SetActive(false);
            textoPresiona.gameObject.SetActive(false);
            if (other.CompareTag("Brazo")){
                timer = 0f;
            }
        }
    }
