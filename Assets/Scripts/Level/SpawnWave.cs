using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create SpawnWave", fileName = "SpawnWave", order = 0)]
public class SpawnWave : ScriptableObject
{
    public List<GameObject> enemies;
    public float spawnRate;
    // number of enemies that spawned in this wave
    public int maxSpawned;
    // to create a burst of enemies spawning at the same spot at the same time
    public bool burst;
}
