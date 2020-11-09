using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartWindow : Window
{
    public void Restart()
    {
        //временное решение, перезагрузка сцены
        SceneManager.LoadScene("Main");
    }
}
