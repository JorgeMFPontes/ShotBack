using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VidaScript : MonoBehaviour
{
    public void MudarPosicaoRespawn()
    {
        posicaoRespawn = gameObject.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        DanoScript danoScript = other.GetComponent<DanoScript>();
        if (danoScript != null)
        {
            vidaAtual = vidaAtual - danoScript.dano;
            if (vidaAtual <= 0)
            {
                if (fazerRespawn == true)
                {
                    gameObject.transform.SetPositionAndRotation(posicaoRespawn, this.transform.rotation);

                    vidaAtual = vidaInicial;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        posicaoRespawn = gameObject.transform.position;

        vidaAtual = vidaInicial;
    }

    // Update is called once per frame
    void Update()
    {

        if (imagemVida != null)
        {
            imagemVida.fillAmount = (float)vidaAtual / vidaInicial;
        }
    }
    public int vidaInicial = 100;

    public bool fazerRespawn = false;

    private Vector3 posicaoRespawn;

    public int vidaAtual;

    public Image imagemVida;
}




