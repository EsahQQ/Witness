using System;
using UnityEngine;

namespace FirstMinigame
{
    public class WorldScroller : MonoBehaviour
    {
        [SerializeField] private float normalScrollSpeed = 5f;
        [SerializeField] private float slowedScrollSpeed = 3f;
        [SerializeField] private float slowDownDuration = 3f;
        [SerializeField] private float resetPositionX = -20f;
        [SerializeField] private float wrapAroundDistance = 20f;

        private float _currentScrollSpeed;
        private bool _canScroll = true;
        
        private void Awake()
        {
            _currentScrollSpeed = normalScrollSpeed;
        }

        private void Start()
        {
            PlayerController.Instance.OnPlayerDeath += OnPlayerDeath;
            PlayerController.Instance.OnPlayerTakeDamage += OnPlayerTakeDamage;
        }
        
        private void Update()
        {
            if (!_canScroll) 
                return;
            
            transform.position = new Vector3(transform.position.x - normalScrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x <= resetPositionX)
                transform.position =  new Vector3(transform.position.y + wrapAroundDistance, transform.position.y, transform.position.z);
        }

        private void OnDestroy()
        {
            PlayerController.Instance.OnPlayerDeath -= OnPlayerDeath;
            PlayerController.Instance.OnPlayerTakeDamage -= OnPlayerTakeDamage;
        }

        private void OnPlayerDeath(object sender, EventArgs e)
        {
            _canScroll = false;
        }
    
        private void OnPlayerTakeDamage(object sender, EventArgs e)
        {
            _currentScrollSpeed = slowedScrollSpeed;
            Invoke(nameof(SetScrollSpeed), slowDownDuration);
        }

        private void SetScrollSpeed()
        {
            _currentScrollSpeed =  normalScrollSpeed;
        }
    }
}
