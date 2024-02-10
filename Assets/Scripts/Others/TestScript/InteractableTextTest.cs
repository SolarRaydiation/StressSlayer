using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableTextTest : MonoBehaviour
{
    public TextMeshPro textMesh;
    public string text;

    public void Start()
    {
        textMesh.text = text;
    }
}
