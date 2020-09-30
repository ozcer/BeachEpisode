﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            LoadGame();
        }
    }

    void LoadGame()
    {
        print("entered");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}