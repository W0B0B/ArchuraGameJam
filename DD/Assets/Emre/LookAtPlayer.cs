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
            // Player'ýn konumunu alarak Enemy'nin yönünü belirle
            Vector3 direction = player.position - transform.position;

            // Yönü belirle
            if (direction.x > 0)
            {
                // Player saðda ise saða doðru bak
                transform.localScale = new Vector3(-1, 1, 1); // Yatay ölçeklemeyi -1 yaparak yönü deðiþtir
            }
            else
            {
                // Player solda ise sola doðru bak
                transform.localScale = new Vector3(1, 1, 1); // Yatay ölçeklemeyi 1 yaparak yönü deðiþtir
            }
        }
    }
}
