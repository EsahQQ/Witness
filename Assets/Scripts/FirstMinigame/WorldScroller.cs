using System;
using UnityEngine;

namespace FirstMinigame
{
    public class WorldScroller : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 5f;
        private bool _canScroll = true;

        private void Start()
        {
            PlayerController.Instance.OnPlayerDeath += OnPlayerDeath;
            PlayerController.Instance.OnPlayerTakeDamage += OnPlayerTakeDamage;
        }



        private void Update()
        {
            if (!_canScroll) return;
            transform.position =  new Vector3(transform.position.x - scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x <= -20)
            {
                float overshot = transform.position.x + 20;
                transform.position =  new Vector3(overshot + 20, transform.position.y, transform.position.z);
            }
            
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
            scrollSpeed = 3f;
            Invoke(nameof(SetScrollSpeed), 3f);
        }

        private void SetScrollSpeed()
        {
            scrollSpeed = 5f;
        }
    }
}
