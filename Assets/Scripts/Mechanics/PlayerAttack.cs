using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;

    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    void Start()
    {
        attackArea.SetActive(false);
    }

    void Update()
    {
        if (!attacking && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0f;
                attacking = false;
                attackArea.SetActive(false);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(true);
    }
}
