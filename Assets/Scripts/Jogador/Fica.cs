using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fica : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.parent.position != transform.position)
            transform.parent.position = transform.position;
        transform.localPosition = Vector3.zero;
    }
}
