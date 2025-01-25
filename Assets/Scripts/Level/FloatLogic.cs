using UnityEngine;

public class FloatLogic : MonoBehaviour
{
    public float floatSpeed = 2f; // How fast the balloon floats upward
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isFloating = false; // Starts as false; toggled externally
    private float Mytimer = 0f;
    private PopBaloon isPopded;

    [SerializeField] private GameObject particleEffect;
    [SerializeField] public GameObject Baloon;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isPopded = Baloon.GetComponent<PopBaloon>();
    }

    void Update()
    {
        if (isPopded.PopTheBalloon)
        {
            Mytimer += Time.deltaTime;
            if (Mytimer > 0.2f)
            {
                BaloonPop();
            }
        }

        if (isFloating)
        {
            transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0); 
        }
    }

    public void EnableFloating()
    {
        isFloating = true;
        rb.constraints = RigidbodyConstraints2D.None;
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
            particleEffect.gameObject.SetActive(true); // Play the particle system

        }
    }

    private void DestroyBalloon()
    {
        Baloon.SetActive(false);
    }
}