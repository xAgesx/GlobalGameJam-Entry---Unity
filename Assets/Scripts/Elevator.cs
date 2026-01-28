using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform upPos;
    public Transform downPos;
    public Transform xrOrigin;

    public GameObject wallTop, wallBottom;

    public float lerpDuration;
    public bool moving;

    private void OnEnable()
    {
        TriggerElevatorUp.OnElevatorGoUp += GoUp;
    }

    private void OnDisable()
    {
        TriggerElevatorUp.OnElevatorGoUp -= GoUp;
    }



    private void Start()
    {
        moving = false;
    }

    private void Update()
    {
        if (moving)
        {
            wallTop.SetActive(true);
            wallBottom.SetActive(true);
        }
        else
        {
            wallTop.SetActive(false);
            wallBottom.SetActive(false);
        }
    }

    IEnumerator DoElevator(Vector3 endingPosition)
    {
        
        Vector3 startingPosition = transform.position;
        Vector3 currentPosition = startingPosition;

        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            currentPosition.y = Mathf.Lerp(startingPosition.y, endingPosition.y, timeElapsed / lerpDuration);

            transform.position = currentPosition;
            xrOrigin.position = new Vector3(xrOrigin.position.x, currentPosition.y, xrOrigin.position.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = endingPosition;
        moving = false;
    }


    public void GoUp()
    {
        moving = true;
        StartCoroutine(DoElevator(upPos.position));
    }

    public void GoDown()
    {
        moving = true;
        StartCoroutine(DoElevator(downPos.position));
    }
}
