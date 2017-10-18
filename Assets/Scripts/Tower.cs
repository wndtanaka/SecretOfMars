﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;
    [Header("Tower Attributes")]
    public float range = 5f;
    public float attackSpeed = 1f;
    private float attackTimer = 0f;

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform rotCannon;
    private string enemyTag = "Enemy";
    public float rotSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        // look for target
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 cannonRotation = Quaternion.Lerp(rotCannon.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
        rotCannon.rotation = Quaternion.Euler(0f, cannonRotation.y, 0f);

        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = 1f / attackSpeed;
        }
        attackTimer -= Time.deltaTime;
    }

    void Attack()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.direction = target.position - transform.position;
        if (bullet != null)
        {
            bullet.Fire(target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closest = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closest)
            {
                closest = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && closest <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
