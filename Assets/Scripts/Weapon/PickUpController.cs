using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public ProjectileGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    private Transform player, gunContainer, fpsCam;
    private GameObject Player, GunContainer, FpsCam;

    public float pickUpRange;

    public bool equipped;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Transform>();
        GunContainer = GameObject.Find("Weapon Holder");
        gunContainer = GunContainer.GetComponent<Transform>();
        FpsCam = GameObject.Find("PlayerCam");
        fpsCam = FpsCam.GetComponent<Transform>();
    }

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            
        }
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E)) PickUp();

    }

    private void PickUp()
    {
        equipped = true;
        

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        gunScript.enabled = true;
    }

}
