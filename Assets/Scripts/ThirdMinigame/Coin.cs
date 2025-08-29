using System;
using UnityEngine;

namespace ThirdMinigame
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 5f;
        private AudioSource _coinPickUpSource;

        public static event EventHandler OnCoinCollected;
        
        private void Awake()
        {
            _coinPickUpSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            OnCoinCollected?.Invoke(this, EventArgs.Empty);
            AudioSource.PlayClipAtPoint(_coinPickUpSource.clip, transform.position);
            Destroy(gameObject);
        }
    }
}
