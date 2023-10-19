using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//TODO: Test para comprobar movimiento en eje z (HECHO)
//TODO: Test para comprobar que no atraviesa un objeto (arbol o roca), simplemente se queda en el sitio

public class PlayerMovementTest
{
    private PlayerControler player;
    private GameObject playerObject;

    [SetUp]
    public void SetUp()
    {
        //Creamos el objeto del player a partir del prefab
        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        GameObject playerObject = MonoBehaviour.Instantiate(playerPrefab);
        
        //Obtenemos el componente PlayerControler del player
        player = playerObject.GetComponent<PlayerControler>();
        //Desactivamos la gravedad
        player.GetComponent<Rigidbody>().useGravity = false;
    }
    
    //Borrar objetos despues de los tests
    [TearDown]
    public void Teardown()
    {
        if (player != null)
        {
            GameObject.Destroy(player.gameObject);   
        }
    }

    //Test para comprobar que se crea el jugador correctamente
    [UnityTest]
    public IEnumerator PlayerIsCreatedProperly()
    {
        yield return new WaitForSeconds(0.1f);  
        Assert.AreEqual(1, player.count);
    }

    //Test para comprobar que el jugador se mueve correctamente en el eje x (horizontal) positivo
    [UnityTest]
    public IEnumerator MovePlayerTestHorizontalPositive()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 movement = player.MovePlayer(2, 0);
        yield return new WaitForSeconds(0.1f);
        Vector3 valorEsperado = new Vector3(2, 0, 0);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }

    //Test para comprobar que el jugador se mueve correctamente en el eje x (horizontal) positivo
    [UnityTest]
    public IEnumerator MovePlayerTestHorizontalNeg()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 movement = player.MovePlayer(-6, 0);
        yield return new WaitForSeconds(0.1f);
        Vector3 valorEsperado = new Vector3(-6, 0, 0);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en el eje z positivo hacia delante
    [UnityTest]
    public IEnumerator MovePlayerTestForward()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 movement = player.MovePlayer(0, 7);
        yield return new WaitForSeconds(0.1f);
        Vector3 valorEsperado = new Vector3(0, 0, 7);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en el eje z negativo hacia atras
    [UnityTest]
    public IEnumerator MovePlayerTestBackWards()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 movement = player.MovePlayer(0, -10);
        yield return new WaitForSeconds(0.1f);
        Vector3 valorEsperado = new Vector3(0, 0, -10);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
}
