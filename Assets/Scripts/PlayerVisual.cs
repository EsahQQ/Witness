using System;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private static readonly int Damage = Animator.StringToHash("Damage");
    private static readonly int Death = Animator.StringToHash("Death");
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerController.Instance.OnPlayerDeath += OnPlayerDeath;
        PlayerController.Instance.OnPlayerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnDestroy()
    {
        PlayerController.Instance.OnPlayerDeath -= OnPlayerDeath;
        PlayerController.Instance.OnPlayerTakeDamage -= OnPlayerTakeDamage;
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        _animator.SetBool(Death, true);
    }
    
    private void OnPlayerTakeDamage(object sender, EventArgs e)
    {
        _animator.SetTrigger(Damage);
    }
}
