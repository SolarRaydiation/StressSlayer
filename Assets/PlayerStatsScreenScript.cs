using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsScreenScript : MonoBehaviour
{
    #region Variables
    public TextMeshProUGUI movementStat;
    public TextMeshProUGUI attackStat;
    public TextMeshProUGUI nextLevelDifficulty;
    public TextMeshProUGUI cashStat;
    public TextMeshProUGUI FruitsAndVegetables;
    public GameObject playerStatsScreen;
    #endregion

    public void UpdateAndOpenScreen()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        sfm.ReloadPlayerData();
        PlayerData pd = sfm.saveFile;

        movementStat.SetText("Movement Speed: " + pd.baseMovementSpeed.ToString());
        attackStat.SetText("Attack Damage: " + pd.baseAttackDamage.ToString());
        nextLevelDifficulty.SetText("Next Level Difficulty: " + pd.nextLevelDifficulty.ToString());
        cashStat.SetText("Cash Remaining: " + pd.cashRemaining.ToString());
        FruitsAndVegetables.SetText("Boosts Remaining: " + pd.fruitsAndVegetablesOwned.ToString());

        playerStatsScreen.SetActive(true);
    }
}
