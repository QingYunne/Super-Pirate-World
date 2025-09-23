using UnityEngine;

public class Pearl : MonoBehaviour, IEnemy
{
    [Header("Settings")]
    [SerializeField] private GameObject particleEffectPrefab;
    [SerializeField] private float speed = 2.34f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float hitCooldown = 0.25f;

    private int direction;
    private Rigidbody2D rb;
    private float hitTimer; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Setup(int dir)
    {
        direction = dir;
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (hitTimer > 0f)
            hitTimer -= Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            hitTarget();
            return;
        }
    }

    public void hitTarget()
    {
        if (particleEffectPrefab != null)
        {
            EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_Pearl, 3f));
            GameObject effect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.21f);
        }

        Destroy(gameObject);
    }

    public void Reverse()
    {
        if (hitTimer <= 0f)
        {
            direction *= -1;
            hitTimer = hitCooldown;
        }
        Debug.Log("Pearl reversed");
    }
}
