using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            Debug.Log("lo");
            pauseMenuUI.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1 ;
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}