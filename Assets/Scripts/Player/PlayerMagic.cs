using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public GameObject fireBall;
    public Transform mSpawnTransform;    
    public float speed = 30f;
    [SerializeField]
    PlayerAnimationEventHandler animationEventHandler;

    private bool isCasting;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        animationEventHandler.OnMagicCast += () => { isCasting = true; };
        
    }

    private void Update()
    {
        if(isCasting)
        {
            CheckMana();
            isCasting = false;


            //Test Code
            //Debug.Log(transform.forward);
            //fireballInsta.transform.position = transform.position + transform.forward;
            //fireballInsta.transform.localRotation = transform.rotation;
            //fireballRigidBody.velocity = new Vector3(fireballInsta.transform.position.x, 0, fireballInsta.transform.position.z) * speed * Time.deltaTime;
            ////fireballRigidBody.AddForce(new Vector3(fireballInsta.transform.position.x, 0, fireballInsta.transform.position.z) * speed );
            //Debug.Log(fireballInsta.transform.position);
        }
    }

    private void CheckMana()
    {
        PlayerStats playerstats = FindObjectOfType<PlayerStats>();
        var fireballInsta = Instantiate(fireBall, mSpawnTransform.transform.position, mSpawnTransform.rotation);
        if (playerstats.CurrentMana >= fireballInsta.GetComponent<MagicSpell>().MagicCost)
        {
            Rigidbody fireballRigidBody = fireballInsta.GetComponent<Rigidbody>();
            fireballRigidBody.AddForce(transform.forward * speed);
            playerstats.SpellCast(fireballInsta);
        }
        else
            Destroy(fireballInsta);
    }
   
}
