using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using System.Text;
using System.Runtime.CompilerServices;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] TMP_Text _text;
    [SerializeField] Image _speakerImage;
    [SerializeField] TMP_Text _speakerName;

    InputSystem_Actions _input;

    Dictionary<int, string> _dialogue = new Dictionary<int, string>();
    [SerializeField] DialogueSequence _sequence;
    StringBuilder _sentenceBuilder = new StringBuilder();

    private void OnEnable()
    {
        _input = new InputSystem_Actions();
        _input.Dialogue.Enable();
        _input.Player.Disable();

        _input.Dialogue.Submit.performed += Submit_performed;
    }

    private void Start()
    {
        _dialogue.Add(0, "Hello");
        _dialogue.Add(1, "World!");
        LoadDialogue(_sequence);
    }

    private void Submit_performed(InputAction.CallbackContext obj)
    {
      
    }

    public void LoadDialogue(DialogueSequence sequence)
    {
        _sequence = sequence;
        gameObject.SetActive(true);
        StartCoroutine(LoadDialogueText(0));
    }

    IEnumerator LoadDialogueText(int id)
    {
        yield return null;
        string sentence = _dialogue[_sequence.dialogueIDs[id]];
        _sentenceBuilder.Clear();
        int count = 0;
        while (count < sentence.Length)
        {
            _sentenceBuilder.Append(sentence[count]);
            _text.text = _sentenceBuilder.ToString();
            count++;
            yield return Helpers.GetWait(.25f);
        }
    }

    private void OnDisable()
    {
        _input.Dialogue.Disable();
        _input.Player.Enable();

        _input.Dialogue.Submit.performed -= Submit_performed;
    }
}
