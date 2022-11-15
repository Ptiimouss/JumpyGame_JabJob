using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject platform;
    Renderer btn = new Renderer();
    bool hit = false;
    Color color;
    private void Awake()
    {
        btn = GetComponent<Renderer>();
        
    }
    private void Start()
    {
        StartCoroutine(Rainbow());
    }
    IEnumerator Rainbow()
    {
        while (!hit)
        {
            btn.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time, 1), 1, 1)));
            yield return new WaitForSeconds(0.01f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            platform.SetActive(true);
            Destroy(other.gameObject);
            animator.SetTrigger("Monter");
            hit = true;
            if (ColorUtility.TryParseHtmlString("#93FF73", out color))
            {
                btn.material.color = color;
            }
        }
    }
}
