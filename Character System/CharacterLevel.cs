using UnityEngine;
using System;

[Serializable]
public class CharacterLevel
{
    public int Level { get { return _level; } }
    public int ExperienceRequired { get { return _experienceRequired; } }

    [SerializeField]
    private int _level = 0;
    [SerializeField]
    private int _experienceRequired;

    public CharacterLevel(int level, int experience)
    {
        _level = level;
        _experienceRequired = experience;
    }
}