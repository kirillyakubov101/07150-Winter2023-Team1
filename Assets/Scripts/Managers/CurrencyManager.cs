/*
 * Created: 2023-02-15
 * Author: Sean Hall
 * 
 * Updated:
 * 
 * Use: Manages currency used by player to purchase units
 */

using System.Collections;
using System.Collections.Generic;
using System;
using OurGame.Units;
using UnityEngine;

namespace OurGame.Currency
{
    public class CurrencyManager : MonoBehaviour
    {
        public static CurrencyManager _instance;

        // Attributes
        private const int CURRENCY_MIN = 0;             // Lower bound
        private const int CURRENCY_MAX = 999999;        // Upper bound
        private static int currencyValue = 1000;               // Current value

        int currencyPassive = 100;                      // Amount for passive income
        float _time;                                    // Time tracker for passive income
        [SerializeField] float _updateInterval = 2f;    // Frequency of passive income

        public int CurrencyValue { get => currencyValue; }

        // Event declaration
        public static event Action<int> OnCurrencyChanged;

        // Singleton
        private void Awake()
        {
            if (_instance != null) { Destroy(this); }
            else { _instance = this; }
        }

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            _time = 0f;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            while (_time >= _updateInterval)
            {
                currencyValue += currencyPassive;
                //Debug.Log(currencyValue);
                UpdateCurrency(currencyValue);
                _time -= _updateInterval;
            }
        }

        public void AddRemoveCurrency(int value)
        {
            UpdateCurrency(currencyValue += value);
        }

        // Will adjust invoke to use passed parameter
        public void UpdateCurrency(int value)
        {
            OnCurrencyChanged?.Invoke(value);
        }
    }
}

