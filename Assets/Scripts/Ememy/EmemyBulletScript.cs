using UnityEngine;
using Platformer.Mechanics;


public class EmemyBulletScript : MonoBehaviour
{

    private Collider2D shooterCollider;
    public GameObject owner;

    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("KnightPlayer");

        shooterCollider = GetComponentInParent<Collider2D>();

        if (shooterCollider != null)
        {
            Physics2D.IgnoreCollision(
                GetComponent<Collider2D>(),
                shooterCollider
            );
        }

        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = direction.normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer>10)
        {
            Destroy(gameObject); 
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        // Hit player
        if (other.CompareTag("KnightPlayer"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            Health health = other.GetComponent<Health>();

            // BLOCK CHECK
            if (player != null && player.IsBlocking && player.IsFacing(transform.position))
            {
                // Block successful
                Destroy(gameObject);
                return;
            }

            // Not blocking → take damage
            if (health != null)
            {
                health.Damage(1);
            }

            Destroy(gameObject);
            return;
        }

        // Prevent enemy damaging itself
        if (other.CompareTag("Enemy"))
            return;
    }



}
