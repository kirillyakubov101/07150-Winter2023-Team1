/*
 * Created: 2023-03-14
 * Author: Sean Hall
 * 
 * Updated: 
 * 
 * Use: Updates UI Text Element for Unit cost in palette
 */

using System.Collections;
using System.Collections.Generic;
using OurGame.Units;
using TMPro;
using UnityEngine;

namespace OurGame.Spawn
{
    public class SetUnitCost : MonoBehaviour
    {
        [SerializeField] private FriendlySpawner fSpawn;


        TextMeshProUGUI _textMesh;

        private void Awake() => _textMesh = GetComponent<TextMeshProUGUI>();

        private void Start()
        {
            
        }
    }
}
