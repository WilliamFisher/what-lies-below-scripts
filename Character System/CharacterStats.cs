using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    public float health { get; private set; }
    [SerializeField]
    private BaseStat _baseHealth;

    public float strength { get; private set; }
    [SerializeField]
    private BaseStat _baseStrength;

    public float speed { get; private set; }
    [SerializeField]
    private BaseStat _baseSpeed;

    [SerializeField]
    private GameObject _StatUIPrefab = null;

    public static event EventHandler onHealthChanged;

    private void Awake()
    {
        health = _baseHealth.CalculatedValue;
        RefreshCalculatedStats(this, EventArgs.Empty);
    }

    private void OnEnable()
    {
        StatsPanelUI.onStatsChanged += RefreshCalculatedStats;
    }

    private void OnDisable()
    {
        StatsPanelUI.onStatsChanged -= RefreshCalculatedStats;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateStatsPanel();
        }
    }

    private void CreateStatsPanel()
    {
        GameObject statUIPanel = Instantiate(_StatUIPrefab, GameObject.Find("Canvas").transform);
        StatsPanelUI statsPanelUI = statUIPanel.GetComponent<StatsPanelUI>();
        statsPanelUI._healthBaseStat = _baseHealth;
        statsPanelUI._speedBaseStat = _baseSpeed;
        statsPanelUI._strengthBaseStat = _baseStrength;
        statsPanelUI.characterData = GetComponent<CharacterBase>().Character;
        Cursor.lockState = CursorLockMode.Confined;
        GetComponentInChildren<CharacterLook>().lockCamRotation = true;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            health = 0;
            //Handle character death
        }
        onHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float amount)
    {
        health += amount;
        if(health > _baseHealth.CalculatedValue)
        {
            health = _baseHealth.CalculatedValue;
        }
        onHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    private void RefreshCalculatedStats(object sender, EventArgs eventArgs)
    {
        strength = _baseStrength.CalculatedValue;
        speed = _baseSpeed.CalculatedValue;
    }

    [ContextMenu("TestAddMod")]
    public void TestAddModifier()
    {
        StatModifier modifier = new StatModifier(50);
        _baseHealth.AddModifier(modifier);
        health = _baseHealth.CalculatedValue;
    }
}
