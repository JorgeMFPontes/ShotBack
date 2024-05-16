using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(new Vector3(0, 1f, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NovoJogo()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Save()
    {
        SceneManager.LoadScene("Saves");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Opções");
    }


    public void Sair()
    {
#if UNITY_EDITOR
        //Application.Quit() não funciona no editor por isso alteramos
        //o valor de Unity.isPlaying para falso para interromper a execução
        //do jogo
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}