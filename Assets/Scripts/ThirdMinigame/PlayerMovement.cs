using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace ThirdMinigame
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float tileSize = 1f;
        [SerializeField] private float moveDuration = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        
        private SceneThreeInput _playerInputActions;
        private bool _isMoving = false;

        private void Awake()
        {
            _playerInputActions = new SceneThreeInput();
            _playerInputActions.Enable();
        }

        private void Start()
        {
            _playerInputActions.Player.Move.started += PlayerMove;
        }

        private void OnDestroy()
        {
            _playerInputActions.Player.Move.started -= PlayerMove;
        }

        private void PlayerMove(InputAction.CallbackContext obj)
        {
            if (_isMoving)
            {
                return;
            }
            
            Vector2 inputVector = obj.ReadValue<Vector2>();
            Vector3 moveDirection = Vector3.zero;
            
            if (inputVector.y > 0)      // W
                moveDirection = Vector3.forward;
            else if (inputVector.y < 0) // S
                moveDirection = Vector3.back;
            else if (inputVector.x > 0) // D
                moveDirection = Vector3.right;
            else if (inputVector.x < 0) // A
                moveDirection = Vector3.left;
            
            if (moveDirection != Vector3.zero)
            {
                if (!CanMove(moveDirection)) return;
                StartCoroutine(MoveToTile(moveDirection));
            }
        }

        private bool CanMove(Vector3 direction)
        {
            Vector3 targetPosition = transform.position + direction * tileSize;
            return Physics.Raycast(targetPosition, Vector3.down, 1f, groundLayer);
        }
                
        private IEnumerator MoveToTile(Vector3 direction)
        {
            _isMoving = true;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition + direction * tileSize;

            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition;
            _isMoving = false;
        }
    }
}
