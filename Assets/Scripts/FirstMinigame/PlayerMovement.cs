using System;
using UnityEngine;

namespace FirstMinigame
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float moveAmount = 1.25f;
        [SerializeField] private int linesAmount = 4;
        [SerializeField] private int currentLine = 1;
        private int _moveDirection;
        private Vector3 _targetPosition;
        private bool _canMove = true;

        private void Start()
        {
            PlayerController.Instance.OnPlayerDeath += OnPlayerDeath;
        }

        private void Update()
        {
            if (!_canMove) return;
            if (_moveDirection == 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (currentLine != 1)
                        StartMovement(1);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (currentLine != linesAmount)
                        StartMovement(-1);
                }
            }
            else
            {
                UpdateMovement();
            }
        }

        private void OnDestroy()
        {
            PlayerController.Instance.OnPlayerDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath(object sender, EventArgs e)
        {
            _canMove = false;
        }

        private void StartMovement(int direction)
        {
            _moveDirection = direction;
            _targetPosition = transform.position + new Vector3(0, 0, direction * moveAmount);
            currentLine -= direction;
        }
    
        private void UpdateMovement()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        
            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            {
                transform.position = _targetPosition;
                _moveDirection = 0;
            }
        }
    }
}
