using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public ProjectileGun Gun;
   
    void OnTriggerEnter(Collider other)
    {
        Gun.magazineSize = 10000;
    }
}
