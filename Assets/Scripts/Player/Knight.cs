using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBase;
using AttackClass;
using UIClass;

public class Knight : Player
{
    [SerializeField] GameObject flameOne;
    [SerializeField] Transform flameSpawner;
    [SerializeField] GameObject shield;

    void Start ()
    {
        canMove = true;
        isRight = true;

        currentMana = maxMana;
        currentHealth = maxHealth;

        GetComponent<PlayerUIManagerKnight>().StartUIPoints();
    }

    void Update ()
    {
        if (canMove)
        {
            DefaultMoving();

            Jumping();

            if (Input.GetKey(KeyCode.C))
            {
                animBehaviour.SetBool("attack", true);
            }
            else
            {
                animBehaviour.SetBool("attack", false);
            }

            if (Input.GetKeyDown(KeyCode.B) && currentMana >= _class.costShadow)
            {
                currentMana -= _class.costShadow;

                PlayerUIManager.instance.ManaUpdate(maxMana, currentMana);

                FireAttack();
            }

            if (Input.GetKeyDown(KeyCode.X) && currentMana >= _class.costMobility)
            {
                currentMana -= _class.costMobility;

                PlayerUIManager.instance.ManaUpdate(maxMana, currentMana);

                ActiveShield();
            }
        }

        if (currentMana < maxMana)
        {
            StartCoroutine(CoolDownToRegenMana());
            if (canRegenMana)
            {
                ManaRegen(manaRegen);
            }
        }
        else
        {
            canRegenMana = false;
        }
    }
    
    public void ActiveShield()
    {
        shield.GetComponent<Animator>().Play("ShieldAnim");
    }

    public void FireAttack()
    {
        GetComponent<Animator>().Play("PowerAttackAnim");
    }

    public void SummonFire()
    {
        Instantiate(flameOne,new Vector3(flameSpawner.position.x, flameSpawner.position.y), Quaternion.identity);
    }


}
