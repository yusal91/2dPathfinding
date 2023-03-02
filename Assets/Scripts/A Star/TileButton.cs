using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    [Header("TileType")]
    [SerializeField]
    private TileType tileType;

    public TileType MyTileType { get => tileType; }
}
