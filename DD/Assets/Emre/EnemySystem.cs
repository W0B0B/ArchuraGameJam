using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f; // D��man�n hareket h�z�
    public float jumpForce = 10f; // D��man�n z�plama kuvveti
    public float followDistance = 5f; // D��man�n oyuncuyu takip edece�i mesafe
    public float destroyDelay = 3f; // D��man�n yok olma gecikmesi s�resi

    private Rigidbody2D rb;
    private Transform player;
    private bool isFollowingPlayer = false;
    private bool isGrounded = false;
    private bool hasTouchedPlayer = false; // Player'a dokunup dokunmad���n� kontrol et
    public LayerMask groundLayer; // Zemin katman�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Oyuncuya takip etme mesafesine gelindi�inde takibi ba�lat
        if (!isFollowingPlayer && Vector2.Distance(transform.position, player.position) < followDistance)
        {
            isFollowingPlayer = true;
        }

        // Oyuncuyu takip etme
        if (isFollowingPlayer)
        {
            // Player'�n yatay konumunu al
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);

            // Hedef pozisyona do�ru hareket et
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // D��man�n zeminde oldu�unu kontrol et
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

            // D��man�n zeminde oldu�u durumda ve oyuncuya yeterince yakla�t���nda z�plamas�n� sa�la
            if (isGrounded && Mathf.Abs(transform.position.x - player.position.x) < 1f)
            {
                Jump();
            }

            // Player'a dokunup dokunmad���n� kontrol et ve sayac� ba�lat
            if (!hasTouchedPlayer)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer > followDistance)
                {
                    hasTouchedPlayer = true;
                    Invoke("DestroyEnemy", destroyDelay); // Belirli bir s�re sonra d��man� yok et
                }
            }
        }
    }

    // D��man�n z�plamas�n� sa�layan metod
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // D��man� yok eden metod
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // D��man�n Player'a �arpt���n� kontrol et
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasTouchedPlayer = true; // Player'a dokunuldu�unda i�aretle
            CancelInvoke("DestroyEnemy"); // Sayac� durdur
            Destroy(gameObject);
        }
    }
}
