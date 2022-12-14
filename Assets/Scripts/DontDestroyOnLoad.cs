using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour
{

    private GameObject[] music;

    void Start()
    {
        music = GameObject.FindGameObjectsWithTag("music");
        if (music.Length > 1)
        {
            Destroy(music[1]);
        }
    }

    // Update is called once per frame
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}