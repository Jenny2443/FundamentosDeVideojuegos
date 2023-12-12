using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolverGema : MonoBehaviour
{
    float timer = 0f;

    public GameObject gema;
    public GameObject agujero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brazo") && PlayerPrefs.GetInt("skipMechanics") == 3)
            timer = 0f;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Brazo") && PlayerPrefs.GetInt("skipMechanics") == 3)
        {
            if (Input.GetKey(KeyCode.P))
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (timer >= 3)
                {
                    resolver();

                    timer = 0f;

                }
            }
            else
            {
                timer = 0f;
            }
        }
    }

    public void resolver() {
        gema.transform.position = new Vector3(agujero.transform.position.x, agujero.transform.position.y, agujero.transform.position.z);
    }
}
