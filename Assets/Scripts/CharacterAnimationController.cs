using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    private Character character;
    private Dictionary<string, float> animationNameLengthDicitonary = new Dictionary<string, float>();

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponent<Character>();

        character.OnMovePerformed += PerformMove;

        foreach(AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            animationNameLengthDicitonary.Add(clip.name, clip.length);
    }

    public float GetAnimationLength(string name)
    {
        if (animationNameLengthDicitonary.ContainsKey(name))
            return animationNameLengthDicitonary[name];
        throw new System.Exception("The animation of name " + name + " does not exist in the animationLengthDictionary.");
    }

    private void PerformMove()
    {
        animator.SetTrigger("Attack");
    }
}
