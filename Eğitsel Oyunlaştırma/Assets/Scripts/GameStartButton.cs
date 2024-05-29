using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public GameObject kolayButton;
    public GameObject ortaButton;
    public GameObject zorButton;
    public GameObject ayarlarButton;
    public GameObject extraButton;
    public GameObject oyunButton;
    private void Start()
    {
        kolayButton.SetActive(false);
        ortaButton.SetActive(false);
        zorButton.SetActive(false);
    }
    

    public void Easy()
    {
        PlayerPrefs.SetInt("SelectedDifficulty", 0); // 0: Kolay
        SceneManager.LoadScene(1);
    }

    public void MidDiff()
    {
        PlayerPrefs.SetInt("SelectedDifficulty", 1); // 1: Orta
        SceneManager.LoadScene(1);
    }

    public void Hard()
    {
        PlayerPrefs.SetInt("SelectedDifficulty", 2); // 2: Zor
        SceneManager.LoadScene(1);
    }

    public void GameStart()
    {
        oyunButton.SetActive(false);
        ayarlarButton.SetActive(false);
        extraButton.SetActive(false);
        kolayButton.SetActive(true);
        ortaButton.SetActive(true);
        zorButton.SetActive(true);
    }
}
