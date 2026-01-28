using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnlock : MonoBehaviour
{
    public GameObject laserDoor = null;
    public Animator animator;
    public Light keyLight1;
    public Light keyLight2;
    public Color unlockedLightColor;

    public ShelfBlockingDoor sbd;
    private bool doorBlocked;

    private Color lockedLightColor1;
    private Color lockedLightColor2;

    private bool doorIsOpen;

    void Start()
    {
        laserDoor.SetActive(true);
        doorIsOpen = false;
        lockedLightColor1 = keyLight1.color;
        lockedLightColor2 = keyLight2.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.name == "Broken Door 2")
        {
            doorBlocked = sbd.blockingDoor; //door2.GetComponent<ShelfBlockingDoor>().blockingDoor;
        }
        else
        {
            doorBlocked = false;
        }
        
        if (other.gameObject.CompareTag("Key") && (doorIsOpen || doorBlocked))
        {
            StopCoroutine(DoorCloseDelay());
            laserDoor.SetActive(false);
            keyLight1.color = unlockedLightColor;
            keyLight2.color = unlockedLightColor;
        }        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            StartCoroutine(DoorCloseDelay());
            keyLight1.color = lockedLightColor1;
            keyLight2.color = lockedLightColor2;
        }
    }

    IEnumerator DoorCloseDelay()
    {
        yield return new WaitForSeconds(1);
        laserDoor.SetActive(true);
    }

    public void OpenDoor()
    {
        doorIsOpen = true;
        animator.SetBool("key", true);        
    }


    public void CloseDoor()
    {
        doorIsOpen = false;
        animator.SetBool("key", false);        
    }
}
