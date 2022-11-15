using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseStatus : MonoBehaviour
{
    public static NoiseStatus instance;
    public int NoiseLevel;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Il y a plus d'une instance de NoiceStatus dans la scene");
            return;
        }
        instance = this;
    }
}