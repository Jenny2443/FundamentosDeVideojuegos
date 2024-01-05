using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{

    public GameObject padre;
    public DragAndDrop scriptPadre;

    // Start is called before the first frame update
    void Start()
    {
        padre = transform.parent.gameObject;
        scriptPadre = padre.GetComponent<DragAndDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Casillas"))
        {
            Debug.Log("New anchor point");

            scriptPadre.anchorPoint = other.gameObject;
        }
    }
}
