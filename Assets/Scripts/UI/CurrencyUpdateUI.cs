/*
 * Created: 2023-02-15
 * Author: Sean Hall
 * 
 * Updated: 2023-03-08  Sean Hall   Added int value to observer
 * 
 * Use: Updates UI Text Element
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace OurGame.Currency
{

    public class CurrencyUpdateUI : MonoBehaviour
    {
        TextMeshProUGUI _textMesh;

        // Get (and cache) the text component on the UI Element this script is attached to
        private void Awake() => _textMesh = GetComponent<TextMeshProUGUI>();

        private void OnEnable() => CurrencyManager.OnCurrencyChanged += UpdateValue;

        private void OnDisable() => CurrencyManager.OnCurrencyChanged -= UpdateValue;


        private void UpdateValue(int value)
        {
            //Debug.Log(value);
            _textMesh.text = value.ToString();
        }

        //private void Update()
        //{
        //    _textMesh.text = "Test";
        //}
    }
}
