using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ReturnToPool))]

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 250.00f;
    
    private Rigidbody2D _rb2D;
    private ReturnToPool _objectReturnToPool;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        _objectReturnToPool = GetComponent<ReturnToPool>();
    }

    private void OnEnable()
    {
        StartCoroutine(Destroy());
    }

    private void FixedUpdate()
    {
        _rb2D.velocity = Vector2.right * (bulletSpeed * Time.deltaTime);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2.30f);
        _objectReturnToPool.ReturnObjectToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _objectReturnToPool.ReturnObjectToPool();
        }
    }
}
