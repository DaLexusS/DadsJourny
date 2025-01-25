using UnityEngine;

public class FloatLogic : MonoBehaviour
{
    public float floatSpeed = 2f; // How fast the balloon floats upward
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isFloating = false; // Starts as false; toggled externally

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Continuously move up if floating is enabled
        if (isFloating)
        {
            transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0); // Move upward

            // Check if the balloon has reached Y = 45
            if (transform.position.y >= 45f)
            {
                Destroy(gameObject); // Destroy the balloon
                Debug.Log("Balloon destroyed at Y=45.");
            }
        }
    }

    // Public method to enable floating
    public void EnableFloating()
    {
        isFloating = true;
        rb.constraints = RigidbodyConstraints2D.None; // Reset constraints in case they were frozen
    }
}