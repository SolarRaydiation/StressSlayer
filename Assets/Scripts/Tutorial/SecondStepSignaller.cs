using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStepSignaller : TutorialSignaller
{
    protected override void InternalSignaller()
    {
        tm.finishedSecondStep = true;
    }
}
