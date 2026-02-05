using UnityEngine;

public class EmemyProjectile : MonoBehaviour, IDeflectable
{

    private Rigidbody2D rb;

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
