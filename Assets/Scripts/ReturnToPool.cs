using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    public void ReturnObjectToPool()
    {
        gameObject.SetActive(false);
    }
}
