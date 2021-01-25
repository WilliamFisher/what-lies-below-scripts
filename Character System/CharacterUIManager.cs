using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField]
    private CharacterDatabase _characterDatabase = null;

    [SerializeField]
    private Image _characterIcon = null;

    [SerializeField]
    private Text _characterNameText = null;

    [SerializeField]
    private Text _characterLevelText = null;

    [SerializeField]
    private Slider _characterExperienceSlider = null;

    [SerializeField]
    private GameObject _warningMessagePrefab;

    private void OnEnable()
    {
        CharacterDatabase.onCharacterChanged += Init;
        CharacterData.onPlayerExperienceChanged += UpdateCharacterLevelUI;
    }

    private void OnDisable()
    {
        CharacterDatabase.onCharacterChanged -= Init;
        CharacterData.onPlayerExperienceChanged -= UpdateCharacterLevelUI;
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        CharacterData characterData = _characterDatabase.currentCharacter;
        _characterIcon.sprite = characterData.characterIcon;
        _characterNameText.text = characterData.characterName;

        UpdateCharacterLevelUI(characterData);
    }

    private void UpdateCharacterLevelUI(CharacterData characterData)
    {
        string levelText = "Level ";
        levelText += characterData.CurrentLevel.Level.ToString();
        _characterLevelText.text = levelText;

        _characterExperienceSlider.value = characterData.GetLevelPercentage();
    }

    public void SpawnWarningMessage(string message)
    {
        GameObject warningMessage = Instantiate(_warningMessagePrefab, gameObject.transform);
        warningMessage.GetComponent<Text>().text = message;
    }

}
