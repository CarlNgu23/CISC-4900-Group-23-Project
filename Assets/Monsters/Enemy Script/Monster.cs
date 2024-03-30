using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Monster : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int ReceivedDamage;
    [SerializeField] public float flashtime;
    [SerializeField] public int expReward;
    
    private SpriteRenderer monsterSprite;
    private Color originalColor;


    // Start is called before the first frame update
    public void Start()
    {
        monsterSprite = GetComponent<SpriteRenderer>();
        originalColor = monsterSprite.color;
    }

    // Update is called once per frame
   public void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(int ReceivedDamage)
    { 
        
        health -= ReceivedDamage;
        FlashColor(flashtime);
    }


    void FlashColor(float time)
    {
        monsterSprite.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        monsterSprite.color = originalColor;
    }

    void Die()
    {
        ExpManager.Instance.GiveExp(expReward);
        Destroy(gameObject);
    }
}