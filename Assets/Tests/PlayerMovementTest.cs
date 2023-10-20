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
        //Vector3 vectorEsperadoMax = new Vector3(2.5f, 0, 0);
        //Vector3 vectorEsperadoMin = new Vector3(1.5f, 0, 0);
        
        Debug.Log("Pos inicial: " + player.transform.position);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos tras 0: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(2, 0);
        yield return new WaitForSeconds(1f);
        Vector3 valorEsperado = new Vector3(2, 0, 0);
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado max: " + 2.5f);
        Debug.Log("Valor esperado min: " + 1.5f);
        
        //Assert.GreaterOrEqual(2.5f, movement.x);
        //Assert.LessOrEqual(1.5f, movement.x);

        
        Assert.AreEqual(valorEsperado, movement);
        
        //Assert.GreaterOrEqual(vectorEsperadoMin.x, movement.x);
        //Assert.LessOrEqual(vectorEsperadoMax.x, movement.x);
    }

    //Test para comprobar que el jugador se mueve correctamente en el eje x (horizontal) positivo
    [UnityTest]
    public IEnumerator MovePlayerTestHorizontalNeg()
    {
        Debug.Log("Pos inicial: " + player.transform.position);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos tras 0: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(-6, 0);
        yield return new WaitForSeconds(1f);
        Vector3 valorEsperado = new Vector3(-6, 0, 0);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en el eje z positivo hacia delante
    [UnityTest]
    public IEnumerator MovePlayerTestForward()
    {
        Debug.Log("Pos inicial: " + player.transform.position);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos tras 0: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(0, 7);
        yield return new WaitForSeconds(1f);
        Vector3 valorEsperado = new Vector3(0, 0, 7);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en el eje z negativo hacia atras
    [UnityTest]
    public IEnumerator MovePlayerTestBackWards()
    {
        Debug.Log("Pos inicial: " + player.transform.position);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos tras 0: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(0, -10);
        yield return new WaitForSeconds(1f);
        Vector3 valorEsperado = new Vector3(0, 0, -10);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal derecha
    [UnityTest]
    public IEnumerator MovePlayerDiagonalRightBack()
    {
        Debug.Log("Pos inicial: " + player.transform.position);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos tras 0: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(6, -8);
        yield return new WaitForSeconds(1f);
        Vector3 valorEsperado = new Vector3(6, 0, -8);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal derecha atras
    [UnityTest]
    public IEnumerator MovePlayerDiagonalRight()
    {
        //Debug.Log("Pos inicial: " + player.transform.position);
        //player.transform.position = new Vector3(0, 0, 0);
        //Debug.Log("Pos tras 0: " + player.transform.position);
        yield return new WaitForSeconds(0.1f);
        Vector3 movement = player.MovePlayer(5, 5);
        yield return new WaitForSeconds(0.1f);
        Vector3 valorEsperado = new Vector3(5, 0, 5);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal izquierda
    [UnityTest]
    public IEnumerator MovePlayerDiagonalLeft()
    {
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(-7, 7);
        yield return new WaitForSeconds(1f);
        Vector3 valorEsperado = new Vector3(-7, 0, 7);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
  
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal izquierda atras
    [UnityTest]
    public IEnumerator MovePlayerDiagonalLeftBack()
    {
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(-5, -10);
        yield return new WaitForSeconds(1f);
        Vector3 valorEsperado = new Vector3(-5, 0, -10);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
    
    [UnityTest]
    public IEnumerator MovePlayerDiagonalLeftBackRango()
    {
        Vector3 vectorEsperado = new Vector3(-5, 0, -10);

        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);

        Vector3 movement = player.MovePlayer(-5, -10);

        float margenError = 1f;

        bool dentroDelMargen = Mathf.Abs(movement.x - vectorEsperado.x) <= margenError
                               && Mathf.Abs(movement.z - vectorEsperado.z) <= margenError;

        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + vectorEsperado);

        // Comprueba si el movimiento está dentro del margen de error en cada componente x y z
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal izquierda
    [UnityTest]
    public IEnumerator MovePlayerDiagonalLeftRango()
    {
        Vector3 valorEsperado = new Vector3(-7, 0, 7);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(-7, 7);
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
    public IEnumerator MovePlayerDiagonalRightRango()
    {
        Vector3 valorEsperado = new Vector3(5, 0, 5);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(5, 5);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError
                               && Mathf.Abs(movement.z - valorEsperado.z) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en diagonal derecha
    [UnityTest]
    public IEnumerator MovePlayerDiagonalRightBackRango()
    {
        Vector3 valorEsperado = new Vector3(6, 0, -8);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
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

    //Test para comprobar que el jugador se mueve correctamente en el eje z negativo hacia atras
    [UnityTest]
    public IEnumerator MovePlayerTestBackWardsRango()
    {
        Vector3 valorEsperado = new Vector3(0, 0, -10);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(0, -10);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en el eje x (horizontal) positivo
    [UnityTest]
    public IEnumerator MovePlayerTestHorizontalNegRango()
    {
        Vector3 valorEsperado = new Vector3(-6, 0, 0);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(-6, 0);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError;
        
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
    
    //Test para comprobar que el jugador se mueve correctamente en el eje z positivo hacia delante
    [UnityTest]
    public IEnumerator MovePlayerTestForwardRango()
    {
        Vector3 valorEsperado = new Vector3(0, 0, 7);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(0, 7);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.z - valorEsperado.z) <= margenError;
        
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

        Vector3 valorEsperado = new Vector3(2, 0, 0);
        player.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Pos inicial: " + player.transform.position);
        yield return new WaitForSeconds(1f);
        Vector3 movement = player.MovePlayer(2, 0);
        yield return new WaitForSeconds(1f);
        float margenError = 1f;
        bool dentroDelMargen = Mathf.Abs(movement.x - valorEsperado.x) <= margenError;

        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.IsTrue(dentroDelMargen);
    }
}
