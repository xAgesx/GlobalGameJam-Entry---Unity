using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerElevatorUp : MonoBehaviour
{
    private bool goUp;

    public delegate void ElevatorGoUp();
    public static event ElevatorGoUp OnElevatorGoUp;

    private void Start()
    {
        goUp = true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goUp = !goUp;
            //Debug.Log("trigger zone, goUp: " + goUp);
            if (goUp)
            {
                OnElevatorGoUp();
            }
        }        
    }

}
