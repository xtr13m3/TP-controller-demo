using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Firerate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Props")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    AimManager aim;

    [SerializeField] AudioClip shotSound;
    AudioSource audioSource;

    WeaponAmmo ammo;

    ActionsManager actions;

    WeaponRecoil recoil;

    void Start()
    {
        recoil = GetComponent<WeaponRecoil>();
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimManager>();
        ammo = GetComponent<WeaponAmmo>();
        actions = GetComponentInParent<ActionsManager>();
        fireRateTimer = fireRate;
    }

    void Update()
    {
        if (ShouldFire()) Fire();
        Debug.Log(ammo.curAmmo);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.curAmmo == 0) return false;
        if (actions.curState == actions.Reload) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(shotSound);
        recoil.TriggerRecoil();
        ammo.curAmmo--;
        for(int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
