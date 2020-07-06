using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{

    public class Weapon : MonoBehaviour
    {
       
        [SerializeField] Camera fpsCamera , weaponCam;
        [SerializeField] ParticleSystem muzzleFlash;
        [SerializeField] Transform barrelLocation;
        [SerializeField] GameObject bulletImpact, crosshair;
        [SerializeField] Ammo ammoSlot;
        [SerializeField] AmmoTypes ammoTypes;
        //[SerializeField] CameraShake cameraShake;

        Animator animator;
      
    



        [SerializeField] float range, timeBetweenShots, reloadTime, baseFieldOfView, zoomInFiledOfView, sperad;
        [SerializeField] int shotPower;
        [SerializeField] int ammoPerClip = 7;
        [SerializeField] int currentAmmo;
      
        private bool isScoped = false;
        bool canShoot = true;
        bool isReloading = false;
        bool startShooting;
        public bool allowbuttonHold;
        [SerializeField] int bulletpershot;


        private void Start()
        {
            animator = GetComponent<Animator>();
            crosshair.SetActive(true);
            currentAmmo = ammoPerClip;
        }

        private void OnEnable()
        {
            if (!canShoot)
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
            //if (currentAmmo <= 0)
            if (Input.GetKey(KeyCode.R) || currentAmmo <= 0)
            {

                StartCoroutine(ReloadWeapon());
                return;
            }

            //Fire Mode
            if (allowbuttonHold)
            {
                startShooting = Input.GetButton("Fire1");//shot untill the fire button is held
             
            }
            else
            {
                startShooting = Input.GetButtonDown("Fire1");//single Shot;
            }



            //Calling Weapon Shoot Method
            if (startShooting && canShoot == true && ammoSlot.GetCurrentAmmo(ammoTypes) > 0)
            {
                StartCoroutine(Shoot());
            }


            //Weapon Zoom-In & Zoom-Out Method
            if (Input.GetButtonDown("Fire2"))
            {
                isScoped = !isScoped;
                animator.SetBool("Scope", true);
                weaponCam.fieldOfView = zoomInFiledOfView;
                

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
                 
                yield return new WaitForSeconds(.30f);
                crosshair.SetActive(true);
                weaponCam.fieldOfView = baseFieldOfView;
                animator.SetBool("Scope", false);
            }


        }


        //Weapon Shoot Method
        IEnumerator Shoot()
        {
            //To do while having Explosion StartCoroutine(cameraShake.Shake(.4f, 0.09f));
            
            for (int i = 1; i <= bulletpershot; i++)
            {
                
                currentAmmo--;
                canShoot = false;
                MuzzelEffect();
                ProcessRayCast();
                ammoSlot.ReduceCurrentAmmo(ammoTypes);
              
                yield return new WaitForSeconds(timeBetweenShots);
                canShoot = true;
            }
           

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
            //spread
            Vector3 shootDirection = fpsCamera.transform.forward;
            shootDirection.x += Random.Range(-sperad, sperad);
            shootDirection.y += Random.Range(-sperad, sperad);

            

            if (Physics.Raycast(fpsCamera.transform.position, shootDirection, out hit, range))
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

        //Waiting Time Between Each Shoot
        void WaitToShoot()
        {
            canShoot = true;
        }

        //Weapon Reload Method
        IEnumerator ReloadWeapon()
        {
            animator.SetTrigger("Reload");
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = ammoPerClip;
            isReloading = false;
        }

   
        }

    }

