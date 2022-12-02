using UnityEngine;
using System.Collections;

namespace AttackClass
{
    public class AttackBase : MonoBehaviour
    {
        #region - Atributes -

        public static AttackBase instance;

        [Header("Funções de Ataque")]

        private bool _isHit;
        private float _damageSuffer;
        private float _voandoX;
        private float _voandoY;
        private float _criticalValue;

        private bool _criticalDamage;

        [SerializeField]
        [Tooltip("O quanto o ataque vai empurrar o inimigo no eixo X")]
        private float _empurrandoX;

        [SerializeField]
        [Tooltip("O quanto o ataque vai empurrar o inimigo no eixo Y")]
        private float _empurrandoY;

        [SerializeField] protected GameObject _damageText;

        [SerializeField] protected Transform _pivot;

        private Animator _animBehaviour;
        #endregion

        #region Properties - 


        public bool isHit
        {
            get
            {
                return _isHit;
            }

            set
            {
                _isHit = value;
            }
        }

        public float voandoX
        {
            get
            {
                return _voandoX;
            }

            set
            {
                _voandoX = value;
            }
        }

        public float voandoY
        {
            get
            {
                return _voandoY;
            }

            set
            {
                _voandoY = value;
            }
        }

        public float empurrandoX
        {
            get
            {
                return _empurrandoX;
            }

            set
            {
                _empurrandoX = value;
            }
        }

        public float empurrandoY
        {
            get
            {
                return _empurrandoY;
            }

            set
            {
                _empurrandoY = value;
            }
        }

        public GameObject damageText
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

        public Animator animBehaviour
        {
            get
            {
                return _animBehaviour;
            }

            set
            {
                _animBehaviour = value;
            }
        }

        public float damageSuffer
        {
            get
            {
                return _damageSuffer;
            }

            set
            {
                _damageSuffer = value;
            }
        }

        public bool criticalDamage
        {
            get
            {
                return _criticalDamage;
            }

            set
            {
                _criticalDamage = value;
            }
        }

        public Transform pivot
        {
            get
            {
                return _pivot;
            }

            set
            {
                _pivot = value;
            }
        }

        public float criticalValue
        {
            get
            {
                return _criticalValue;
            }

            set
            {
                _criticalValue = value;
            }
        }

        #endregion

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            animBehaviour = GetComponent<Animator>();
        }
        public void Attacking()
        {


        }
    }
}




