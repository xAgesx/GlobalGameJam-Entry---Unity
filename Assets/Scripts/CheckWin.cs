using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public GameObject winMessage;

    private void Start()
    {
        winMessage.SetActive(false);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("MainCamera"))
        {
            // we won!
            if (!winMessage.activeInHierarchy)
            {
                winMessage.SetActive(true);
            }
            
        }
    }
    */


    public void setWinMessage()
    {
        // we won!
        if (!winMessage.activeInHierarchy)
        {
            winMessage.SetActive(true);
        }
    }
}
