using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Animator tranAm;
    public void OpenScene(string scene)
    {
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
        tranAm.SetTrigger("start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}
