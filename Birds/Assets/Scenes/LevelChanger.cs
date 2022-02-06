using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;

    private string sceneToLoad = "";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeToScene(string scene)
    {
        animator.SetTrigger("FadeOut");
        sceneToLoad = scene;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
