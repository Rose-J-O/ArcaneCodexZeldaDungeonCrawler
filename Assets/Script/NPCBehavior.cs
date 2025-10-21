using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    bool _canEngage;    
    InputSystem_Actions _input;
    [SerializeField] string _speakerName;
    [SerializeField] Sprite _portraitSprite;
    [SerializeField] DialogueSequence _dialogueSequence;


    private void OnEnable()
    {
        _input = new InputSystem_Actions();
        _input.Enable();
        _input.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_canEngage)
        {
            UIManager.Instance.StartDialogueSequence(_dialogueSequence, _speakerName, _portraitSprite);
        }
    }

    private void OnDisable()
    {
        _input.Player.Interact.performed -= Interact_performed;
        _input.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _canEngage = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
            _canEngage = false;
    }
}
