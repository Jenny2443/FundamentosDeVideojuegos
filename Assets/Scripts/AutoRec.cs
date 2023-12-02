using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class AutoRec : MonoBehaviour
{
    private int autoRec;
    // Start is called before the first frame update

    private void Awake(){
	    loadData();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy(){
	    saveData();
    }

    private void saveData(){
        PlayerPrefs.SetInt("autoRec", autoRec);
    }

    // autoRec se inicializa a 0
    private void loadData(){
        autoRec = PlayerPrefs.GetInt("autoRec", 0);
    }


}
