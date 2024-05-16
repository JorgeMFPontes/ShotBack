using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpControllerGrappler : MonoBehaviour
{
    public Grappling grap;
    public Rigidbody rb;
    private GameObject rbs;
    public BoxCollider coll;
    private GameObject colls;
    public Transform player, gunContainer, fpsCam;
    private GameObject Player;

    public float pickUpRange;

    public bool equipped;


    private void Awake()
    {
        colls = GameObject.Find("Gun");
        coll = colls.GetComponent<BoxCollider>();
        rbs = GameObject.Find("Gun");
        rb = rbs.GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Transform>();
    }

    private void Start()
    {
        grap.enabled = false;

        //Setup
        if (!equipped)
        {
            grap.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            grap.enabled = true;
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
        grap.enabled = true;
    }

}
