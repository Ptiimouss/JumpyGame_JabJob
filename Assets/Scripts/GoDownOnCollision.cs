using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDownOnCollision : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject pilier;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Descendre");
            StartCoroutine(Active(false, gameObject, 3));
        }
    }
    private IEnumerator Active(bool b, GameObject g, float i)
    {
        yield return new WaitForSeconds(i);
        g.SetActive(b);
    }
}