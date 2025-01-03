using System;
using System.Collections;
using Code.Game.Interfaces;
using Code.Game.PlayerLogic.Machine;
using Code.Game.PlayerLogic.Machine.Interfaces;
using Code.Game.PlayerLogic.Machine.States;
using Code.Infrastructure.Interfaces;
using Code.Music;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Game.PlayerLogic
{
    public class Player : MonoBehaviour, IDamageable
    {
        private const float AwaitBeforeJump = 0.5f;
        private const float SetupStateMachineDelay = 0.1f;
        private const int MinHp = 0;
        private const int ZeroDirection = 0;
        
        private readonly GetBounds _getBounds = new();
        
        [SerializeField] private TextMeshProUGUI _candyCountText;
        [SerializeField] private PlayerHpView _playerHpView;
        [SerializeField] private LoseWindow _loseWindow;
        [SerializeField] private PlayerControls _playerControls;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _rayStartPoint;
        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _platformLayer;
        [SerializeField] private PlayerSpineAnimator _playerSpineAnimator;
        [SerializeField] private MeshRenderer _meshRenderer;

        private IPlayerDataSocket _playerDataSocket;
        private IGameSoundPlayer _gameSoundPlayer;
        private IPlayerStateMachine _playerStateMachine;
        
        private int _hp = 3;
        private float _mySize;
        private bool _onGround = true;
        private bool _isDead;
        private bool _canJump = true;

        public bool LockedFull { get; private set; }
        public bool LockedHorizontal { get; private set; }
        public Rigidbody2D Rigidbody => _rigidbody;
        public PlayerControls PlayerControls => _playerControls;
        public PlayerSpineAnimator PlayerSpineAnimator => _playerSpineAnimator;
        public IGameSoundPlayer GameSoundPlayer => _gameSoundPlayer;
        public IPlayerStateMachine PlayerStateMachine => _playerStateMachine;
        public IPlayerDataSocket PlayerDataSocket => _playerDataSocket;

        private Vector2 _movementBounds;

        private int _candyCount;

        [Inject]
        public void Initialize(IPlayerDataSocket playerDataSocket, IGameSoundPlayer gameSoundPlayer)
        {
            _gameSoundPlayer = gameSoundPlayer;
            _playerDataSocket = playerDataSocket;
        }

        public void GetDamage(int damage)
        {
            if (_hp <= MinHp)
                return;
            
            _hp--;
            _playerHpView.UpdateHearts(_hp);
            _gameSoundPlayer.Play(ShortSfx.Damage);

            if (_hp <= MinHp)
                Die();
        }

        public void DoHorizontalMovement()
        {
            Rigidbody.velocity = new Vector2(_playerControls.HorizontalDirection * _playerDataSocket.MovementSpeed, Rigidbody.velocity.y);
        }

        public void AddCandyCount(int count)
        {
            _candyCount += count;
            _candyCountText.text = _candyCount.ToString();
            _gameSoundPlayer.Play(ShortSfx.Collect);
        }

        private void FixedUpdate()
        {
            _playerStateMachine?.CurrentState?.OnFixedUpdate();
        }

        private void Update()
        {
            Clamp();
            _playerStateMachine?.CurrentState?.OnUpdate();
            ControlGrounding();
            _getBounds.Get(Camera.main, out _movementBounds, out _mySize, _meshRenderer);
            CheckDeath();
            Debug.DrawRay(_rayStartPoint.position, Vector2.down * _rayDistance, Color.red);
        }

        private void Awake()
        {
            StartCoroutine(SetupStateMachine());

            _playerHpView.UpdateHearts(_hp);
            
            _getBounds.Get(Camera.main, out _movementBounds, out _mySize, _meshRenderer);
            
            _playerControls.OnJump += Jump;
            _playerControls.OnMove += ToWalkOrIdleState;

            _candyCountText.text = _candyCount.ToString();
        }

        private void OnDestroy()
        {
            _playerControls.OnJump -= Jump;
            _playerControls.OnMove -= ToWalkOrIdleState;
        }

        private IEnumerator SetupStateMachine()
        {
            _playerStateMachine = new PlayerStateMachine();
            _playerStateMachine.Add<PlayerIdleState>(new PlayerIdleState(this));
            _playerStateMachine.Add<PlayerWalkState>(new PlayerWalkState(this));
            _playerStateMachine.Add<PlayerJumpState>(new PlayerJumpState(this));

            yield return new WaitForSeconds(SetupStateMachineDelay);
            
            _playerStateMachine.Enter<PlayerIdleState>();
        }

        private void Clamp()
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -_movementBounds
                .x + _mySize, _movementBounds.x - _mySize), transform.position.y);
        }

        private void Die()
        {
            if (_isDead)
                return;

            _isDead = true;
            LockedFull = true;
            LockedHorizontal = true;
            
            _gameSoundPlayer.Play(ShortSfx.GameOver);
            
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0;
            _rigidbody.gravityScale = 0;

            _meshRenderer.enabled = false;
            
            _loseWindow.SetCandyCount(_candyCount);
            _loseWindow.gameObject.SetActive(true);
        }
        
        private void DoRotationWhenChangeDirection()
        {
            transform.rotation = Quaternion.Euler(Math.Abs(_playerControls.HorizontalDirection - (-1)) < 0.01f ? new 
                Vector3(0, 180, 0) : new Vector3(0, 0, 0));
        }

        private void ToWalkOrIdleState()
        {
            if (_playerControls.HorizontalDirection != ZeroDirection & !_playerStateMachine.CompareStateWithCurrent<PlayerWalkState>())
            {
                DoRotationWhenChangeDirection();
                _playerStateMachine.Enter<PlayerWalkState>();
            }
            else if (_onGround)
            {
                _playerStateMachine.Enter<PlayerIdleState>();   
            }
        }
        
        private void ControlGrounding()
        {
            if (!_onGround & IsTouchingGround())
            {
                _onGround = true;
                EnterIdleState();
            }
            else if (_onGround & !IsTouchingGround())
            {
                _onGround = false;
            }
        }

        private bool IsTouchingGround()
        { 
            return Physics2D.Raycast(_rayStartPoint.position, Vector2.down, _rayDistance, _platformLayer);
        }

        private void Jump()
        {
            if (_onGround & !LockedFull & _canJump)
            {
                _canJump = false;
                _playerStateMachine.Enter<PlayerJumpState>();
                StartCoroutine(ResetJump());
            }
        }

        private void EnterIdleState()
        {
            if (_playerStateMachine.CompareStateWithCurrent<PlayerJumpState>() & _playerControls.HorizontalDirection == ZeroDirection)
                _playerStateMachine.Enter<PlayerIdleState>();
        }

        private void CheckDeath()
        {
            float diePoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, Camera.main.nearClipPlane)).y;
            
            if (transform.position.y < diePoint - _mySize & !_isDead) 
                Die();
        }

        private IEnumerator ResetJump()
        {
            yield return new WaitForSeconds(AwaitBeforeJump);

            _canJump = true;
        }
    }
}