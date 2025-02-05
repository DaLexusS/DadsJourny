using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    static public UnityAction<bool> walkingState;
    static public UnityAction OnJumping;

    public GameObject player;
    public GameObject playerSprite;
    public PlayerSettings playerSettings;
    public GameObject GroundCheckerObject;
    public Tutorial tutorial;
    public ParticleSystem jumpDust;
    public ParticleSystem DustRight;
    public ParticleSystem DustLeft;



    private Rigidbody2D playerRigid;
    private CheckGrounded isGround;
    private SpriteRenderer playerVisual;

    private bool wasFalling = false; // ✅ Tracks if the player was falling before landing
    private bool HasEverJumped = false; // ✅ Prevents first-frame issue

    private void Awake()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
        isGround = GroundCheckerObject.GetComponent<CheckGrounded>();
        playerVisual = playerSprite.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        CheckLanding();

        // ✅ Mark that the game has started, so first-frame issues don’t happen
    }
    private void OnEnable()
    {
        HasEverJumped = false;
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (isGround.isGrounded)
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerSounds, SoundName.Player_JumpStart, 0.7f);
                HasEverJumped = true;
                playerRigid.AddForce(new Vector2(0f, playerSettings.JumpPower));
                OnJumping.Invoke();
                if (tutorial)
                {
                    tutorial.Jumped = true;
                }
            }
        }
    }

    void CheckLanding()
    {
        // ✅ Skip the first frame to prevent false landing detection
        if (!HasEverJumped) return;

        // ✅ Detect if the player was falling before landing
        if (wasFalling && isGround.isGrounded)
        {
            SoundManager.Instance.PlaySound(SoundType.PlayerSounds, SoundName.Player_Landing, 0.7f);

            if (jumpDust != null && isGround.isCollidingGround)
            {
                jumpDust.Play(); // ✅ Now correctly plays when landing
            }
        }

        // ✅ Update `wasFalling` for the next frame
        wasFalling = !isGround.isGrounded && playerRigid.linearVelocity.y < 0f;
    }
    void HandleMovement()
    {
        float moveInput = 0f;
        bool isWalking = false;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
            isWalking = true;
            playerVisual.flipX = true;
            if (IsWalkingFast())
            {
                DustRight.Play();
                DustLeft.Stop();
            }

            if (tutorial)
            {
                tutorial.MovedPlayer = true;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
            isWalking = true;
            playerVisual.flipX = false;
            if (IsWalkingFast())
            {
                DustLeft.Play();
                DustRight.Stop();
            }


            if (tutorial)
            {
                tutorial.MovedPlayer = true;
            }
        }

        walkingState.Invoke(isWalking);

        Vector2 currentVelocity = playerRigid.linearVelocity;
        // DustLeft.Stop(); DustRight.Stop() ;
        currentVelocity.x = moveInput * playerSettings.WalkSpeed;
        playerRigid.linearVelocity = currentVelocity;
    }
    public bool IsWalkingFast()
    {
        return playerRigid.linearVelocity.x >= 7f || playerRigid.linearVelocity.x <= -7f;
    }
}






