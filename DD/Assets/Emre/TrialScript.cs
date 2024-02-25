using UnityEngine;

public class TrialScript : MonoBehaviour
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
           
            Vector3 direction = player.position - transform.position;

           
            if (direction.x > 0)
            {
              
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
               
                transform.localScale = new Vector3(1, 1, 1); 
            }
        }
    }
}
