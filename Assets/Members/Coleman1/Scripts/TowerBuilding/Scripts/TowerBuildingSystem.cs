using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerBuildingSystem : MonoBehaviour
{
    #region Building System Variables

    public static TowerBuildingSystem current; //Singleton reference of our script

    public GridLayout gridLayout;
    private Grid grid;

    [Header("TileMap/TakenTile")]
    [SerializeField] private Tilemap towerTileMap;
    [SerializeField] private TileBase takenAreaTile;

    [Header("List of Tower Prefabs")]
    //All of the prefabs we want to build
    public GameObject[] towerPrefabs;

    private PlacableObjects objectToPlace;

    private void Awake()
    {

    }

    #endregion

    #region Unity Methods



    #endregion
}
