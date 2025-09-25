using System;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _defensePoints;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _baseAttackPower = 10;
    [SerializeField] private int _attackDamage = 10;


    [Header("Sword Settings")]
    [SerializeField] private bool[] _swordAcquiredArray = new bool[5];
    [SerializeField] private int[] _swordPowerArray = new int[] { 1, 2, 4, 8, 16 };
    [SerializeField] private Transform[] _swordDisplay;
    [SerializeField] private Transform _unarmed;

    Animator _animator;
    Collider _attackCollider;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public float Speed => _speed;
    public int AttackDamage => _attackDamage;

    public bool[] AcquiredSwords => _swordAcquiredArray;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        //SetAttackPower();
    }

    public void CauseDamge(int damageAmount)
    {
        if (_defensePoints > 0 && damageAmount > 0)
        {
            damageAmount -= _defensePoints;
            if (damageAmount < 0) 
                damageAmount = 0;
        }

        _currentHealth -= damageAmount;
        if (_currentHealth < 0) 
            _currentHealth = 0;

        UIManager.Instance.UpdateHealth(_currentHealth);
        _animator.SetTrigger("Damage");

        if (_currentHealth == 0)
            Destroy(this.gameObject); //Change Later
    }

    public void HealDamage(int healAmount)
    {
        _currentHealth += healAmount;
        if ( _currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        UIManager.Instance.UpdateHealth( _currentHealth);
    }   

    public void IncreaseMaxHealth(int increaseAmount, bool fullRestore)
    {
        _maxHealth += increaseAmount;
        if (fullRestore)
            _currentHealth = _maxHealth;
        else
            _currentHealth += increaseAmount;

        UIManager.Instance.UpdateHealth(_currentHealth);
    }

    /// <summary>
    /// Equip(+) or unEquip(-) armor items
    /// </summary>
    public void EquipArmor(int armorDefense)
    {
        _defensePoints += armorDefense;
    }

    [ContextMenu("Update Sword Power")]
    public void SetAttackPower(int index)
    {
        _unarmed.gameObject.SetActive(false);
        foreach (Transform t in _swordDisplay)
            t.gameObject.SetActive(false);


        _attackDamage = _baseAttackPower + _swordPowerArray[index];
        _swordDisplay[index].gameObject.SetActive(true);
        _attackCollider = _swordDisplay[index].GetComponent<Collider>();
        
        //_unarmed.gameObject.SetActive(true);
    }

    public void AcquireSword(int id)
    {
        if(id >= _swordAcquiredArray.Length || id < 0)
        {
            Debug.LogError("THis Sowrd ID does not exist");
            return;
        }
        _swordAcquiredArray[id] = true;
        //SetAttackPower();
    }

    public void ActivateAttackCollider()
    {
        _attackCollider.enabled = !_attackCollider.enabled;
    }
}
