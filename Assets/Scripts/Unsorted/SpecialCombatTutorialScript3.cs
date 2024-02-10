using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialCombatTutorialScript3 : MonoBehaviour
{

    public GameObject waypoint;
    public GameObject barrier;
    public void ShowWaypoint()
    {
        waypoint.SetActive(true);
        barrier.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
