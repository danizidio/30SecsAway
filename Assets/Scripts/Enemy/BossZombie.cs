using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossZombie : MonoBehaviour {

    HealthPointsEnemy healthEnemy;
    EnemyBase enemyBase;
    Trajectory trajectory;
    [SerializeField] Transform enemyView1;
    [SerializeField] Transform enemyView2;
    [SerializeField] Transform viewLine;

    [SerializeField] PhysicsMaterial2D bouncingMaterial;

    [SerializeField] private bool heroFar;

    private float jumpCooldown;
    public float hopHeight;
    private bool hopping = false;


    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();
        enemyBase.SearchingNames();
        enemyBase.thatName = enemyBase.enemyNames[Random.Range(0, enemyBase.enemyNames.Length)].ToString();

    }
    void Start()
    {
        
        enemyBase = GetComponent<EnemyBase>();
        enemyBase.healthPotion = null;
        enemyBase.animBehaviour = GetComponent<Animator>();
        healthEnemy = GetComponent<HealthPointsEnemy>();

        heroFar = false;
        healthEnemy.enemyName.GetComponent<Text>();

        healthEnemy.enemyName.text = enemyBase.thatName.ToString();

        enemyBase.chanceToGiveCoin = Random.value * 10;

        enemyBase.AtributesBonusForEnemies(LevelUpManager.instance.currentLevel);

        healthEnemy.HealthStart();
        GameBehaviour.instance.bossSpawn = false;
    }

    void Update()
    {
        healthEnemy.HealthUpdate();
        GetComponent<EnemyBase>().BossIsDead();
        LineCasters();
        JumpAttack();

        if (healthEnemy.GetComponent<HealthPointsEnemy>().currentHealth <= 0)
        {
            Cronometro.instance.relogio = false;
            StartCoroutine(TimeToCollect());
        }

        if (GetComponent<EnemyBase>().lookingHero)
        {
            GetComponent<EnemyBase>().Attack();
        }
        else
        {
            GetComponent<EnemyBase>().Routine2();
        }
    }

    void LineCasters()
    {
        if (Physics2D.Linecast(viewLine.position, enemyView2.transform.position, 1 << LayerMask.NameToLayer("Player")) && GetComponent<EnemyBase>().lookingHero == false)
        {
            heroFar = true;
        }
        else
        {
            heroFar = false;
        }

        if (Physics2D.Linecast(viewLine.position, enemyView1.position, 1 << LayerMask.NameToLayer("Player")))
        {
            GetComponent<EnemyBase>().lookingHero = true;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            GetComponent<EnemyBase>().lookingHero = false;
        }

        if (Physics2D.Linecast(viewLine.position, GetComponent<EnemyBase>().enemyView.position, 1 << LayerMask.NameToLayer("Player")))
        {
            GetComponent<EnemyBase>().lookingHero = true;
        }
        else
        {
            GetComponent<EnemyBase>().lookingHero = false;
        }

        if (Physics2D.Linecast(transform.position, GetComponent<EnemyBase>().groundDetector.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            GetComponent<EnemyBase>().touchGround = true;
        }
        else
        {
            GetComponent<EnemyBase>().touchGround = false;
        }

        if (Physics2D.Linecast(transform.position, GetComponent<EnemyBase>().wallDetector.position, 1 << LayerMask.NameToLayer("Wall")))
        {
            GetComponent<EnemyBase>().touchWall = true;
        }
        else
        {
            GetComponent<EnemyBase>().touchWall = false;
        }
    }

    void JumpAttack()
    {
        Animator anim = GetComponent<Animator>();

        if (heroFar && jumpCooldown <= 0 )
        {
            StartCoroutine(Hop(new Vector2(GetComponent<EnemyBase>().player.transform.position.x,
                GetComponent<EnemyBase>().player.transform.position.y), 1f));
        }
        if (GetComponent<EnemyBase>().touchGround == false)
        {
            heroFar = true;

            anim.SetBool("far", true);
            anim.SetBool("walking", false);
            anim.SetBool("Idle", false);
            anim.SetBool("attacking", false);
            GetComponent<Rigidbody2D>().sharedMaterial = bouncingMaterial;
            
        }
        else
        {
            anim.SetBool("far", false);
            anim.SetBool("walking", false);
            anim.SetBool("Idle", true);
            anim.SetBool("attacking", false);
            GetComponent<Rigidbody2D>().sharedMaterial = null;

            jumpCooldown += Time.deltaTime;

            if(jumpCooldown >= 3)
            {
                jumpCooldown = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-100, 100), ForceMode2D.Impulse);

            Component damageable = collision.gameObject.GetComponent(typeof(IPlayerCombat));
            if (damageable)
            {
                (damageable as IPlayerCombat).EnemyHitPlayer(GetComponent<EnemyBase>().enemyDamage,
                    collision.GetComponent<PlayerBase.Player>().playerArmor, collision.GetComponent<PlayerBase.Player>().currentHealth);
            }
        }
    }

void OnDrawGizmosSelected()
    {
        if (GetComponent<EnemyBase>().player != null)
        {
            Gizmos.DrawLine(viewLine.position, enemyView2.transform.position);
            Gizmos.DrawLine(viewLine.position, GetComponent<EnemyBase>().enemyView.position);
            Gizmos.DrawLine(viewLine.position, enemyView1.position);
            Gizmos.DrawLine(transform.position, GetComponent<EnemyBase>().player.transform.position);
        }
    }

    IEnumerator Hop(Vector3 dest, float time)
    {
        if (hopping) yield break;

        hopping = true;
        var startPos = transform.position;
        var timer = 0.0f;
        
        while (timer <= 1.0f)
        {
            var height = Mathf.Sin(Mathf.PI * timer) * hopHeight;
            transform.position = Vector3.Lerp(startPos, dest, timer) + Vector3.up * height;

            timer += Time.deltaTime / time;
            yield return null;
        }
        hopping = false;
    }
    IEnumerator TimeToCollect()
    {
        yield return new WaitForSeconds(10);

        GameBehaviour.instance.nextState = GameStates.STAGECLEAR;
    }
}

