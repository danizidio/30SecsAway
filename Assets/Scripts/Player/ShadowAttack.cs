using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerBase;
public class ShadowAttack : MonoBehaviour {

    public static ShadowAttack instance;
    [SerializeField] GameObject dagger;
    [SerializeField] GameObject daggerSpot;
    [SerializeField] Transform onGround;

    bool _touchGround;
    float timer = 0;
    bool yes = true;

    public bool onTheGround{get{return _touchGround;}}

    private void Start()
    {
        instance = this;
        StartCoroutine(ShadowTime());
        Player.instance.canMove = false;
    }
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= .07f && yes == true)
        {
            ShadowThrowingDagger();
            timer = 0;
        }
        if (Physics2D.Linecast(transform.position, onGround.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            _touchGround = true;
        }
        else
        {
           _touchGround = false;
        }
    }

    public void ShadowThrowingDagger()
    {
        GameObject temp = Instantiate(dagger);
        Vector3 pos = daggerSpot.transform.position;

        temp.transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    IEnumerator ShadowTime()
    {
        yield return new WaitForSeconds(1);
        timer = 0;
        yes = false;
        Player.instance.canMove = true;
        Destroy(this.gameObject);
    }
}
