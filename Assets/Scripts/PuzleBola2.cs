using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine.SceneManagement;

public class PuzleBola2 : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Bola2.0");
    }
}