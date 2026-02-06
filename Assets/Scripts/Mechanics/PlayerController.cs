using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.InputSystem;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {


        [Header("Dash")]
        public float dashSpeed = 20f;
        public float dashTime = 0.2f;
        public float dashCooldown = 0.5f;
        public int startDashCount = 1;

        private int dashCount;
        private bool isDashing;
        private float dashTimer;
        private float dashCooldownTimer;

        private InputAction m_DashAction;



        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private InputAction m_MoveAction;
        private InputAction m_JumpAction;
        private InputAction m_AttackAction;
        private InputAction m_BlockAction;


        public Bounds Bounds => collider2d.bounds;

        protected override void Start()
            {
                base.Start(); 

                if (UserInput.instance == null)
                    {
                        Debug.LogError("UserInput is missing from the scene!");
                        enabled = false;
                        return;
                    }

                var controls = UserInput.instance.controls;
                

                m_MoveAction   = controls.Player.Move;
                m_JumpAction   = controls.Player.Jump;
                m_AttackAction = controls.Player.Attack;
                m_DashAction = controls.Player.Dash;
                m_BlockAction = controls.Player.Block;

                
                m_BlockAction.Enable();
                m_DashAction.Enable();
                m_MoveAction.Enable();
                m_JumpAction.Enable();
                m_AttackAction.Enable();

                dashCount = startDashCount;
            }



        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }



        public bool IsBlocking
            {
                get { return animator.GetBool("isBlocking"); }
            }

        public bool IsFacing(Vector2 sourcePosition)
        {
            float dirToSource = sourcePosition.x - transform.position.x;

            // if flipX == false → facing right
            // if flipX == true  → facing left
            return (dirToSource > 0 && !spriteRenderer.flipX) ||
                (dirToSource < 0 && spriteRenderer.flipX);
        }



        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = m_MoveAction.ReadValue<Vector2>().x;

                animator.SetBool("isBlocking", m_BlockAction.IsPressed());

                if (m_DashAction.WasPressedThisFrame() && dashCount > 0 && !isDashing)
                {
                    StartDash();
                }
                
                if (jumpState == JumpState.Grounded && m_JumpAction.WasPressedThisFrame())
                    jumpState = JumpState.PrepareToJump;
                else if (m_JumpAction.WasReleasedThisFrame())
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            
            UpdateJumpState();
            UpdateDash();
            base.Update();


        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        void StartDash()
        {
            isDashing = true;
            dashTimer = dashTime;
            dashCount--;

            float direction = spriteRenderer.flipX ? -1f : 1f;
            velocity = new Vector2(direction * dashSpeed, 0);
        }

        void UpdateDash()
        {
            if (!isDashing) return;

            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }


        protected override void ComputeVelocity()
        {
            if (isDashing)
            {
                targetVelocity = velocity;
                return;
            }

            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;

            if (IsGrounded && dashCount < startDashCount)
            {
                dashCooldownTimer += Time.deltaTime;
                if (dashCooldownTimer >= dashCooldown)
                {
                    dashCount = startDashCount;
                    dashCooldownTimer = 0f;
                }
            }

        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }


    }
}