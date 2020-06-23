using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
   
    [SerializeField] AmmoSlot[] ammoSlot;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoTypes ammoTypes;
        public int maxAmmoAmmount ;
    }

     public int GetCurrentAmmo(AmmoTypes ammoTypes)
    {
        return GetAmmoSlot(ammoTypes).maxAmmoAmmount;
    }

    public void ReduceCurrentAmmo(AmmoTypes ammoTypes)
    {
       GetAmmoSlot(ammoTypes).maxAmmoAmmount--;
    }

    public void IncreaseCurrentAmmo(AmmoTypes ammoTypes,int ammoAmount)
    {
        GetAmmoSlot(ammoTypes).maxAmmoAmmount += ammoAmount;
    }

    private AmmoSlot GetAmmoSlot(AmmoTypes ammoTypes)
    {
        foreach(AmmoSlot slot in ammoSlot)
        {
            if(slot.ammoTypes == ammoTypes)
            {
                return slot;
            }
        }
        return null;
    }
}
