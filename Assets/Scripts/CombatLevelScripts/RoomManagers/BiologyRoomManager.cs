using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BiologyRoomManager : RoomManager
{
    protected override void TriggerStartingAnimation()
    {
        animator.SetTrigger("FadeToBiology");
    }

    protected override void TriggerEndingAnimation()
    {
        animator.SetTrigger("BiologyToDefault");
    }

    protected override void ExecuteRoomStartFunctions()
    {
        // intentionally left blank
    }

    protected override void ExecuteRoomEndFunctions()
    {
        // intentionally left blank
    }
}
