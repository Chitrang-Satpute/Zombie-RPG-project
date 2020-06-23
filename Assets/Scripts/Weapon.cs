using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FpsCamera;  
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Transform barrelLocation;
    [SerializeField] GameObject bulletImpact, crosshair;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoTypes ammoTypes;
    Animator animator;

    [SerializeField] float range, timeBetweenShots, reloadTime, baseFieldOfView, zoomInFiledOfView;
    [SerializeField] int shotPower;
    [SerializeField] int ammoPerClip = 7;
    [SerializeField] int currentAmmo;
    private bool isScoped = false;
    bool canShoot = true;
    bool isReloading = false;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        crosshair.SetActive(true);
        currentAmmo = ammoPerClip;
    }

    private void OnEnable()
    {
        if(!canShoot)
        {
            Invoke("WaitToShoot", timeBetweenShots);
        }
        isReloading = false;
    }

    

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        //Weapon Reload Method
        if (currentAmmo <= 0)
        {
            StartCoroutine(ReloadWeapon());
            return;
        }

        //Callin Weapon Shoot Method
        if (Input.GetButtonDown("Fire1")&& canShoot == true && ammoSlot.GetCurrentAmmo(ammoTypes) > 0)
        {
            StartCoroutine(Shoot());    
        }

        //Weapon Zoom-In & Zoom-Out Method
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scope", true);
            FpsCamera.fieldOfView = zoomInFiledOfView;
           
            if (!isScoped)
            {
                StartCoroutine(unScope());
            }
            else
            {
                crosshair.SetActive(false);
            }

        }
        IEnumerator unScope()
        {
            yield return new WaitForSeconds(.25f);
            crosshair.SetActive(true);
            FpsCamera.fieldOfView = baseFieldOfView;
            animator.SetBool("Scope", false);
        }
        
       
    }

   
    //Weapon Shoot Method
    IEnumerator Shoot()
    {
        currentAmmo--;
        canShoot = false;
        MuzzelEffect();
        ProcessRayCast();
        ammoSlot.ReduceCurrentAmmo(ammoTypes);
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    //Weapon Shoting VFX Effect
    private void MuzzelEffect()
    {
        Instantiate(muzzleFlash, barrelLocation.position, Quaternion.identity);
    }

    //RayCast Method
    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FpsCamera.transform.position, FpsCamera.transform.forward, out hit, range))
        {
            BulletImpactEffect(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }
            target.ReduceEnemyHealth(shotPower);
        }
        else
        {
            return;
        }
    }

    //BulletImapact Method
    private void BulletImpactEffect(RaycastHit hit)
    {
        var impactVfx = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactVfx, 1f);
        
    }
    
    //Wating Time Between Each Shoot
    void WaitToShoot()
    {
        canShoot = true;
    }

    //Weapon Reload Method
     IEnumerator ReloadWeapon()
    {
        print("Reloading");
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoPerClip;
        isReloading = false;
    }
}
