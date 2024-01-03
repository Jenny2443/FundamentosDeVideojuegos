using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    public Animator puerta1;
    public Animator puerta2;
    public VariablesGlobales globals;
    private AudioSource audioSource;
    [SerializeField] private AudioClip fin;

    public GameObject[]list = new GameObject[2];
    private bool unaVez = false;

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
            if (!unaVez)
            {
                list[0].gameObject.SetActive(true);
                list[1].gameObject.SetActive(false);
                Invoke("VolverCamara", 3.3f);
                unaVez = true;
            }
            puerta1.SetBool("BoolEjeP1", true);
            puerta2.SetBool("BoolEjeP2", true);
            //audioSource.Play();
            audioSource.PlayOneShot(fin);
        }

    }

    private void VolverCamara()
    {
        list[0].gameObject.SetActive(false);
        list[1].gameObject.SetActive(true);
    }
}
