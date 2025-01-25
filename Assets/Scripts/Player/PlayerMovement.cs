using Mono.Cecil.Cil;
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

    private Rigidbody2D playerRigid;
    private CheckGrounded isGround;
    private SpriteRenderer playerVisual;

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
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
            isWalking = true;
            playerVisual.flipX = false;
        }

        walkingState.Invoke(isWalking);

        playerRigid.linearVelocity = new Vector2(moveInput * playerSettings.WalkSpeed, playerRigid.linearVelocity.y);
    }


    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (isGround.isGrounded)
            {
                playerRigid.AddForce(new Vector2(0f, playerSettings.JumpPower));
                OnJumping.Invoke();
            }
        }
    }

}
