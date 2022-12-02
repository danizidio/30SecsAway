using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AttackClass;

public class ShowDamage : MonoBehaviour
{
    public static ShowDamage instance;

    [SerializeField] private Text _damageText;

    GameObject player;
    HealthPointsEnemy enemy;

    private GameObject textobj;

    bool _redText;
    public Text damageText
    {
        get
        {
            return _damageText;
        }

        set
        {
            _damageText = value;
        }
    }

    public bool redText
    {
        get
        {
            return _redText;
        }

        set
        {
            _redText = value;
        }
    }

    void Start()
    {
        instance = this;
        textobj = this.gameObject;

        player = GameObject.FindGameObjectWithTag("Player");

        enemy = gameObject.GetComponent<HealthPointsEnemy>();

        StartCoroutine(Countdown());

        
    }

    //private void LateUpdate()
    //{
    //    if (redText == false)
    //    {
    //        if (player.GetComponent<AttackBase>().criticalDamage)
    //        {
    //            damageText.text = "CRÍTICO!!\n" + player.GetComponent<AttackBase>().damageSuffer.ToString();
    //            damageText.color = Color.yellow;
    //        }
    //        else
    //        {
    //            damageText.text = player.GetComponent<AttackBase>().damageSuffer.ToString();
    //        }
    //    }
    //}

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);

        Destroy(textobj);
    }
}
