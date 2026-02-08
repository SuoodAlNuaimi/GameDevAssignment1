using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class VictoryZone : MonoBehaviour
    {
        public WinManager winManager;

        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                // Show win UI + sound
                winManager.ShowWin();

                // Optional: stop player movement
                p.controlEnabled = false;

                // Optional: stop time
                Time.timeScale = 0f;
            }
        }
    }
}
