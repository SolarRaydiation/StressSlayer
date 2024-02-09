using UnityEngine;
using UnityEngine.Events;

public class SpecialTeacherInteractableScript : MonoBehaviour
{
    public int time = 15;
    public UnityEvent isPastThreePM;
    public UnityEvent isBeforeThreePM;
    public void CheckInteractableResponse()
    {
        ClockManager cm = ClockManager.GetInstance();
        int currentTime = cm.currentHour;
        if(currentTime < time)
        {
            Debug.Log("isBeforeThreePM");
            isBeforeThreePM.Invoke();
        } else
        {
            Debug.Log("isPastThreePM");
            isPastThreePM.Invoke();
        }
    }
}
