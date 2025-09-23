using UnityEngine;

public class Tooth : MonoBehaviour, IEnemy
{
    [Header("Settings")]
    [SerializeField] private float speed = 3.125f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private Transform wallCheck;

    private int direction;
    private float hitCooldown = 0.25f;
    private float hitTimer;

    private void Awake()
    {
        direction = Random.value > 0.5f ? 1 : -1;
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void Update()
    {

        transform.localScale = new Vector3(direction, 1, 1);

        if (ShouldReverse())
            Reverse();

        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (hitTimer > 0f)
            hitTimer -= Time.deltaTime;
    }

    private bool ShouldReverse()
    {

        bool noGroundAhead = !Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        bool hitWall = Physics2D.Raycast(wallCheck.position, Vector2.right * direction, 0.1f, groundLayer);

        return noGroundAhead || hitWall;
    }

    public void Reverse()
    {
        if (hitTimer <= 0f)
        {
            direction *= -1;
            hitTimer = hitCooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Tooth hit Player");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        if (wallCheck != null)
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * 0.1f * direction);
    }

    
}
