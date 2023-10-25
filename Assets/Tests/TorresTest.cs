using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TorresTest
{
    private Torres_hanoi torres;
    private GameObject discoGrandePrefab;
    private GameObject discoMedianoPrefab;
    private GameObject discoPequenoPrefab;
    private GameObject inventoryPrefab;
    private Inventory inventory;

    private GameObject discoGrande;
    private GameObject discoMediano;
    private GameObject discoPequeno;

    [SetUp]
    public void SetUp()
    {
        // Cargar los prefabs desde la carpeta Resources
        discoGrandePrefab = Resources.Load<GameObject>("Prefabs/Disco_grande");
        discoMedianoPrefab = Resources.Load<GameObject>("Prefabs/Disco_mediano");
        discoPequenoPrefab = Resources.Load<GameObject>("Prefabs/Disco_pequeno");
        inventoryPrefab = Resources.Load<GameObject>("Prefabs/Inventario");
        
        Debug.Log("Prefabs creados");
        
        // Crear una instancia de Inventory
        inventory = Object.Instantiate(inventoryPrefab).GetComponent<Inventory>();
        Debug.Log("Inventory creado: " + inventory);

        // Configurar el inventario con discos
         discoGrande = Object.Instantiate(discoGrandePrefab);
         discoMediano = Object.Instantiate(discoMedianoPrefab);
         discoPequeno = Object.Instantiate(discoPequenoPrefab);
        
        // Crear una instancia de Torres_hanoi
        torres = new GameObject().AddComponent<Torres_hanoi>();
        torres.inventory = inventory;
        Debug.Log("Torres creadas: " + torres);
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void TorresTestSimplePasses()
    {
        
        // Use the Assert class to test conditions
    }
    
    [Test]
    public void TestGetColumnForDiscoPequeno()
    {
        // Agregar discos al inventario
        inventory.AddItem(discoGrande.GetComponent<Item>());
        inventory.AddItem(discoMediano.GetComponent<Item>());
        inventory.AddItem(discoPequeno.GetComponent<Item>());
        Debug.Log("En test column");
        // Llama a la función getColumn() y verifica que devuelva 2, ya que es un disco pequeño.
        inventory.getInventoryItem(inventory.getNowActive()).gameObject.SetActive(true);
        Debug.Log("Inventory: " + inventory.getInventoryItem(inventory.getNowActive()));
        int result = torres.getColumn();
        Debug.Log("Result desps: " + result);
        Assert.AreEqual(2, result);
    }

    [Test]
    public void TestGetColumnForDiscoMediano()
    {
        // Agregar discos al inventario
        inventory.AddItem(discoPequeno.GetComponent<Item>());
        inventory.AddItem(discoGrande.GetComponent<Item>());
        inventory.AddItem(discoMediano.GetComponent<Item>());
        Debug.Log("En test column");
        // Llama a la función getColumn() y verifica que devuelva 1, ya que es un disco mediano.
        inventory.getInventoryItem(inventory.getNowActive()).gameObject.SetActive(true);
        Debug.Log("Inventory: " + inventory.getInventoryItem(inventory.getNowActive()));
        int result = torres.getColumn();
        Debug.Log("Result desps: " + result);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestGetColumnForDiscoGrande()
    {
        // Agregar disco grande al inventario
        inventory.AddItem(discoGrande.GetComponent<Item>());
        Debug.Log("En test column");
        // Llama a la función getColumn() y verifica que devuelva 0, ya que es un disco grande.
        inventory.getInventoryItem(inventory.getNowActive()).gameObject.SetActive(true);
        Debug.Log("Inventory: " + inventory.getInventoryItem(inventory.getNowActive()));
        int result = torres.getColumn();
        Debug.Log("Result desps: " + result);
        Assert.AreEqual(0, result);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TorresTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    
    //Eliminar el objeto al terminar los tests
    [TearDown]
    public void Teardown()
    {
        if (torres != null)
        {
            GameObject.Destroy(torres.gameObject);   
        }
        
        if (inventory != null)
        {
            GameObject.Destroy(inventory.gameObject);   
        }
        
        if (discoGrande != null)
        {
            GameObject.Destroy(discoGrande.gameObject);   
        }
        
        if (discoMediano != null)
        {
            GameObject.Destroy(discoMediano.gameObject);   
        }
        
        if (discoPequeno != null)
        {
            GameObject.Destroy(discoPequeno.gameObject);   
        }
        
    }
}