using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    

    private void Update()
    {
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Health.Instance.CurrentHealt)
            {
                hearts[i].sprite = FullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }
            if (i < Health.Instance.MaxHealt)
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
