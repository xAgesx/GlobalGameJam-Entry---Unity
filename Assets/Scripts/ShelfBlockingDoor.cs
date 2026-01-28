using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfBlockingDoor : MonoBehaviour
{
    public Animator animator;

    public bool blockingDoor;
    private bool doorOpen;

    private void Start()
    {
        blockingDoor = false;
        doorOpen = false;
        Physics.IgnoreLayerCollision(7, 8, false);
    }

    private void Update()
    {       
        if (doorOpen)
        {
            IgnoreLaserLayer();
        }
        else
        {
            if (!blockingDoor)
            {
                CollideLaserLayer();
            }            
        }

        if (blockingDoor)
        {
            animator.SetBool("blocked", true);
        }
        else
        {
            animator.SetBool("blocked", false);
        }

        //Debug.Log("Blocking: " + blockingDoor);
        //Debug.Log("Ignoring Layer: " + Physics.GetIgnoreLayerCollision(7, 8));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shelf"))
        {
            blockingDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Shelf"))
        {
            blockingDoor = false;
        }
    }

    public void isDoorOpen(bool isOpen)
    {
        doorOpen = isOpen;
    }

    private void IgnoreLaserLayer()
    {
        Physics.IgnoreLayerCollision(7, 8, true);
    }

    private void CollideLaserLayer()
    {
        Physics.IgnoreLayerCollision(7, 8, false);              
    }
}
