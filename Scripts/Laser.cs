using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laser;
    public Transform spawnPoint;
    private float cooldown;
    private float cooldownTime = 5f;

    private void Start()
    {
        cooldown = cooldownTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            Instantiate(laser, spawnPoint.position, Quaternion.identity);
            cooldown = cooldownTime;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}
