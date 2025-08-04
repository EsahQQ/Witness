using System;
using System.Collections.Generic;
using NUnit.Framework;
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
            PlayerController.Instance.OnPlayerTakeDamage += ChangeHeartsCount;
        }

        private void OnDestroy()
        {
            PlayerController.Instance.OnPlayerTakeDamage -= ChangeHeartsCount;
        }

        private void ChangeHeartsCount(object sender, EventArgs e)
        {
            hearts[_currentHeartsCount - 1].color = Color.dimGray;
            _currentHeartsCount--;
        }
    }
}
