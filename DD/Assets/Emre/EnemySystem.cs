using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f; // Düþmanýn hareket hýzý
    public float jumpForce = 10f; // Düþmanýn zýplama kuvveti
    public float followDistance = 5f; // Düþmanýn oyuncuyu takip edeceði mesafe
    public float destroyDelay = 3f; // Düþmanýn yok olma gecikmesi süresi

    private Rigidbody2D rb;
    private Transform player;
    private bool isFollowingPlayer = false;
    private bool isGrounded = false;
    private bool hasTouchedPlayer = false; // Player'a dokunup dokunmadýðýný kontrol et
    public LayerMask groundLayer; // Zemin katmaný

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Oyuncuya takip etme mesafesine gelindiðinde takibi baþlat
        if (!isFollowingPlayer && Vector2.Distance(transform.position, player.position) < followDistance)
        {
            isFollowingPlayer = true;
        }

        // Oyuncuyu takip etme
        if (isFollowingPlayer)
        {
            // Player'ýn yatay konumunu al
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);

            // Hedef pozisyona doðru hareket et
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Düþmanýn zeminde olduðunu kontrol et
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

            // Düþmanýn zeminde olduðu durumda ve oyuncuya yeterince yaklaþtýðýnda zýplamasýný saðla
            if (isGrounded && Mathf.Abs(transform.position.x - player.position.x) < 1f)
            {
                Jump();
            }

            // Player'a dokunup dokunmadýðýný kontrol et ve sayacý baþlat
            if (!hasTouchedPlayer)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer > followDistance)
                {
                    hasTouchedPlayer = true;
                    Invoke("DestroyEnemy", destroyDelay); // Belirli bir süre sonra düþmaný yok et
                }
            }
        }
    }

    // Düþmanýn zýplamasýný saðlayan metod
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Düþmaný yok eden metod
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // Düþmanýn Player'a çarptýðýný kontrol et
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasTouchedPlayer = true; // Player'a dokunulduðunda iþaretle
            CancelInvoke("DestroyEnemy"); // Sayacý durdur
            Destroy(gameObject);
        }
    }
}
