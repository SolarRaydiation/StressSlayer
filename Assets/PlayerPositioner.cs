    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{
    public string[] lastSceneName;
    public Vector2[] newPlayerPosition;

    void Start()
    {
        Transform playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        string lastSceneTravelled = saveFile.lastSceneLocation;

        for (int i = 0; i < lastSceneName.Length; i++)
        {
            if (lastSceneName[i] == lastSceneTravelled)
            {
                Vector3 temp = new Vector3(newPlayerPosition[i].x, newPlayerPosition[i].y, 0f);
                playerPosition.position = temp;
                return;
            }
        }
        return;
    }
}
