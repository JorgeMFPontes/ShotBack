using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        VidaScript script = other.GetComponent<VidaScript>();
        if (script != null)
        {
            script.MudarPosicaoRespawn();
        }
    }
}
