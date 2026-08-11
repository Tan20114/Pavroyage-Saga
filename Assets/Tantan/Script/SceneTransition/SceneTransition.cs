using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Animator inSceneTransitionAnimator;
    [SerializeField] Animator outSceneTransitionAnimator;

    public void TransitionInScene(int stateIndex)
    {
        inSceneTransitionAnimator.SetTrigger($"TriggerState{stateIndex}");
    }

    public void TransitionOutScene(int index)
    {
        outSceneTransitionAnimator.SetTrigger("SceneOut");
    }
}