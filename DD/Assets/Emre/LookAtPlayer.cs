using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Player'�n konumunu alarak Enemy'nin y�n�n� belirle
            Vector3 direction = player.position - transform.position;

            // Y�n� belirle
            if (direction.x > 0)
            {
                // Player sa�da ise sa�a do�ru bak
                transform.localScale = new Vector3(-1, 1, 1); // Yatay �l�eklemeyi -1 yaparak y�n� de�i�tir
            }
            else
            {
                // Player solda ise sola do�ru bak
                transform.localScale = new Vector3(1, 1, 1); // Yatay �l�eklemeyi 1 yaparak y�n� de�i�tir
            }
        }
    }
}
