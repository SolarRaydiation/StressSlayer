using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterNameGetter : MonoBehaviour
{

    private TextMeshProUGUI playerNameText;
    void Start()
    {
        playerNameText = GetComponent<TextMeshProUGUI>();   
        PlayerStatsController ps = PlayerStatsController.GetInstance();
        Debug.Log(ps.playerName);
        playerNameText.SetText(ps.playerName);
    }
}
