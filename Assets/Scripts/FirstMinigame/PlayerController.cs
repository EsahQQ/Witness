using System;
using UnityEngine;

namespace FirstMinigame
{
    [RequireComponent(typeof(BoxCollider))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private int playerHealth = 3;
    
        private BoxCollider _boxCollider;
    
        public static PlayerController Instance { get; private set; }
        public event EventHandler OnPlayerDeath;
        public event EventHandler OnPlayerTakeDamage;

        private void Awake()
        {
            Instance = this;
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            playerHealth--;
            OnPlayerTakeDamage?.Invoke(this, EventArgs.Empty);
            if (playerHealth <= 0)
            {
                OnPlayerDeath?.Invoke(this, EventArgs.Empty);
                _boxCollider.enabled = false;
            }
            Debug.Log(playerHealth);
        }
    }
}
