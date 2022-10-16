using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DieManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private GameMaster gm;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastPos;
    }
    public void Respawn()
    {
        StartCoroutine(RespawnDelay());
    }

    IEnumerator RespawnDelay()
    {
        Time.timeScale = 0.2f; //Slowmotion

        for (int i = 0; i < 5 & virtualCamera.m_Lens.OrthographicSize > 0.2f; i++)
        {
            virtualCamera.m_Lens.OrthographicSize -= 0.05f;//Zoom effect
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;//Removing Slowmotion
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Reload Active scene
    }
}
