using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    private CharacterDatabase characterDatabase = null;
    [SerializeField]
    private CharacterData _character = null;
    [SerializeField]
    private GameObject playerCamObject = null;
    [SerializeField]
    private GameObject characterGraphics = null;

    public int characterID;

    private bool isActive = false;
    private Settlement _currentSettlement = null;

    
    public bool isPlayable { get; private set; } = false;
    public CharacterData Character { get { return _character; } }
    public Settlement CurrentSettlement { get { return _currentSettlement; } }

    
    void Start()
    {
        characterID = GetCharacterID();
        InitCharacter();
        if (characterID == 0)
            isPlayable = true;
        FindMySettlement();
    } 

    private void OnEnable()
    {
        CharacterDatabase.onCharacterChanged += InitCharacter;
    }

    private void OnDisable()
    {
        CharacterDatabase.onCharacterChanged -= InitCharacter;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isActive)
        {
            PlayerInventoryManager.Instance.ToggleUI();
        }
    }

    public void RecruitToSettlement(Settlement settlement)
    {
        isPlayable = true;
        _currentSettlement = settlement;
    }

    private int GetCharacterID()
    {
        for(int i=0; i < characterDatabase.characters.Length; i++)
        {
            if (characterDatabase.characters[i] == _character)
            {
                return i;
            }
        }
        throw new Exception("Could not find this character in the database");
    }

    private void InitCharacter()
    {
        if(characterID == characterDatabase.currentCharacterIndex)
        {
            //This is the current active character
            isActive = true;
            playerCamObject.SetActive(true);
            GetComponent<CharacterMovement>().enabled = true;
            GetComponent<CharacterInteract>().enabled = true;
            GetComponent<CharacterStats>().enabled = true;
            gameObject.tag = "Player";
            TogglePlayerGraphics(false);
        }
        else
        {
            isActive = false;
            playerCamObject.SetActive(false);
            GetComponent<CharacterMovement>().enabled = false;
            GetComponent<CharacterInteract>().enabled = false;
            GetComponent<CharacterStats>().enabled = false;
            gameObject.tag = "Interactable";
            TogglePlayerGraphics(true);
        }
    }

    private void TogglePlayerGraphics(bool value)
    {
        characterGraphics.SetActive(value);
    }

    void FindMySettlement()
    {
        try
        {
            Settlement[] theSettlements = GameObject.FindObjectsOfType<Settlement>();
            foreach (Settlement settlement in theSettlements)
            {
                List<CharacterData> characters = settlement.SettlementData.SettlementCharacters;
                foreach (CharacterData character in characters)
                {
                    if (character == this.Character)
                    {
                        _currentSettlement = settlement;
                        return;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error looking for settlements!");
            Debug.LogError(e);
        }
        
    }
}
