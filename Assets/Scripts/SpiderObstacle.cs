using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderObstacle : BaseObstacle
{
    [SerializeField] private GameObject webPrefab;
    [SerializeField] private LayerMask playerLayer;

    private float shootTimer = 1f;
    private float currentTimer = 0f;

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

            //transform.position += moveDir * 5f * Time.deltaTime;

            transform.LookAt(playerTransform);

            currentTimer += Time.deltaTime;

            if (currentTimer >= shootTimer)
            {
                Shoot();
            }


            if (Vector3.Distance(playerTransform.position, transform.position) <= 1f)
            {
                if (playerTransform.parent.TryGetComponent(out HealthSystem health))
                {
                    health.Damage(damageAmount);
                }
            }
        }
    }

    private void Shoot()
    {
        GameObject spawnedWebObject = Instantiate(webPrefab, transform.position, Quaternion.identity);

        spawnedWebObject.transform.LookAt(playerTransform);

        currentTimer = 0;
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
