using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPlayerEnterObstacleZone(Transform playerTransform)
    {
        if (playerTransform.parent.TryGetComponent(out HealthSystem health))
        {
            health.Damage(damageAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEnterObstacleZone(other.transform);
    }
}
