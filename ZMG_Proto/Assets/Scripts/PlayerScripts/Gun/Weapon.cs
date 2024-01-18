using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    BulletPool pool;
    public Transform firstPersonCamera;
    public Transform firePoint;

    public float firePower = 10f;

    public bool isShooting;
    public float fireSpeed;
    public float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        pool = BulletPool.main;
    }

    private void Update()
    {
        if(isShooting)
        {
            if (fireTimer > 0) fireTimer -= Time.deltaTime;
            else
            {
                fireTimer = fireSpeed;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        Vector3 bulletVelocity = firstPersonCamera.forward * firePower;
        pool.PickFromPool(firePoint.position, bulletVelocity);
    }

    public void PullTrigger()
    {
        if (fireSpeed > 0)
            isShooting = true;
        else
            Shoot();
    }
    public void ReleaseTrigger()
    {
        isShooting = false;
        fireTimer = 0;
    }
}
