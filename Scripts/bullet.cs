using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(timer());
    }
    public ParticleSystem ps;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        die();
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(2f);
        die();
    }
    void die()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
