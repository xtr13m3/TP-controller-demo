using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int curAmmo;

    public AudioClip reloading;


    // Start is called before the first frame update
    void Start()
    {
        curAmmo = clipSize;
    }

    public void Reload()
    {
        if(extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - curAmmo;
            extraAmmo -= ammoToReload;
            curAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)
        {
            if(extraAmmo + curAmmo > clipSize)
            {
                int ammoLeft = extraAmmo + curAmmo - clipSize;
                extraAmmo = ammoLeft;
                curAmmo = clipSize;
            }
            else
            {
                curAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
