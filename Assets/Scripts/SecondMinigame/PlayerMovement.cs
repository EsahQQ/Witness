using System;
using UnityEngine;


namespace SecondMinigame
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        private PlayerInputActions _playerInputActions;
        private Rigidbody _rigidbody;
        private Vector3 _currentInputVector;
        private readonly float _minMoveSpeed = 0.1f;
        private float _lastVectorX;
        private bool _canMove = true;

        public event EventHandler OnPlayerTurn;
        public bool IsRunning { get; private set; }
        public static PlayerMovement Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            _rigidbody = GetComponent<Rigidbody>();
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
        }

        private void Start()
        {
            PlayerController.Instance.OnPlayerHide += PlayerCantMove;
            UIManager.Instance.OnNoteShow += PlayerCantMove;
        }

        private void Update()
        {
            if (!_canMove) return;
            
            _currentInputVector = GetMovementVector();
            _currentInputVector = _currentInputVector.normalized;
            if (_currentInputVector.x != 0 && !Mathf.Approximately(Mathf.Sign(_currentInputVector.x), Mathf.Sign(_lastVectorX)))
            {
                OnPlayerTurn?.Invoke(this, EventArgs.Empty);
            }
            
            if (_currentInputVector.x != 0)
            {
                _lastVectorX = _currentInputVector.x;
            }
        }

        private void FixedUpdate()
        {
            if (!_canMove)
            {
                _rigidbody.linearVelocity =  Vector3.zero;
                return;
            } 
            Vector3 targetVelocity = new Vector3(_currentInputVector.x * moveSpeed, _rigidbody.linearVelocity.y, _currentInputVector.z * moveSpeed);
            _rigidbody.linearVelocity = targetVelocity;
        }

        private void OnDestroy()
        {
            PlayerController.Instance.OnPlayerHide -= PlayerCantMove;
            UIManager.Instance.OnNoteShow -= PlayerCantMove;
            _playerInputActions.Disable();
        }

        private void PlayerCantMove(object sender, EventArgs e)
        {
            _canMove = !_canMove;
            if (IsRunning)
                IsRunning = false;
        }

        private Vector3 GetMovementVector()
        {
            Vector3 inputVector = _playerInputActions.Player.Move.ReadValue<Vector3>();
            IsRunning = Mathf.Abs(inputVector.x) >= _minMoveSpeed || Mathf.Abs(inputVector.z) >= _minMoveSpeed;
            return inputVector;
        }
    }
}
