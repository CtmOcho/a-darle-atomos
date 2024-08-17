using UnityEngine;
using UnityEngine.UI;

public class DirectionalMovementOnCollision : MonoBehaviour
{
    public Slider temperatureSlider; 
    public float baseSpeed = 200f; 

    private Rigidbody rb;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomDirection();

        if (temperatureSlider != null)
        {
            temperatureSlider.onValueChanged.AddListener(UpdateSpeed);
            UpdateSpeed(temperatureSlider.value/10);
        }
        
    }

    void FixedUpdate()
    {

        rb.velocity = direction * baseSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        direction = Vector3.Reflect(direction, normal).normalized;

        if (Mathf.Abs(direction.x) < 0.01f)
        {
            direction.x = Random.Range(0.1f, 0.2f);
        }

        // Ensure the new direction is normalized
        direction.Normalize();
    }

    private void SetRandomDirection()
    {

        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        direction = new Vector3(x, y, z).normalized;
    }

    private void UpdateSpeed(float temperature)
    {
        baseSpeed = temperature/10; 
    }
}
