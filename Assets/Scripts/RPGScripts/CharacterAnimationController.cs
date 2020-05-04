using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    public Animator GetAnimator { get { return animator; } }
    private Character character;
    private Dictionary<string, float> animationDictionary = new Dictionary<string, float>();
    private BattleUI BattleUI;
   
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponentInChildren<Character>();

        character.OnMovePerformed += PerformMove;
        FindObjectOfType<BattleController>().gameObject.GetComponent<BattleController>().OnWalk
            += PerformWalk;
       // character.OnDeath += StartCoroutine(PerformOnDeath);
       // character.GetComponentInParent<BattleController>().OnWalk += PerformWalk;

        
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (!animationDictionary.ContainsKey(clip.name))
            {
                animationDictionary.Add(clip.name, clip.length);
            }
        }
    }

    private void Start()
    {
        BattleUI = FindObjectOfType<BattleUI>().gameObject.GetComponent<BattleUI>();
    }

    private void Update()
    {
          if(character.isDead())
        {
            StartCoroutine(PerformOnDeath());
        }
    }



    public float GetAnimationLength(string name)
    {
        if (animationDictionary.ContainsKey(name))
            return animationDictionary[name];
        throw new System.Exception("The animation of name " + name + " does not exist in the animationNameLengthDictionary");
    }

    private void PerformMove()
    {
        animator.SetTrigger("Attack");
    }

    IEnumerator PerformOnDeath()
    {
        animator.SetTrigger("Death");
        BattleUI.SetMoveButtonsInteractable(false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            Debug.Log("Animation end");        
        this.gameObject.SetActive(false);
        BattleUI.SetSwitchButton(true);

    }

    

    private void PerformWalk()
    {
        animator.SetTrigger("Walk");

    }

  
}
