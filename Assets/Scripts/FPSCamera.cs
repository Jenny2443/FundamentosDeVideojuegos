using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    private new Transform camera;
    public VariablesGlobales almacen;

    public Vector2 sensibility;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform.Find("CameraMain");
        //Para que el cursor no se salga del juego
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!almacen.cameraLocked){
            float hor = Input.GetAxis("Mouse X");
            float ver = Input.GetAxis("Mouse Y");
            //Giro
            if(hor != 0){
                transform.Rotate(Vector3.up * hor * sensibility.x);
            }

            if (ver != 0){
                //camera.Rotate(Vector3.left * ver * sensibility.y);
                float angle = (camera.localEulerAngles.x - ver * sensibility.y + 360) % 360;
                if(angle > 180) { angle -= 360; }
                angle = Mathf.Clamp(angle, -80, 80);
                camera.localEulerAngles = Vector3.right * angle;
            }   
        }

    }
}
