using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathRoomManager : RoomManager
{
    protected override void ExecuteRoomEndFunctions()
    {
        animator.SetTrigger("MathToDefault");
    }

    protected override void ExecuteRoomStartFunctions()
    {
        animator.SetTrigger("FadeToMath");
    }
    
    protected override void TriggerEndingAnimation()
    {
        // intentionally left blank
    }

    protected override void TriggerStartingAnimation()
    {
        // intentionally left blank
    }
}
