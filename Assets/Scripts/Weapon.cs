using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FpsCamera;
    [SerializeField] float range;
    [SerializeField] float shotPower = 50;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Transform barrelLocation;
    [SerializeField] GameObject bulletImpact;
    [SerializeField] GameObject crosshair;
    [SerializeField] float baseFieldOfView;
    [SerializeField] float zoomInFiledOfView;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoTypes ammoTypes;
    [SerializeField] float timeBetweenShots;

    Animator animator;
    private bool isScoped = false;
    bool canShoot = true;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        crosshair.SetActive(true);
    }

    private void OnEnable()
    {
        if(!canShoot)
        {
            Invoke("WaitToShoot", timeBetweenShots);
        }
    }

    

    void Update()
    {
        if (Input.GetButtonDown("Fire1")&& canShoot == true && ammoSlot.GetCurrentAmmo(ammoTypes) > 0)
        {
            StartCoroutine(Shoot());    
        }


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

    IEnumerator Shoot()
    {
        canShoot = false;
        MuzzelEffect();
        ProcessRayCast();
        ammoSlot.ReduceCurrentAmmo(ammoTypes);
        
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void MuzzelEffect()
    {
        Instantiate(muzzleFlash, barrelLocation.position, Quaternion.identity);
    }

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

    private void BulletImpactEffect(RaycastHit hit)
    {
        var impactVfx = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactVfx, 1f);
        
    }
    void WaitToShoot()
    {
        canShoot = true;
    }
}
