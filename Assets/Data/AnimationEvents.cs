using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Awake()
    {
        CheckGrounded.onLanded += onLanded;
        PlayerMovement.walkingState += IsWalking;
        PlayerMovement.OnJumping += Jumping;
        CheckGrounded.AirBorn += OnAirBorn;
    }
    private void onLanded()
    {
        animator.SetTrigger("Landed");
    }

    public void OnAirBorn(bool state)
    {
        animator.SetBool("AirBorn", state);
    }

    private void IsWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    private void Jumping()
    {
        animator.SetTrigger("Jump");
    }
    private void OnEnable()
    {
        animator.SetTrigger("OnEnable");
    }
    private void OnDestroy()
    {
        CheckGrounded.onLanded -= onLanded;
        PlayerMovement.walkingState -= IsWalking;
        PlayerMovement.OnJumping -= Jumping;
        CheckGrounded.AirBorn -= OnAirBorn;
    }
}
