using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosCompartidos : MonoBehaviour
{
    public bool autorecoleccion;
    // Start is called before the first frame update
    void Start()
    {
        autorecoleccion = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
