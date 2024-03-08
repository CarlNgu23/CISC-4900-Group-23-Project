using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Eye_Dog : MonoBehaviour
{
    [SerializeField] public int hp = 10;
    [SerializeField] public int atk = 5;
    [SerializeField] public int defense = 1;
    [SerializeField] public int exp = 100;
    [SerializeField] private LayerMask player_BasicAttack_Mask;
    public Detection detection;
    private Animator animations;
    private Rigidbody2D rgbd2D;
    private BoxCollider2D hitBox;
    public bool isDying_Ref = false;
    private ExpManager expManager;

    //public void OnEnable()
    //{
    //    expManager.OnReward += ExpCheck;
    //}
    ////The ExpManager will become disabled when nothing happens.
    //public void OnDisable()
    //{
    //    expManager.OnReward -= ExpCheck;
    //}

    void ExpCheck(int exp)
    {

    }

    private void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<BoxCollider2D>();
        animations = GetComponent<Animator>();
        detection = GetComponent<Detection>();
        expManager = GameObject.Find("ExpManager").GetComponent<ExpManager>();
    }

    private void Update()
    {
        AnimationTransition();
        if (hp <= 0)
        {
            isDying_Ref = true;     //A reference to prevent chasing when dying.
            DieAnimation();
        }
    }

    private void AnimationTransition()
    {
        if (rgbd2D.velocity.x == 0f)
        {
            animations.SetBool("isIdle", true);
        }
        if (rgbd2D.velocity.x == detection.moveSpeed || rgbd2D.velocity.x == -detection.moveSpeed)
        {
            animations.SetBool("isIdle", false);
        }
    }

    private void OnTriggerEnter2D()
    {
        if (hitBox.IsTouchingLayers(player_BasicAttack_Mask))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        hp -= ((Stats.Instance.attack + Stats.Instance.strength) - defense);
    }

    private void DieAnimation()
    {
        rgbd2D.velocity = new Vector2(0f, 0f);
        animations.SetBool("isDead", true);
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        expManager.GiveExp(exp);
        Destroy(gameObject);
    }

}