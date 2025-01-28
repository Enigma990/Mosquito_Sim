using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    [SerializeField] protected int damageAmount = 10;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject attackZone;


    public virtual void OnPlayerEnterObstacleZone(Transform playerTransform)
    {
        if (playerTransform.parent.TryGetComponent(out HealthSystem health))
        {
            attackZone.SetActive(false);
            if (animator)
            {
                animator.SetTrigger("Attack");
            }

            health.Damage(damageAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEnterObstacleZone(other.transform);
    }
}
