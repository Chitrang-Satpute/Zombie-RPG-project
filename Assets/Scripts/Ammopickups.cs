using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammopickups : MonoBehaviour
{
    [SerializeField] AmmoTypes ammoTypes;
    [SerializeField] int ammoAmount = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") ;
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoTypes, ammoAmount);
            Destroy(gameObject);
        }
    }
}
