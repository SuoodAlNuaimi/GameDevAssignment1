using UnityEngine;
using Platformer.Mechanics;


public class EmemyProjectile : MonoBehaviour, IDeflectable
{

    private Rigidbody2D rb;
    private bool hasHit = false;


    public void deflect(Vector2 direction)
    {
        IgnoreCollisionWithEmemy();

        rb.linearVelocity = direction * ReturnSpeed;
    }

    public float ReturnSpeed {get; set;} =10f;


    [SerializeField] private float damageAmount = 1f;
    private IDamgeable iDamageable;

    private Collider2D coll;
    public Collider2D EmemyColl {get; set;}

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        IgnoreCollisionWithEmemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (hasHit) return; // 🔒 prevent multiple hits
            hasHit = true;

        PlayerController player = collision.GetComponent<PlayerController>();


        if (player != null)
            {
                // PLAYER HIT
                if (player.IsBlocking)
                {
                    // BLOCKED → DESTROY ARROW, NO DAMAGE
                    Destroy(gameObject);
                    return;
                }

                // NOT BLOCKING → DAMAGE PLAYER
                IDamgeable dmg = collision.GetComponent<IDamgeable>();
                if (dmg != null)
                {
                    dmg.Damage(damageAmount);
                }

                Destroy(gameObject);
                return;
            }


        iDamageable = collision.gameObject.GetComponent<IDamgeable>();
        if (iDamageable != null)
        {
            //damage
            iDamageable.Damage(damageAmount);
        }
    }

    private void IgnoreCollisionWithEmemy()
    {
        if (!Physics2D.GetIgnoreCollision(coll, EmemyColl))
        {
            Physics2D.IgnoreCollision(coll, EmemyColl, true);
        }
        else
        {
            Physics2D.IgnoreCollision(coll, EmemyColl, false);
        }
    }

}
