using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DialogueTest
{
    private PlayerControler player;
    private Dialogue dialogue;

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
        
        Debug.Log("Player creado: " + player.transform.position);

        GameObject sifoPrefab = Resources.Load<GameObject>("Prefabs/Sifo");
        GameObject sifoObject = MonoBehaviour.Instantiate(sifoPrefab);
        // Obtener el componente Dialogue de Sifo
        dialogue = sifoObject.GetComponent<Dialogue>();
        
        Debug.Log("Sifo creado: " + dialogue.transform.position);
    }

    [UnityTest]
    public IEnumerator PlayerEstaEnRango()
    {
        Debug.Log("Esta en rango 0: " + dialogue.estaEnRango);
        dialogue.transform.position = new Vector3(5, 0, 0);
        Debug.Log("Pos dialogo: " + dialogue.transform.position);
        player.transform.position = new Vector3(5, 0, 0);
        Debug.Log("Pos player: " + player.transform.position);
        
        dialogue.OnTriggerEnter(player.gameObject.GetComponent<Collider>());
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Esta en rango 1: " + dialogue.estaEnRango);
        Assert.IsTrue(dialogue.estaEnRango);
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void DialogueTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DialogueTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
