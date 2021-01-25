using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "Character Data", menuName = "Character/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName = "New Character";
    public GameObject model = null;
    public Sprite characterIcon = null;
    public CharacterLevel CurrentLevel { get { return CalculateCurrentLevel(); } }
    public int Experience { get { return _currentExperience; } set { SetExperience(value); } }

    public delegate void OnPlayerEvent(CharacterData characterData);
    public static event OnPlayerEvent onPlayerExperienceChanged;

    [SerializeField]
    private Inventory inventory = null;

    [SerializeField]
    private CharacterLevel[] characterLevels;

    private int _currentExperience = 0;
    private int _statPointsAvailable = 0;
    private int _currentLevel = 0;

    public Inventory GetInventory()
    {
        return inventory;
    }

    public CharacterLevel CalculateCurrentLevel()
    {
        CharacterLevel tempLevel = characterLevels[0];
        foreach(CharacterLevel level in characterLevels)
        {
            if(_currentExperience >= level.ExperienceRequired)
            {
                tempLevel = level;
            }
        }
        if(tempLevel.Level > _currentLevel)
        {
            _statPointsAvailable++;
        }
        _currentLevel = tempLevel.Level;
        return tempLevel;
    }

    private void SetExperience(int value)
    {
        _currentExperience = value;
        onPlayerExperienceChanged?.Invoke(this);
    }

    public float GetLevelPercentage()
    {
        try
        {
            CharacterLevel theNextLevel = characterLevels.First(element => element.Level == CurrentLevel.Level + 1);
            int amountToGet = theNextLevel.ExperienceRequired - CurrentLevel.ExperienceRequired;
            int amountSoFar = _currentExperience - CurrentLevel.ExperienceRequired;

            return amountSoFar / (float)amountToGet;

        } catch (Exception e)
        {
            if(e.GetType() == typeof(InvalidOperationException))
            {
                Debug.LogWarning("No more levels to acheive!");
                return 1;
            }
            Debug.LogError("Error calculating percentage!" + e);
        }
        return 1;
    }

    public void UseStatPoint(StatsPanelUI.CharacterSkill skill)
    {
        if(_statPointsAvailable > 0)
        {
            _statPointsAvailable--;
        }
    }

    public int GetStatPoints()
    {
        return _statPointsAvailable;
    }

    [ContextMenu("Reset Experience")]
    public void ResetExperience()
    {
        _currentExperience = 0;
        onPlayerExperienceChanged?.Invoke(this);
    }
}
