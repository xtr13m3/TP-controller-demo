using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionsManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState curState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Idle = new DefaultState();

    public GameObject curWeapon;
    [HideInInspector] public WeaponAmmo ammo;

    AudioSource audioSource;

    [HideInInspector] public Animator anim;

    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint lHandIK;



    void Start()
    {
        SwitchState(Idle);
        ammo = curWeapon.GetComponent<WeaponAmmo>();
        audioSource = curWeapon.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        curState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        curState = state;
        curState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        ammo.Reload();
        SwitchState(Idle);
    }

    public void Reloading()
    {
        audioSource.PlayOneShot(ammo.reloading);
    }
}
