using System;
using UnityEngine;

namespace SecondMinigame
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private AudioSource stepSource;
        [SerializeField] private AudioSource heartBeatSource;
        
        private PlayerMovement _playerMovement;
        
        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            stepSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_playerMovement.IsRunning)
            {
                if (!stepSource.isPlaying)
                    stepSource.Play();
            }
            else
            {
                stepSource.Stop();
            }
            
            heartBeatSource.pitch = Mathf.Lerp(1f, 2f,Mathf.InverseLerp(15f, 3f, Vector3.Distance(this.transform.position, enemy.transform.position)));
        }
    }
}
