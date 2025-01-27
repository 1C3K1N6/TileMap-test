using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootAim : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] public Transform gunT;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] public float detectionRadius = 10f; // Радиус обнаружения противников
    [SerializeField] public LayerMask enemyLayer; // слой вражин
   
    private GameObject bulletInst;
    private Vector2 worldPos;
    private Vector2 direction;
    private float angle;
    private void Update()
    {
        //HandleGunRotation();
        HandleGunShooting();
        //RotateHandToMouse();
        AimToEnemy();
        
    }

    
    private void HandleGunRotation()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = worldPos - (Vector2)gun.transform.position.normalized;
        gun.transform.up = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 localScale = new Vector3(1f, 1f, 1f);
        Vector3 localPos = gun.transform.localPosition;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }

        gun.transform.localScale = localScale;
        gun.transform.localPosition = localPos;
    }
    
    void RotateHandToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Убедитесь, что Z-координата равна 0 для 2D

        // Вычисляем направление от руки к мыши
        Vector3 direction = mousePosition - gunT.position;
        direction.Normalize();

        // Вычисляем угол поворота
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Применяем поворот к кости руки
        gunT.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bulletInst = Instantiate(bullet, bulletSpawn.position, gunT.rotation);
        }
    }
    
    Transform FindClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }
    
    void RotateWeaponTowards(Transform target)
    {
        Vector3 direction = target.position - gunT.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunT.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void AimToEnemy()
    {
        Transform closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            RotateWeaponTowards(closestEnemy);
        }
    }
    
}
