using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour
{
    private Slider healthSlider;
    [SerializeField]
    private BaseStat _playerBaseHealth;

    private void OnEnable()
    {
        CharacterStats.onHealthChanged += UpdateSliderValue;
        StatsPanelUI.onStatsChanged += UpdateSliderValue;
    }

    private void OnDisable()
    {
        CharacterStats.onHealthChanged -= UpdateSliderValue;
        StatsPanelUI.onStatsChanged -= UpdateSliderValue;
    }

    void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.maxValue = _playerBaseHealth.CalculatedValue;
        healthSlider.value = healthSlider.maxValue;
    }

    void UpdateSliderValue(object sender, EventArgs eventArgs)
    {
        healthSlider.value = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().health;
        healthSlider.maxValue = _playerBaseHealth.CalculatedValue;
    }
}
