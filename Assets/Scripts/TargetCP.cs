using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetCP : MonoBehaviour
{
    Renderer btn = new Renderer();
    public float MaxDistance = 10;
    [SerializeField] GameObject player;
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
        while (true)
        {
            btn.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time, 1), 1, 1)));
            yield return new WaitForSeconds(0.01f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            if ((player.transform.position - transform.position).sqrMagnitude < MaxDistance * MaxDistance)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}
