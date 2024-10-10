﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    public GameObject TestPrefab;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        controller.OnAttackEvent += OnShoot;

        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        aimDirection = direction;
    }

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        if (rangedAttackSO == null) return;

        float projectileAngleSpace = rangedAttackSO.multipleProjectilesAngle;
        int numberOfProjectilesPerShot = rangedAttackSO.numberOfProjectilesPerShot;
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectileAngleSpace + 0.5f * rangedAttackSO.multipleProjectilesAngle;
        for(int i = 0; i < numberOfProjectilesPerShot;  i++)
        {
            float angle = minAngle + i * projectileAngleSpace;
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;
            CreatProjectile(rangedAttackSO, angle);
        }

    }

    private void CreatProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        GameObject ob = Instantiate(TestPrefab);
        ob.transform.position = projectileSpawnPosition.position;
        ProjectileController attackController = GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);
    }

    private static Vector2 RotateVector2(Vector2 v,  float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}

