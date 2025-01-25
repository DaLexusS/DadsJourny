using UnityEngine;

public class FloatLogic : MonoBehaviour
{
    public float floatSpeed = 2f; // How fast the balloon floats upward
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isFloating = false; // Starts as false; toggled externally
    private float Mytimer = 0f;
    private bool PopTheBalloon = false;

    [SerializeField] private ParticleSystem particleEffect; // Reference to the Particle System

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Ensure the particle system is stopped at the beginning
        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
        else
        {
            Debug.LogWarning("No ParticleSystem assigned to " + gameObject.name);
        }
    }

    void Update()
    {
        if (PopTheBalloon)
        {
            Mytimer += Time.deltaTime;
            if (Mytimer > 0.1f)
            {
                BaloonPop();
                Debug.Log($"Baloon popped after {Mytimer} seconds");
            }
        }

        
        // Continuously move up if floating is enabled
        if (isFloating)
        {
            transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0); // Move upward
        }
    }

    // Trigger floating
    public void EnableFloating()
    {
        isFloating = true;
        rb.constraints = RigidbodyConstraints2D.None; // Reset constraints in case they were frozen
    }

    // Called when the player jumps on the balloon
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCharacter")) // Ensure it's the player
        {
            PopTheBalloon = true;
        }
    }

    public void BaloonPop()
    {
        ActivateParticleEffect();
        DestroyBalloon();
    }

    private void ActivateParticleEffect()
    {
        if (particleEffect != null)
        {
            particleEffect.Play(); // Play the particle system
            Debug.Log("Particle effect activated.");
        }
    }

    private void DestroyBalloon()
    {
        // Destroy the balloon GameObject after the particle effect finishes playing
        if (particleEffect != null)
        {
            Destroy(gameObject, particleEffect.main.duration); // Wait for the particle system to finish
        }
        else
        {
            Destroy(gameObject); // Destroy immediately if no particle system is assigned
        }

        Debug.Log("Balloon destroyed after collision with player.");
    }
}
