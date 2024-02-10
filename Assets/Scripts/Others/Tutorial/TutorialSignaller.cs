using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialSignaller : MonoBehaviour
{
    protected TutorialManager tm;

    protected abstract void InternalSignaller();

    public void Start()
    {
        tm = TutorialManager.GetInstance();
    }

    public void SignalTutorial()
    {
        InternalSignaller();
    }
}
