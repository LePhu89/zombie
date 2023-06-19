using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle")]
    [SerializeField] private Camera cam;
    [SerializeField] private int giveDamage;
    [SerializeField] private float shootingRange;
    [SerializeField] private float fireChange = 15f;
    private float nextTimeShoot = 0f;
    [SerializeField] private Player player;
    [SerializeField] private Transform hand;

    [Header("Rifle Shooting")]
    private int maximumAmmunition = 32;
    private int presentAmmunition;
    [SerializeField] private int mag = 10;
    [SerializeField] private float reloadingTime = 2f;
    private bool setReloading = false;
    [SerializeField] private Animator anim;
    

    [Header("Rifle Effect")]
    [SerializeField] private ParticleSystem muzzleSpark;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject goreEffect;

    private void Awake()
    {
        transform.SetParent(hand);
        presentAmmunition = maximumAmmunition;
        AmmoCount.AmmoCountInstance.UpdateAmmoText(presentAmmunition);
        AmmoCount.AmmoCountInstance.UpdateMagText(mag);
    }
    private void Update()
    {
        if (setReloading) return;

        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeShoot)
        {           
            anim.SetBool("Idle", false);
            anim.SetBool("Fire", true);
            nextTimeShoot = Time.time + 1f / fireChange;
            Shoot();
            
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("FireWalk", true);
        }
        else if(Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("FireWalk", true);
            anim.SetBool("Walk", true);
            anim.SetBool("Reloading", false);
        }
        else
        {
            anim.SetBool("Fire", false);
            anim.SetBool("Idle", true);
            anim.SetBool("FireWalk", false);
        }
    }
    private void Shoot()
    {
        if(mag == 0) return;

        presentAmmunition--;

        if(presentAmmunition <= 0)
        {
            mag--;
        }
        AmmoCount.AmmoCountInstance.UpdateAmmoText(presentAmmunition);
        AmmoCount.AmmoCountInstance.UpdateMagText(mag);

        muzzleSpark.Play();

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitinfo, shootingRange))
        {

            Healths healths = hitinfo.transform.GetComponent<Healths>();
            Enemy1 enemy1 = hitinfo.transform.GetComponent<Enemy1>();
            if (healths != null)
            {
                healths.Takedamage(giveDamage);
                GameObject effect = Instantiate(impactEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(effect, 1f);
            }
            else if(enemy1 != null)
            {
                enemy1.ZombieHitDame(giveDamage);
                GameObject gore = Instantiate(goreEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(gore, 1f);
            }
            LookAt(hitinfo.point);
        }

    }
    private void LookAt(Vector3 position)
    {
        Vector3 lookingPoint = position;
        lookingPoint.y = player.transform.position.y;
        if (Vector3.Distance(lookingPoint, player.transform.position) > 0.1f)
        {
            player.transform.LookAt(lookingPoint);
        }

    }
   
     private IEnumerator Reload()
     {
        player.Movement.enabled = false;
        setReloading = true;
        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadingTime);
        anim.SetBool("Reloading", false);
        presentAmmunition = maximumAmmunition;
        player.Movement.enabled = true;
        setReloading = false;          
     }
}
