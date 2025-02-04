﻿using Unity.Mathematics;
using UnityEngine;

public class ExperienceOrbSpawner : MonoBehaviour
{
    [SerializeField] GameObject orbPrefab;
    HealthSystem _healthSystem;

    void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        if (_healthSystem != null)
            _healthSystem.OnDeath += OnDeath;
    }

    void OnDestroy()
    {
        if (_healthSystem != null)
            _healthSystem.OnDeath -= OnDeath;
    }

    void OnDeath() => PoolManager.Spawn(orbPrefab, transform.position, quaternion.identity);
}
