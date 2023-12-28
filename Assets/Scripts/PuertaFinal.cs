using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    public Animator puerta1;
    public Animator puerta2;
    public VariablesGlobales globals;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(globals.pieza1Colocada && globals.pieza2Colocada && globals.pieza3Colocada)
        {
            puerta1.SetBool("BoolEjeP1", true);
            puerta2.SetBool("BoolEjeP2", true);
            audioSource.Play();
        }

    }
}
