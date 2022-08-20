using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 250.00f;
    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        _rb2D.velocity = Vector2.up * (bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
