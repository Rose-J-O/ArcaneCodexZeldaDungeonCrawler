using UnityEngine;

public class EquipmentPickup : MonoBehaviour, ICollidable
{
    [SerializeField] EquipmentType _type;
    [SerializeField] int _typeID;

    public void OnCollide(Transform other)
    {
        Debug.Log("Hitting Sword");
        if (other.CompareTag("Player"))
        {
            switch(_type)
            {
                case EquipmentType.Sword:
                    other.GetComponent<PlayerInformation>().AcquireSword(_typeID);
                    break;
            }

            //Fanfare or something
            Destroy(this.gameObject);
        }
    }
}
