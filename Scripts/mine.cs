using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine : MonoBehaviour
{
    public LayerMask mask;
    public ParticleSystem ps;

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(explode());
    }
    IEnumerator explode()
    {
        yield return new WaitForSeconds(1);
        Collider2D[] toEffect = Physics2D.OverlapCircleAll(transform.position, 0.2f, mask);
        for (int i = 0; i < toEffect.Length; i++)
        {
            toEffect[i].GetComponent<DieManager>().Respawn();
        }
        Destroy(gameObject);
        Instantiate(ps, transform.position, Quaternion.identity);
    }
}
