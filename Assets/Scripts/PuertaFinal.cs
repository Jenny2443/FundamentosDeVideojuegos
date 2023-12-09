using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    public Animator puerta1;
    public Animator puerta2;

    // Start is called before the first frame update
    void Start()
    {
        puerta2.SetBool("BoolEjeP2", true);
        puerta1.SetBool("BoolEjeP1", true);
        
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("");
        //puerta1.SetBool("sePuede1", false);
        //puerta2.SetBool("sePuede", false);
    }
}
