using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FirstMinigame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<Image> hearts;
        private int _currentHeartsCount = 3;
        
        private void Start()
        {
            PlayerController.Instance.OnPlayerTakeDamage += OnPlayerTakeDamage;
        }

        private void OnDestroy()
        {
            PlayerController.Instance.OnPlayerTakeDamage -= OnPlayerTakeDamage;
        }

        private void OnPlayerTakeDamage(object sender, EventArgs e)
        {
            hearts[_currentHeartsCount - 1].color = Color.dimGray;
            _currentHeartsCount--;
        }
    }
}
