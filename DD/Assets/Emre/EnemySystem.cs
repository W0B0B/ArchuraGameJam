using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float jumpForce = 10f; 
    public float followDistance = 5f; 
    public float destroyDelay = 3f;
    private Rigidbody2D rb;
    private Transform player;
    private bool isFollowingPlayer = false;
    private bool isGrounded = false;
    private bool hasTouchedPlayer = false; 
    public LayerMask groundLayer; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
       if (player!=null)
       {
        if (!isFollowingPlayer && Vector2.Distance(transform.position, player.position) < followDistance)
        {
            isFollowingPlayer = true;
        }

        
        if (isFollowingPlayer)
        {
            Vector2 targetPosition = new Vector2(player.position.x, player.position.y);

           
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

           
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

            if (!hasTouchedPlayer)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer > followDistance)
                {
                    hasTouchedPlayer = true;
                    Invoke("DestroyEnemy", destroyDelay); 
                }
            }
        }
       }
        
    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasTouchedPlayer = true; 
           collision.gameObject.GetComponent<Health>().TakeDamage(1);
           
        }
    }
}
