using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CrazyMarble.UI
{
    public class BoostInfoRenderer : MonoBehaviour
    {
        [SerializeField]
        private Image _darkFuelCan;
        [SerializeField]
        private Image _flame;
        public void Render(MarbleBoostController boostInfo)
        {
            float fuelRemaining = boostInfo.Fuel / boostInfo.MaxFuel;
            _darkFuelCan.fillAmount = 1 - fuelRemaining;
            _flame.gameObject.SetActive(boostInfo.IsBoosting);
        }
    }
}