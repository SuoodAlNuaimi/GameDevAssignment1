using UnityEngine;  
using System.Collections;


public class EmemyShoot : MonoBehaviour 
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float timeBtwAttacks = 2f;

    private float shootTimer;

    private Rigidbody2D bulletRB;

    private EmemyProjectile ememyProjectile;

    private Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //update timer
        shootTimer += Time.deltaTime;

        if (shootTimer >= timeBtwAttacks)
        {
            //reset timer
            shootTimer = 0;

            //shoot projectile
            Shoot();
        }
    }

    private void Shoot()
    {
        //spawn a bullet
        bulletRB = Instantiate(bulletPrefab, transform.position, transform.rotation);

        bulletRB.transform.right = GetShootDir();

        bulletRB.linearVelocity = bulletRB.transform.right * bulletSpeed;

        ememyProjectile = bulletRB.gameObject.GetComponent<EmemyProjectile>();

        ememyProjectile.EmemyColl = coll;
    }

    public Vector2 GetShootDir()
    {
        Transform playerTrans = GameObject.FindGameObjectWithTag("KnightPlayer").transform;

        return (playerTrans.position - transform.position).normalized;
    }

}
