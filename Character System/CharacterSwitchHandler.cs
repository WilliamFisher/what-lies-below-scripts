using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchHandler : MonoBehaviour, IInteractable
{
    public enum CharacterState
    {
        NPC,
        Character
    }
    [SerializeField]
    private CharacterDatabase characterDatabase = null;
    private CharacterState _state = CharacterState.NPC;
    private CharacterBase _characterInstance;
    [SerializeField]
    private CharacterUIManager _charUIManager;

    public string GetInteractText()
    {
        switch (_state)
        {
            case CharacterState.NPC:
                return "Press E to recruit to settlement";
            case CharacterState.Character:
                return "Press E to switch characters";
            default:
                return "Press E to interact";
        }
    }

    public void Interact(GameObject go)
    {
        switch (_state)
        {
            case CharacterState.NPC:
                CharacterBase characterThatIsRecruiting = go.GetComponent<CharacterBase>();
                if (characterThatIsRecruiting.CurrentSettlement == null)
                {
                    _charUIManager.SpawnWarningMessage("You need to claim a settlement first!");
                    return;
                }
                _characterInstance.RecruitToSettlement(characterThatIsRecruiting.CurrentSettlement);
                characterThatIsRecruiting.CurrentSettlement.SettlementData.AddCharacter(_characterInstance.Character);
                _state = CharacterState.Character;
                break;
            case CharacterState.Character:
                characterDatabase.SetCharacterIndex(GetComponent<CharacterBase>().characterID);
                break;
        }
    }

    void Start()
    {
        if (GetComponent<CharacterBase>().isPlayable)
        {
            _state = CharacterState.Character;
        }
        characterDatabase = Resources.Load<CharacterDatabase>("Characters/Character_CharacterDatabase");
        _characterInstance = GetComponent<CharacterBase>();
    }

    public void SwitchState(CharacterState state)
    {
        _state = state;
    }
}
