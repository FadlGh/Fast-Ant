using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject door;
    private bool shouldOpen = false;
    private bool destroyed = false;
    public float speed;
    private Animator am;

    private void Start()
    {
        am = GetComponent<Animator>();
    }
    private void Update()
    {
        if (shouldOpen & !destroyed)
        {
            Destroy(door);
            shouldOpen = false;
            am.SetBool("pressed", true);
            destroyed = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        shouldOpen = true;
    }
}
