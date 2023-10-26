using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private new Rigidbody rigidbody;

    public float movementSpeed = 1f;
    
    //Contador creado para test
    public int count = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //movementSpeed = 1f;
        count = 1;
    }



    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        //
        // Vector3 velocity = Vector3.zero;
        //
        // if(hor != 0 || ver != 0){
        //     Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
        //
        //     velocity = direction * movementSpeed;
        // }
        //
        // velocity.y = rigidbody.velocity.y;
        //
        // rigidbody.velocity = velocity;
        //MovePlayer(hor, ver);
        rigidbody.velocity = MovePlayer(hor, ver);

    }

    //Funcion que toma los valores de movimiento y ejecuta el movimiento
    public Vector3 MovePlayer(float hor, float ver)
    {
        //Debug.Log("Pos inicial: " + transform.position);
        Vector3 velocity = Vector3.zero;
        
        if(hor != 0 || ver != 0){
            //Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
            Vector3 direction = (transform.forward * ver + transform.right * hor);
            //Debug.Log("Direction: " + direction);
            // Debug.Log("Direction: " + direction);
            velocity = direction * movementSpeed;
            //Debug.Log("Velocity: " + velocity);
        }

        velocity.y = rigidbody.velocity.y;

        //rigidbody.velocity = velocity;
        return velocity;
    }
}
