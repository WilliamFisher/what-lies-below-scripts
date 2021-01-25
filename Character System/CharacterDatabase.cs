using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_CharacterDatabase", menuName = "Character/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public delegate void OnCharacterChanged();
    public static event OnCharacterChanged onCharacterChanged;

    public CharacterData[] characters = null;

    public int currentCharacterIndex { get; private set; } = 0;

    public string currentCharacterName { get { return characters[currentCharacterIndex].characterName; } }

    public CharacterData currentCharacter { get { return characters[currentCharacterIndex]; } }

    [ContextMenu("Print Character Name")]
    public void PrintCurrentCharacterName()
    {
        Debug.Log(currentCharacterName);
    }

    public void SetCharacterIndex(int index)
    {
        currentCharacterIndex = index;
        onCharacterChanged?.Invoke();
    }


}
