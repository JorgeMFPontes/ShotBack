using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing: MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip;
    public Transform cam, player;
    private GameObject Cam;
    private GameObject Player;
    public LayerMask whatIsGrappleable;
    public PlayerMovementAdvanced pm;

    [Header("Swinging")]
    private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("OdmGear")]
    public Transform orientation;
    private GameObject Orientation;
    public Rigidbody rb;
    private GameObject RB;
    public float horizontalThrustForce;
    public float forwardThrustForce;
    public float extendCableSpeed;

    [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;
    private GameObject PredictionPoint;

    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse0;

    private bool swinging;

    private void Awake()
    {
        Cam = GameObject.Find("PlayerCam");
        cam = Cam.GetComponent<Transform>();
        Player = GameObject.Find("Player");
        player = gameObject.GetComponent<Transform>();
        Orientation = GameObject.Find("Orientation");
        orientation = Orientation.GetComponent<Transform>();
        RB = GameObject.Find("Player");
        rb = RB.GetComponent<Rigidbody>();
        PredictionPoint = GameObject.Find("Prediction_Point");
        predictionPoint = PredictionPoint.GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();

        CheckForSwingPoints();

        if (joint != null) OdmGearMovement();
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void CheckForSwingPoints()
    {
        if (joint != null) return;

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position, predictionSphereCastRadius, cam.forward,
                            out sphereCastHit, maxSwingDistance, whatIsGrappleable);

        RaycastHit raycastHit;
        Physics.Raycast(cam.position, cam.forward,
                            out raycastHit, maxSwingDistance, whatIsGrappleable);

        Vector3 realHitPoint;

        // Option 1 - Direct Hit
        if (raycastHit.point != Vector3.zero)
            realHitPoint = raycastHit.point;

        // Option 2 - Indirect (predicted) Hit
        else if (sphereCastHit.point != Vector3.zero)
            realHitPoint = sphereCastHit.point;

        // Option 3 - Miss
        else
            realHitPoint = Vector3.zero;

        // realHitPoint found
        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }
        // realHitPoint not found
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }


    private void StartSwing()
    {
        // return if predictionHit not found
        if (predictionHit.point == Vector3.zero) return;

        // deactivate active grapple
        if (GetComponent<Grappling>() != null)
            GetComponent<Grappling>().StopGrapple();
        pm.ResetRestrictions();

        pm.swinging = true;

        swingPoint = predictionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

        // the distance grapple will try to keep from grapple point. 
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        // customize values as you like
        joint.spring = 4f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;

        swinging = true;
    }

    public void StopSwing()
    {
        pm.swinging = false;

        lr.positionCount = 0;

        Destroy(joint);

        swinging = false;
    }

    private void OdmGearMovement()
    {
        // right
        if (Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
        // left
        if (Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);

        // forward
        if (Input.GetKey(KeyCode.W)) rb.AddForce(orientation.forward * horizontalThrustForce * Time.deltaTime);

        // shorten cable
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
    }

    private Vector3 currentGrapplePosition;

    private void DrawRope()
    {
        // if not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition =
            Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool isSwinging()
    {
        return swinging;
    }
    public Vector3 SwingPoint()
    {
        return predictionHit.point;
    }
}
