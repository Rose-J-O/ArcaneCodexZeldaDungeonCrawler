using UnityEngine;
using UnityEngine.UI;

public class SwordSelectUI : MonoBehaviour
{
    [SerializeField] Toggle[] _toggleSlots;
    [SerializeField] Image[] _imageSlots;

    PlayerInformation _player;

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerInformation>();
        SetSwordDisplay();
    }

    public void SetSwordDisplay()
    {
        bool[] swords = _player.AcquiredSwords;

        for (int i = 0; i < swords.Length; i++)
        {
            _toggleSlots[i].interactable = swords[i];
            _imageSlots[i].enabled = swords[i];
        }
    }

   public void ChangeSword(int index)
    {
        if (_toggleSlots[index].isOn)
        {
            Debug.Log($"{_toggleSlots[index].name} is on");
            _player.SetAttackPower(index);
        }
    }
}
