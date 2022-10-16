using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private GameMaster gm;
    public LevelSelector transition;
    public bool isWinningCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isWinningCheckpoint)
            {
                transition.OpenScene("LevelUi");
                return;
            }
            gm.lastPos = transform.position;
        }
    }
}
