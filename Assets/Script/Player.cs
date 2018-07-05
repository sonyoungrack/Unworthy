using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [Header("Ability")]
    public int health = 100;
    public int maxHealth = 100;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public int damage = 1;
    public float attackDelay = 1f;
    public float moveSpeed = 1f;
    public float rollingInvincibilityTime = 1f;

    [Space]
    [Header("UI")]
    public Image healthBar;
    public Image staminaBar;
    public Image guardGage;
    [Space]
    [Header("Charging Speed")]
    public float staminaChargingPerSecend = 1f;
    public float guardChargingPerSecend = 1f;
    [Space]
    [Header("Item")]
    public int PortionHillRange = 10;
    public Weapon weapon;
    [Space]
    [Header("Reduction amount")]
    public float attackStamina = 15f;
    public float rollingStamina = 20f;
    [HideInInspector]
    public Vector3 distance;
    public Vector3 currentPosition;
    private Rigidbody2D rigid;
    private Animator anim;
    private bool staminaCharging = true;
    private float invincibilityDelay = 0f;
    private bool invincibility = false;
    private bool guard = true;
    private float guardGageDelay = 0f;
    // Use this for initialization
    private void Start () 
    {
        currentPosition = transform.position;
	}
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Guard()
    {
        guard = false;
        guardGageDelay = 0f;
        guardGage.fillAmount = 0f;
    }
    public void DrinkingPortion()
    {
        
    }
    public void MinusHealth(int damage)
    {
        if (invincibility)
            return;
        else if (guard)
        {
            Guard();
            return;
        }

        var nowHealth = health;
        health -= (health - damage >= 0) ? damage : health;
        if (healthBar != null)
            StartCoroutine(MinusHealthBar());
    }
    public IEnumerator MinusHealthBar()
    {
        while(healthBar.fillAmount>=(float)health/maxHealth)
        {
            healthBar.fillAmount -= Time.deltaTime;
            yield return 0;
        }
        healthBar.fillAmount = (float)health / maxHealth;
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoAttack();
        }
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            if (!weapon.attacking)
            {
                anim.Play("Work");
                var angle = (Input.GetAxisRaw("Horizontal") == 1f) ? 0f : 180f;

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                rigid.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigid.velocity.y);
            }
        }
        else
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }
        distance = transform.position - currentPosition;
        currentPosition = transform.position;
        if (staminaCharging)
        {
            if(stamina < maxStamina)
                stamina += maxStamina * Time.deltaTime / staminaChargingPerSecend;
            staminaBar.fillAmount = stamina / maxStamina;
        }
        if(invincibility)
        {
            invincibilityDelay += Time.deltaTime;
            if (invincibilityDelay >= rollingInvincibilityTime)
                invincibility = false;
        }
        if(!guard)
        {
            guardGageDelay += Time.deltaTime / guardChargingPerSecend;
            guardGage.fillAmount = guardGageDelay;
            if(guardGageDelay>=1f)
            {
                guard = true;
                guardGageDelay = 0f;
            }
        }
    }
    public IEnumerator MinusStamina()
    {
        while(staminaBar.fillAmount>stamina/maxStamina)
        {
            staminaCharging = false;
            stamina -= Time.deltaTime;
            yield return 0;
        }
        staminaCharging = true;
        staminaBar.fillAmount = stamina / maxStamina;
    }
    public void GoAttack()
    {
        if (stamina < attackStamina)
            return;
        stamina -= attackStamina;
        anim.Play("Attack");
        weapon.GoAttack(damage);
    }
    public void GoRolling()
    {
        if (stamina < rollingStamina)
            return;
        stamina -= rollingStamina;
        anim.Play("Rolling");
        Invincibility();
    }
    public void Invincibility()
    {
        invincibilityDelay = 0f;
        invincibility = true;
    }
}
