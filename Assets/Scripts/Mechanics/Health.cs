using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represents the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        public int maxHP = 1;

        [Header("UI Animation")]
        public float animationSpeed = 8f;

        public bool IsAlive => currentHP > 0;

        int currentHP;
        float animatedHP;

        /// <summary>
        /// Value between 0 and 1 for UI health bars
        /// </summary>
        public float HealthPercent => animatedHP / maxHP;

        void Awake()
        {
            currentHP = maxHP;
            animatedHP = maxHP;
        }

        void Update()
        {
            // Smooth animation toward real HP
            animatedHP = Mathf.Lerp(animatedHP, currentHP, Time.deltaTime * animationSpeed);
        }

        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);

            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        public void Die()
        {
            while (currentHP > 0)
                Decrement();
        }
    }
}
