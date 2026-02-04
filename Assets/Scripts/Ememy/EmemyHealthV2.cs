using UnityEngine;

public class EmemyHealthV2 : MonoBehaviour, IDamgeable
{

    public void Damage(float damageAmount, Vector2 attackDirection)
    {

        HasTakenDamage = true;

        currentHealth -= damageAmount;

        SpawnDamageParticles(attackDirection); 

        if (currentHealth <= 0)
        {
            Die();
        }

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private ParticleSystem damageParticles;

    private float currentHealth;

    private ParticleSystem damageParticleInstance;

    private EmemyHealthBar healthBar;

    public bool HasTakenDamage { get; set;}

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar = GetComponentInChildren<EmemyHealthBar>();
    } 

    private void SpawnDamageParticles(Vector2 attackDirection)
    {
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);

        damageParticleInstance = Instantiate(damageParticles, transform.position, spawnRotation);
    }
}
