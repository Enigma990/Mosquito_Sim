using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderObstacle : BaseObstacle
{
    [SerializeField] private LayerMask playerLayer;

    private Transform playerTransform;

    private bool hasPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayer)
        {
            Vector3 moveDir = (playerTransform.position - transform.position).normalized;

            transform.position += moveDir * 5f * Time.deltaTime;

            transform.LookAt(moveDir);


            if (Vector3.Distance(playerTransform.position, transform.position) <= 1f)
            {
                if (playerTransform.parent.TryGetComponent(out HealthSystem health))
                {
                    health.Damage(damageAmount);
                }
            }
        }
    }

    public override void OnPlayerEnterObstacleZone(Transform playerTransform)
    {
        if (playerTransform.CompareTag("Player"))
        {
            this.playerTransform = playerTransform;

            hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hasPlayer = false;
    }

}
