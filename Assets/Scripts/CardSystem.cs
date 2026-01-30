using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardSystem : MonoBehaviour
{
    public List<GameObject> cards; 
    private int currentIndex = 0;

    void Start()
    {
        
        ShowCard(0);
    }

    public void NextCard()
    {
        currentIndex++;

        // If we reach the end, loop back to the start (or stay at the last card)
        if (currentIndex >= cards.Count)
        {
            currentIndex = 0; 
        }

        ShowCard(currentIndex);
    }

    void ShowCard(int index)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetActive(i == index);
        }
    }
}