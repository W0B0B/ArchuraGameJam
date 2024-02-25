using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public Health health;
    

    private void Update()
    {
        
        for (int i = 0; i <hearts.Length; i++)
        {
            if (i < health.CurrentHealt)
            {
                hearts[i].sprite = FullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }
            if (i < health.MaxHealt)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
