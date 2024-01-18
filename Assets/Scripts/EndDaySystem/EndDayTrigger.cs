using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDayTrigger : MonoBehaviour
{
    public EndDaySystem eds;
    public void TriggerEndDay()
    {
       eds.InitializeEndDayMode();
    }
}
