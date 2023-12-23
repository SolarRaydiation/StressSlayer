using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyingActivity : ActivityManager
{
    /**
     * Initialize method can be found ActivityManager abstract class
     */
    public override void StartActivity(int hoursSpentOnActivity)
    {
        Debug.Log("Performed studying activity!");
    }
}
