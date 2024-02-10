using System;
using UnityEngine;

public class DeleteCanvas : MonoBehaviour
{
    public void DestroyObject()
    {
        GameObject.Destroy(gameObject);
        try
        {
            AudioManager.instance.PlaySFX("TapSFX");
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not play SFX from DeletCanvas!" + e.ToString());
        }
    }

    public void DeactivateObject()
    {
        gameObject.SetActive(false);
        try
        {
            AudioManager.instance.PlaySFX("TapSFX");
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not play SFX from DeletCanvas!" + e.ToString());
        }
    }
}
