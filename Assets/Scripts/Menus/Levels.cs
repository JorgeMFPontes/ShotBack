using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Nivel 2");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
