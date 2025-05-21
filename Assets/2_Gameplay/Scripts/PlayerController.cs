using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour, IJumpController
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private float airborneSpeedMultiplier = .5f;
        //TODO: This booleans are not flexible enough. If we want to have a third jump or other things, it will become a hazzle.
        //private bool _isJumping;
        //private bool _isDoubleJumping;
        private Character _character;
        private Coroutine _jumpCoroutine;
        private JumpState _currentJumpState;

        private void Awake()
        {
             _character = GetComponent<Character>();
            _currentJumpState = new SingleJumpState(this);
        }

        private void OnEnable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started += HandleMoveInput;
                moveInput.action.performed += HandleMoveInput;
                moveInput.action.canceled += HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed += HandleJumpInput;
        }
        private void OnDisable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.performed -= HandleMoveInput;
                moveInput.action.canceled -= HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed -= HandleJumpInput;
        }

        private void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            var direction = ctx.ReadValue<Vector2>().ToHorizontalPlane();            
            direction *= airborneSpeedMultiplier;
            _character?.SetDirection(direction);
        }

        private void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            //TODO: This function is barely readable. We need to refactor how we control the jumping
             _currentJumpState?.HandleJump();
        }

        public void RunJumpCoroutine()
        {
            if (_jumpCoroutine != null)
                StopCoroutine(_jumpCoroutine);
            _jumpCoroutine = StartCoroutine(_character.Jump());
        }

        private void OnCollisionEnter(Collision other)
        {
            foreach (var contact in other.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    _currentJumpState.OnLanding();
                }
            }
        }

        public void SetJumpState(JumpState state)
        {
            _currentJumpState = state;
        }
    }
}