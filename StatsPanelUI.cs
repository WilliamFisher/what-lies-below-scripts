using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanelUI : MonoBehaviour
{
    public static event EventHandler onStatsChanged;

    [SerializeField]
    private Text _pointsText;
    [SerializeField]
    private Button _healthButton;
    [SerializeField]
    private Button _speedButton;
    [SerializeField]
    private Button _strengthButton;
    [SerializeField]
    private Button _closeButton;

    public BaseStat _healthBaseStat;
    public BaseStat _speedBaseStat;
    public BaseStat _strengthBaseStat;
    public CharacterData characterData;


    public enum CharacterSkill
    {
        Health,
        Speed,
        Strength
    }

    void Start()
    {
        SetPointsText();
        _healthButton.onClick.AddListener(() => SpendSkillPoint(CharacterSkill.Health));
        _speedButton.onClick.AddListener(() => SpendSkillPoint(CharacterSkill.Speed));
        _strengthButton.onClick.AddListener(() => SpendSkillPoint(CharacterSkill.Strength));
        _closeButton.onClick.AddListener(DestroyStatsUI);
    }

    void DestroyStatsUI()
    {
        Destroy(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.GetComponent<CharacterLook>().lockCamRotation = false;
    }

    void SetPointsText()
    {
        int pointsAvailable = characterData.GetStatPoints();
        if(pointsAvailable > 0)
        {
            if(pointsAvailable == 1)
            {
                _pointsText.text = "1 point available";
            }
            else
            {
                _pointsText.text = pointsAvailable.ToString();
                _pointsText.text += " points available";
            }
        }
        else
        {
            _pointsText.text = "No points available";
        }
    }

    public void SpendSkillPoint(CharacterSkill skill)
    {
        if(characterData.GetStatPoints() == 0) { return; }

        switch (skill)
        {
            case CharacterSkill.Health:
                _healthBaseStat.AddModifier(new StatModifier(25));
                break;
            case CharacterSkill.Speed:
                _speedBaseStat.AddModifier(new StatModifier(2));
                break;
            case CharacterSkill.Strength:
                _strengthBaseStat.AddModifier(new StatModifier(10));
                break;
        }
        characterData.UseStatPoint(skill);
        onStatsChanged?.Invoke(this, EventArgs.Empty);
        SetPointsText();
    }

}
