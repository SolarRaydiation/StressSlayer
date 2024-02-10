using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClockManager;

public class DayBasedDialogueSupplier : MonoBehaviour
{
    public TextAsset mondayTextJSON;
    public TextAsset tuesdayTextJSON;
    public TextAsset wendesdayTextJSON;
    public TextAsset thursdayTextJSON;
    public TextAsset fridayTextJSON;

    void Start()
    {
        ClockManager cm = ClockManager.GetInstance();
        DialogueTrigger dt = gameObject.GetComponent<DialogueTrigger>();

        switch (cm.currentDay)
        {
            case Day.Monday:
                dt.inkJSON = mondayTextJSON;
                break;
            case Day.Tuesday:
                dt.inkJSON = tuesdayTextJSON;
                break;
            case Day.Wednesday:
                dt.inkJSON = wendesdayTextJSON; 
                break;
            case Day.Thursday:
                dt.inkJSON= thursdayTextJSON;
                break;
            case Day.Friday:
                dt.inkJSON = fridayTextJSON;
                break;
            default:
                Debug.LogError($"Recieved unknown Day type {cm.currentDay} while supplying dialogue!");
                dt.inkJSON = null;
                break;
        }
    }
}
