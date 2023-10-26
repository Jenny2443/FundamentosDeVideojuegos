using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
    public IEnumerator MovePlayerTestHorizontalNegRango()
    {
        Vector3 valorEsperado = new Vector3(-6, 0, 0);
        player.transform.position = new Vector3(0, 0, 0);
        player.movementSpeed = 1f;
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(-6, 0);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError && 
                               Mathf.Abs(movement.z - valorEsperado.z) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en el eje z positivo hacia delante
    [UnityTest]
    public IEnumerator MovePlayerTestForwardRango()
    {
        Vector3 valorEsperado = new Vector3(0, 0, 21);
        player.transform.position = new Vector3(0, 0, 0);
        player.movementSpeed = 3f;
        
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(0, 7);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.z - valorEsperado.z) <= margenError && 
                               Mathf.Abs(movement.x - valorEsperado.x) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en el eje x (horizontal) positivo
    [UnityTest]
    public IEnumerator MovePlayerTestHorizontalPositiveRango()
    {
        //Vector3 vectorEsperadoMax = new Vector3(2.5f, 0, 0);
        //Vector3 vectorEsperadoMin = new Vector3(1.5f, 0, 0);

        Vector3 valorEsperado = new Vector3(6, 0, 0);
        player.transform.position = new Vector3(0, 0, 0);
        player.movementSpeed = 3f;
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(2, 0);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError && 
                               Mathf.Abs(movement.z - valorEsperado.z) <= margenError;

        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    
    //Test para comprobar que el jugador se mueve correctamente en el eje z negativo hacia atras
    [UnityTest]
    public IEnumerator MovePlayerTestBackWardsRango()
    {
        Vector3 valorEsperado = new Vector3(0, 0, -50);
        player.transform.position = new Vector3(0, 0, 0);
        player.movementSpeed = 5f;
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(0, -10);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError && 
                               Mathf.Abs(movement.z - valorEsperado.z) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal izquierda atras
    [UnityTest]
    public IEnumerator MovePlayerDiagonalLeftBackRango()
    {
        Vector3 vectorEsperado = new Vector3(-5, 0, -7);

        player.transform.position = new Vector3(0, 0, 0);
        player.movementSpeed = 1f;
        yield return new WaitForSeconds(1f);

        Vector3 movement = player.MovePlayer(-5, -7);

        yield return null;
        float margenError = 1f;

        bool dentroDelMargen = Mathf.Abs(movement.x - vectorEsperado.x) <= margenError
                               && Mathf.Abs(movement.z - vectorEsperado.z) <= margenError;

        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + vectorEsperado);

        // Comprueba si el movimiento está dentro del margen de error en cada componente x y z
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal izquierda delante
    [UnityTest]
    public IEnumerator MovePlayerDiagonalLeftRango()
    {
        Vector3 valorEsperado = new Vector3(-21, 0, 27);
        player.transform.position = new Vector3(0, 0, 0);
        player.movementSpeed = 3f;
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(-7, 9);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError
                               && Mathf.Abs(movement.z - valorEsperado.z) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal derecha alante
    [UnityTest]
    public IEnumerator MovePlayerDiagonalRightRango()
    {
        Vector3 valorEsperado = new Vector3(5, 0, 9);
        player.transform.position = new Vector3(0, 0, 0);
        player.movementSpeed = 1f;
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(5, 9);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError
                               && Mathf.Abs(movement.z - valorEsperado.z) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal derecha atras
    [UnityTest]
    public IEnumerator MovePlayerDiagonalRightBackRango()
    {
        Vector3 valorEsperado = new Vector3(12, 0, -16);
        player.transform.position = new Vector3(0, 0, 0);
        player.movementSpeed = 2f;
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(6, -8);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError
                               && Mathf.Abs(movement.z - valorEsperado.z) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
}
