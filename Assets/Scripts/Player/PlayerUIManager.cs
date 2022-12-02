using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AttackClass;
using PlayerBase;

namespace UIClass
{
    public class PlayerUIManager : MonoBehaviour, IShowDamage
    {
        public static PlayerUIManager instance;
        
        [SerializeField] private Image _lifeBar, _manaBar;

        public Image lifeBar
        {
            get { return _lifeBar; }
            set { _lifeBar = value; }
        }

        public Image manaBar
        {
            get
            {
                return _manaBar;
            }

            set
            {
                _manaBar = value;
            }
        }

        private void Awake()
        {
            instance = this;
        }

        public void ManaUpdate(float maxMana, float currentMana)
        {
            manaBar.fillAmount = currentMana / maxMana;

            if (currentMana <= 0)
            {
                currentMana = 0;
            }

            if (currentMana >= maxMana)
            {
                currentMana = maxMana;
            }
        }

        public void HealthUpdate(float maxLife, float currentLife)
        {
            if (lifeBar != null)
            {
                lifeBar.GetComponent<Image>().fillAmount = currentLife / maxLife;
            }

            if (currentLife <= 0)
            {
                currentLife = 0;
                GameBehaviour.instance.itsOver = true;
            }

            if (currentLife >= maxLife)
            {
                currentLife = maxLife;
            }
        }

        public void LoseLife(int minusLife)
        {
            Player atributes = GetComponent<Player>();
            atributes.currentHealth -= minusLife;

            HealthUpdate(atributes.maxHealth, atributes.currentHealth);
        }

        public void EarnLife(int earnLife)
        {
            Player atributes = GetComponent<Player>();
            atributes.currentHealth += earnLife;

            HealthUpdate(atributes.maxHealth, atributes.currentHealth);
        }

        public void EarnMana(int earnMana)
        {
            Player atributes = GetComponent<Player>();
            atributes.currentMana += earnMana;

            ManaUpdate(atributes.maxMana, atributes.currentMana);
        }

        public void ShowText(string txt, Color txtColor, float damage)
        {
            AttackBase attack = GetComponent<AttackBase>();

            float adjustScale = 1;

            attack.damageText.GetComponentInChildren<Text>().color = txtColor;
            attack.damageText.GetComponentInChildren<Text>().text = txt + damage;

            if(GetComponent<Player>().isRight == false)
            {
                adjustScale *= -1;
                attack.pivot.GetComponent<Transform>().localScale = new Vector3(
                    attack.pivot.transform.localScale.x * -1,
                    attack.pivot.transform.localScale.y,
                    attack.pivot.transform.localScale.z);
            }

            Instantiate(attack.damageText, new Vector3(attack.pivot.transform.position.x,
                                            attack.pivot.transform.position.y,
                                            attack.pivot.transform.position.z),
                                            Quaternion.identity,
                                            attack.pivot.transform);
        }
    }
}