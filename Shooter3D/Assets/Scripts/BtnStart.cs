using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnStart : MonoBehaviour
{
    public void StartGame()
    {
        print("Start game");
        SceneManager.LoadScene("MainScene");
    }
}
