using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField] private GameObject bulletPrefab;

    private readonly List<GameObject> _pooledObjects = new List<GameObject>();
    private float _amountToPool = 20.00f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (var i = 0; i < _amountToPool; i++)
        {
            var obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (var t in _pooledObjects)
        {
            if (!t.activeInHierarchy)
            {
                return t;
            }
        }

        return null;
    }
}
