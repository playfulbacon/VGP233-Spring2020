using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    private Character character;
    private Dictionary<string, float> animationNamedic = new Dictionary<string, float>();
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponentInChildren<Character>();

        character.OnMovePerformed += PerformMove;

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            animationNamedic.Add(clip.name, clip.length);
        }
    }

    public float GetAnimationLength(string name)
    {
        if (animationNamedic.ContainsKey(name))
            return animationNamedic[name];
        throw new System.Exception("The animation of name " + name + " does not exist in the animationNameLengthDictionary");
    }

    private void PerformMove()
    {
        animator.SetTrigger("attack");
    }

  
}
