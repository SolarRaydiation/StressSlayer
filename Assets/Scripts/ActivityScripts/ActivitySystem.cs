using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitySystem : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject playerControls;
    public GameObject dialogueScreen;
    public GameObject overworldScreen;
    public GameObject activityConfirmationScreen;

    public void Initialize()
    {
        DisableAllOtherScreens();
        activityConfirmationScreen.SetActive(true);
        ActivityConfirmationScreenManager acsm = activityConfirmationScreen.GetComponent<ActivityConfirmationScreenManager>();
        acsm.RefreshScreen();
    }

    private void DisableAllOtherScreens()
    {
        playerControls.SetActive(false);
        dialogueScreen.SetActive(false);
        overworldScreen.SetActive(false);
    }

    public void CancelInitialization()
    {
        dialogueScreen.SetActive(false);
        playerControls.SetActive(true);
        overworldScreen.SetActive(true);
    }
}
