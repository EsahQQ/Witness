using System;
using UnityEngine;
using System.Collections;

namespace ThirdMinigame
{
    public class PlayerController : MonoBehaviour
    {
        private int _coinsCollected;
        [SerializeField] private AudioSource coinPickUp;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                _coinsCollected++;
                coinPickUp.Play();
                Destroy(other.gameObject);
            }
            
            if (other.gameObject.CompareTag("Exit"))
            {
                GameManager.Instance.CompleteLevel(_coinsCollected);
                _coinsCollected = 0;
            }
        }
    }
}
