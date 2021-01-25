using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTextHandler : MonoBehaviour
{
    private static ActionTextHandler _instance;

    public static ActionTextHandler Instance { get { return _instance; } }

    private Text actionText = null;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        actionText = GetComponent<Text>();
    }

    public void SetText(string text)
    {
        actionText.text = text;
    }

    public void Clear()
    {
        actionText.text = "";
    }
}
