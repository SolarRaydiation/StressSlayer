using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviSciRoomManager : RoomManager
{
    protected override void TriggerStartingAnimation()
    {
        animator.SetTrigger("FadeToEnviSci");
    }
    protected override void TriggerEndingAnimation()
    {
        animator.SetTrigger("EnviSciToDefault");
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
