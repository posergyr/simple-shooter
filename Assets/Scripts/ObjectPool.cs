using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private ReturnToPool prefab;

    [Space(10)] [SerializeField] private Transform container;
    [SerializeField] private int minCapacity;
    [SerializeField] private int maxCapacity;
    [Space(10)] [SerializeField] private bool isAutoExpandable;

    private List<ReturnToPool> _pool;

    private void OnValidate()
    {
        if (isAutoExpandable)
        {
            maxCapacity = int.MaxValue;
        }
    }

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _pool = new List<ReturnToPool>(minCapacity);

        for (var i = 0; i < minCapacity; i++)
        {
            CreateElement();
        }
    }

    private ReturnToPool CreateElement(bool isActiveByDefault = false)
    {
        var createdObject = Instantiate(prefab, container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        
        _pool.Add(createdObject);

        return createdObject;
    }

    private bool TryGetElement(out ReturnToPool element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public ReturnToPool GetFreeElement(Vector3 position, Quaternion rotation)
    {
        var element = GetFreeElement(position);
        element.transform.rotation = rotation;
        return element;
    }

    private ReturnToPool GetFreeElement(Vector3 position)
    {
        var element = GetFreeElement();
        element.transform.position = position;
        return element;
    }

    private ReturnToPool GetFreeElement()
    {
        if (TryGetElement(out var element))
        {
            return element;
        }
 
        if (isAutoExpandable)
        {
            return CreateElement(true);
        }

        if (_pool.Count < maxCapacity)
        {
            return CreateElement(true);
        }

        throw new Exception("The limit of pool has been reached");
    }
}
