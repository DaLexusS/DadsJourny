using UnityEngine;

public class FloatLogic : MonoBehaviour
{
    [SerializeField] public float floatSpeed = 2f;
    [SerializeField] public float maxHeight = 5f;

    void Update()
    {
        if (transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);
        }
    }
}
