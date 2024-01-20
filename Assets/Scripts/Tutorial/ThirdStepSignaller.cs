using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdStepSignaller : TutorialSignaller
{
    protected override void InternalSignaller()
    {
        tm.finishedThirdStep = true;
        tm.SetupForFourthStep();
    }
}
