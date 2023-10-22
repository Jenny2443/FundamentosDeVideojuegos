using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InteraccionSifoTest
{
    
    private PlayerControler player;
    private GameObject playerObject;
    
    private Dialogue sifo;
    private GameObject sifoObject;

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
        
        //Creamos el objeto de sifo
        GameObject sifoPrefab = Resources.Load<GameObject>("Prefabs/Sifo");
        GameObject sifoObject = MonoBehaviour.Instantiate(sifoPrefab);
        
        //Obtenemos el componente Dialogue de sifo
        sifo = sifoObject.GetComponent<Dialogue>();
    }
    
    //Borrar objetos despues de los tests
    [TearDown]
    public void Teardown()
    {
        if (player != null)
        {
            GameObject.Destroy(player.gameObject);   
        }
        if (sifo != null)
        {
            GameObject.Destroy(sifo.gameObject);   
        }
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void InteraccionSifoTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator InteraccionSifoTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
