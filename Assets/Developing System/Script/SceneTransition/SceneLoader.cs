using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] SceneTransition transition;
    [SerializeField] float animTime = 1f;

    public void StateTransition(int index)
    {
        StartCoroutine(LoadStateCoroutine(index));
    }

    public void SceneTransition(int index)
    {
        StartCoroutine(LoadSceneCoroutine(index));
    }

    public void ExitGame()
    {
        StartCoroutine(ExitGameCoroutine());
    }

    IEnumerator LoadSceneCoroutine(int index)
    {
        transition.TransitionOutScene(index);
        yield return new WaitForSecondsRealtime(animTime);
        SceneManager.LoadScene(index);
    }

    IEnumerator LoadStateCoroutine(int index)
    {
        transition.TransitionInScene(index);
        yield return new WaitForSecondsRealtime(animTime);
    }

    IEnumerator ExitGameCoroutine()
    {
        transition.TransitionOutScene(0);
        yield return new WaitForSecondsRealtime(animTime);
        Application.Quit();
    }
}
