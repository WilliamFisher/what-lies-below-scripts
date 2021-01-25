using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used to test switching characters with left bracket key before the raycast system
public class CharacterTester : MonoBehaviour
{
    [SerializeField]
    private CharacterDatabase _characterDatabase = null;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            int indexToChangeTo = _characterDatabase.currentCharacterIndex + 1;
            if (indexToChangeTo > _characterDatabase.characters.Length - 1)
                indexToChangeTo = 0;

            _characterDatabase.SetCharacterIndex(indexToChangeTo);
        }
    }
}
