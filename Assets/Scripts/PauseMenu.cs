using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _PauseMenu;
    bool paused = false;

    private void Awake()
    {
        _PauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                _PauseMenu.SetActive(false);
                Time.timeScale = 1f;
                paused = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                _PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                paused = true;
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Home()
    {
        _PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene("Menu");
    }
}