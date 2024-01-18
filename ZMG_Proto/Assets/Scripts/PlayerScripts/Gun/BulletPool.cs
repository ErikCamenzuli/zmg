using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool main;
    public GameObject bulletPrefab;
    public int poolSize = 100;

    private List<Bullet> availableBullets;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        availableBullets = new List<Bullet>();

        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);

            availableBullets.Add(bullet);
        }
    }

    public void PickFromPool(Vector3 position, Vector3 velocity)
    {
        if (availableBullets.Count < 1)
            return;

        availableBullets[0].Activate(position, velocity);
        availableBullets.RemoveAt(0);
    }

    public void AddToPool (Bullet bullet)
    {
        if (!availableBullets.Contains(bullet)) availableBullets.Add(bullet);
    }

}
