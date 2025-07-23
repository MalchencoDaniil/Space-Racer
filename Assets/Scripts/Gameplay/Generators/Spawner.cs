using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Header("Base")]
    [field: SerializeField] public List<Floor> StartFloors { get; private set; }
    [field: SerializeField] public float TileLength { get; private set; }
    [field: SerializeField] public int StartFloorTileCount { get; private set; } = 3;
}