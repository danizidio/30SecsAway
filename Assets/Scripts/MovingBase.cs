//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using PlayerBase;

//public class MovingBase : MonoBehaviour
//{
//    #region - Atributes -

//    [SerializeField] private float _speed;
//    [SerializeField] private float _speedJump;

//    private int count, count2 = 1;

//    private float timer = 0;
//    private Animator _animBehaviour;

//    private bool _isRight;

//    private int _isRightInt;

//    private bool _dashCooldown;
//    #endregion

//    #region - Properties -


//    public float speed
//    {
//        get
//        {
//            return _speed;
//        }

//        set
//        {
//            _speed = value;
//        }
//    }

//    public float speedJump
//    {
//        get
//        {
//            return _speedJump;
//        }

//        set
//        {
//            _speedJump = value;
//        }
//    }

//    public Animator animBehaviour
//    {
//        get
//        {
//            return _animBehaviour;
//        }

//        set
//        {
//            _animBehaviour = value;
//        }
//    }

//    public bool isRight
//    {
//        get
//        {
//            return _isRight;
//        }

//        set
//        {
//            _isRight = value;
//        }
//    }
//    public int isRightInt
//    {
//        get
//        {
//            return _isRightInt;
//        }

//        set
//        {
//            _isRightInt = value;
//        }
//    }

//    public bool dashCooldown
//    {
//        get
//        {
//            return _dashCooldown;
//        }

//        set
//        {
//            _dashCooldown = value;
//        }
//    }

//    #endregion

//    public void DefaultMoving(int mobility)
//    {
//        if (isRight)
//        {
//            isRightInt = 1;
//        }
//        else
//        {
//            isRightInt = -1;
//        }

//        animBehaviour = GetComponent<Animator>();
//        Vector2 pos2 = GetComponent<Player>().transform.position;
//        animBehaviour.SetFloat("walk", Mathf.Abs(Input.GetAxis("Horizontal")));

//        if(Input.GetAxis("Horizontal") > 0)
//       // if (Input.GetKey(KeyCode.RightArrow))
//        {
//            isRight = true;
//            transform.Translate(Vector2.right * speed);
//            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

//            if (Input.GetKey(KeyCode.V) & ((GetComponent<Atributes>().currentMana >= 2 & dashCooldown == false)))
//            {
//                dashCooldown = true;
//                SpecialDash(true);
//                StartCoroutine(DashCoolDown());
//                GetComponent<Atributes>().currentMana -= 2;
//                transform.position = new Vector3(pos2.x + 10, pos2.y, 20);
//            }
//        }

//        if (Input.GetAxis("Horizontal") < 0)
//        //if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            isRight = false;
//            transform.Translate(Vector2.left * speed);
//            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

//            if (Input.GetKey(KeyCode.V) & ((GetComponent<Atributes>().currentMana >= mobility & dashCooldown == false)))
//            {
//                dashCooldown = true;
//                SpecialDash(true);
//                StartCoroutine(DashCoolDown());
//                GetComponent<Atributes>().currentMana -= mobility;
//                transform.position = new Vector3(pos2.x - 10, pos2.y, 20);
//            }
//        }
//    }

//    public void CanJump()
//    {
//        this.GetComponent<Rigidbody2D>();
//        if (Player.instance.isOnGround)
//            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speedJump * 100));
//    }
//    public void CanDoubleJump()
//    {
//        this.GetComponent<Rigidbody2D>();
//        if (!Player.instance.isOnGround)
//        {
//            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speedJump * 100));
//        }
//        Atributes.instance.currentMana -= Player.instance.costMobility;
//    }
//    public void Jumping()
//    {
//        this.GetComponent<Animator>();
//        //Pulo só é habilitado enquanto o footDetector do player estiver encostado na layer "Ground"
//        if ((Physics2D.Linecast(transform.position, Player.instance.footDetector1.position, 1 << LayerMask.NameToLayer("Ground")) | (Physics2D.Linecast(transform.position, Player.instance.footDetector1.position, 1 << LayerMask.NameToLayer("FloatingPlatform")) | (Physics2D.Linecast(transform.position, Player.instance.footDetector1.position, 1 << LayerMask.NameToLayer("PushPull"))))) | (Physics2D.Linecast(transform.position, Player.instance.footDetector2.position, 1 << LayerMask.NameToLayer("Ground")) | (Physics2D.Linecast(transform.position, Player.instance.footDetector2.position, 1 << LayerMask.NameToLayer("FloatingPlatform")) | (Physics2D.Linecast(transform.position, Player.instance.footDetector2.position, 1 << LayerMask.NameToLayer("PushPull"))))))
//        {
//            Player.instance.isOnGround = true;
//            this.GetComponent<Animator>().SetBool("jump", false);
//            count = 1;
//            count2 = 1;
//        }
//        else
//        {
//            Player.instance.isOnGround = false;
//            this.GetComponent<Animator>().SetBool("jump", true);
//        }

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            CanJump();

//            if (((Input.GetKeyDown(KeyCode.Space) && Player.instance.isOnGround == false) && Atributes.instance.currentMana >= Player.instance.costMobility))
//            {

//                if (count > 0 && count2 > 0)
//                {
//                    count -= 1;
//                    count2 -= 1;
//                    CanDoubleJump();
//                }
//            }
//        }
//    }

//    public void SpecialDash(bool dash)
//    {
//        timer = 0;
//    }

//    public IEnumerator DashCoolDown()
//    {
//        yield return new WaitForSeconds(1);

//        dashCooldown = false;

//        print(dashCooldown);
//    }
//}
