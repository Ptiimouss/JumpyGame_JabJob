using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    bool passed = false;
    Color color;
    Renderer cp = new Renderer();
    [SerializeField] GameObject child;
    void Awake()
    {
        cp = GetComponentInChildren<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && child.gameObject.tag == "CheckPoint")
        {
            FirstPersonController.lastCheckPointPos = transform.position;
            FirstPersonController.lastCheckPointPos.y += 1;
            FirstPersonController.lastCheckPointRot = transform.rotation;
            if (!passed)
            {
                if (ColorUtility.TryParseHtmlString("#32A24F", out color))
                {
                    cp.material.color = color;
                }
                passed = true;
            }
        }
        else if (other.gameObject.CompareTag("Player") && child.gameObject.tag == "Finish")
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("End");
        }
    }
}