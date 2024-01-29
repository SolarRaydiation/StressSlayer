using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextScript : MonoBehaviour
{
    public string text;
    public float objectLifetime;
    public float floatSpeed;

    private TextMeshPro textMesh;
    
    public void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        textMesh.SetText(text);

        StartCoroutine(DestroyFloatingText());
    }

    public void Update()
    {
        transform.Translate(Vector2.up * floatSpeed * Time.deltaTime);
    }

    IEnumerator DestroyFloatingText()
    {
        yield return new WaitForSeconds(objectLifetime);
        Destroy(gameObject);
    }
}
