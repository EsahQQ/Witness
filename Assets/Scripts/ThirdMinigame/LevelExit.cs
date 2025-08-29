using System;
using UnityEngine;

namespace ThirdMinigame
{
    public class LevelExit : MonoBehaviour
    {
        public static event EventHandler OnPlayerExit;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                OnPlayerExit?.Invoke(this, EventArgs.Empty);
        }
    }
}
