using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using NUnit.Framework;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _languageDropDown;

    private void Start()
    {
        if (_languageDropDown != null)
        {
            _languageDropDown.ClearOptions();

            string[] enumNames = Enum.GetNames(typeof(Language));

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

            foreach (string enumName in enumNames)
            {
                options.Add(new TMP_Dropdown.OptionData(enumName));
            }

            _languageDropDown.AddOptions(options);
        }
    }

    public void SetLanguage()
    {
        DialogueService.Instance.SetLanguage((Language)_languageDropDown.value);
    }
}
