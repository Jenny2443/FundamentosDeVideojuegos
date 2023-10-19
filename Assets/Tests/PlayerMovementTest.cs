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
        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        GameObject playerObject = MonoBehaviour.Instantiate(playerPrefab);
        player = playerObject.GetComponent<PlayerControler>();
        player.GetComponent<Rigidbody>().useGravity = false;
        player.movementSpeed = 1;
    }
    
    [TearDown]
    public void Teardown()
    {
        if (player != null)
        {
            GameObject.Destroy(player.gameObject);   
        }
    }

    [UnityTest]
    public IEnumerator PlayerIsCreatedProperly()
    {
        yield return new WaitForSeconds(0.1f);  
        Assert.AreEqual(1, player.count);
    }

    [UnityTest]
    public IEnumerator MovePlayerTest()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 movement = player.MovePlayer(1, 0);
        yield return new WaitForSeconds(1f);
        Vector3 valorEsperado = new Vector3(1, 0, 0);
        Debug.Log("Movement: " + movement);
        Debug.Log("Valor esperado: " + valorEsperado);
        Assert.AreEqual(valorEsperado, movement);
    }
}
