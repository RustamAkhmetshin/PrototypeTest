using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartWindow : Window
{
    public void Restart()
    {
        //временное решение, перезагрузка сцены. Нужно дописать инструмент для сериализации даты уровней и динамически их загружать\выгружать.
        SceneManager.LoadScene("Main");
    }
}
