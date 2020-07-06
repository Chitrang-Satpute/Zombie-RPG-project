using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] int selectWeapon = 0;
    [SerializeField] Camera FpsCamera;

    Animator animator;

    void Start()
    {
        SelectWeapon();
        animator = GetComponentInChildren<Animator>();
        //animator.keepAnimatorControllerStateOnDisable = false;


    }
    void Update()
    {
        
        int previousSelectedWeapon = selectWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectWeapon >= transform.childCount - 1)
            {
                selectWeapon = 0;
             
            }
            else
            {
                selectWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectWeapon <= 0)
            {
                selectWeapon = transform.childCount - 1;
            }
            else
            {
                selectWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectWeapon = 2;
        }

        if (previousSelectedWeapon != selectWeapon)
        {
            SelectWeapon();
         
        }

        
    }
    private void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if(i == selectWeapon)
            {
                FpsCamera.fieldOfView = 60;
                
                weapon.gameObject.SetActive(true);
            }
            else
            {
                FpsCamera.fieldOfView = 60;
              
                weapon.gameObject.SetActive(false);
            }
            i++;

        }

    }
  

    
   
}
