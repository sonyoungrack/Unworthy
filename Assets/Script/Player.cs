using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Weapon
{
    Sword,
    Bow,
    Hammer
}
public class Player : MonoBehaviour {
    [Header("Ability")]
    public Weapon weapon;
    public int health = 100;
    public int maxHealth = 100;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public int damage = 1;
    public float attackDelay = 1f;
    public float moveSpeed = 1f;
    public float rollingDistance = 2f;
    public float knockback = 1f;

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
    public int havePortion = 1;
    public Text havePortionText;
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
    private bool guard = true;
    private float guardGageDelay = 0f;
    private bool attacking = false;
    private float attackingDelay = 0f;
    private bool rolling = false;
    private float rollingDelay = 0f;
    private float staminaChargingDelay = 0f;
    private bool canAttack = true;
    private float canAttackDelay = 0f;
    [Space]
    [Header("ColliderBranch")]
    public ColliderBranch attackRangeCollider;
    [Space]
    [Header("Prefab")]
    public GameObject arrowPrefab;
    public GameObject arrowDirectPrefab;
    
    private void Start () 
    {
        currentPosition = transform.position;
        havePortionText.text = "x" + havePortion;
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
        if(havePortion>0)
        {
            health = (health + PortionHillRange > maxHealth) ? maxHealth : health + PortionHillRange;
            StartCoroutine(HealthBarControll(true));
            havePortion--;
            havePortionText.text = "x" + havePortion;
        }
    }
    public void MinusHealth(int damage)
    {
        if (rolling)
            return;
        else if (guard)
        {
            Guard();
            return;
        }
        rigid.AddForce(new Vector2(-knockback, 0f));
        var nowHealth = health;
        health -= (health - damage >= 0) ? damage : health;
        if (healthBar != null)
            StartCoroutine(HealthBarControll(false));
    }
    public IEnumerator HealthBarControll(bool isPlus)
    {
        if (!isPlus)
        {
            while (healthBar.fillAmount >= (float)health / maxHealth)
            {
                healthBar.fillAmount -= Time.deltaTime;
                yield return 0;
            }
        }
        else
        {
            while (healthBar.fillAmount <= (float)health / maxHealth)
            {
                healthBar.fillAmount += Time.deltaTime;
                yield return 0;
            }
        }
        healthBar.fillAmount = (float)health / maxHealth;
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoAttack();
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if(!attacking&&!rolling)
            {
                anim.Play("Run");
                anim.SetBool("Running", true);
                var angle = (Input.GetAxisRaw("Horizontal") == 1f) ? 0f : 180f;

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                rigid.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigid.velocity.y);
            }
        }
        else
        {
            anim.SetBool("Running", false);
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            GoRolling();
        }
        distance = transform.position - currentPosition;
        currentPosition = transform.position;
        if (staminaCharging)
        {
            if(stamina < maxStamina)
                stamina += maxStamina * Time.deltaTime / staminaChargingPerSecend;
            staminaBar.fillAmount = stamina / maxStamina;
        }
        else
        {
            staminaChargingDelay += Time.deltaTime;
            if(staminaChargingDelay>=1f)
            {
                staminaChargingDelay = 0f;
                staminaCharging = true;
            }
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
        if(attacking)
        {
            attackingDelay += Time.deltaTime;
            var tempTime = anim.GetBool("DoubleAttack") ? 0.9f : 0.6f;
            if (attackingDelay >= tempTime)
            {
                attackingDelay = 0f;
                attacking = false;
                if (attackRangeCollider.mob != null)
                {
                    attackRangeCollider.mob.GetComponent<Enemy>().MinusHealth(damage);
                }
            }
        }
        if(!canAttack)
        {
            canAttackDelay += Time.deltaTime;
            if(canAttackDelay>=attackDelay)
            {
                canAttackDelay = 0f;
                canAttack = true;
            }
        }
        if(rolling)
        {
            rollingDelay += Time.deltaTime;
            var angle = (transform.localRotation.y == -1) ? -1 : 1;
            rigid.MovePosition(new Vector2(transform.position.x+(angle*Time.deltaTime*rollingDistance),transform.position.y));
            if (rollingDelay >= 0.5f)
            {
                rollingDelay = 0f;
                rolling = false;
                gameObject.layer = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            DrinkingPortion();
        }
    }
    public IEnumerator MinusStamina()
    {
        while(staminaBar.fillAmount>stamina/maxStamina)
        {
            staminaCharging = false;
            staminaBar.fillAmount -= Time.deltaTime;
            staminaChargingDelay = 0f;
            yield return 0;
        }
        staminaBar.fillAmount = stamina / maxStamina;
    }
    public void GoAttack()
    {
        if (stamina < attackStamina||rolling||!canAttack)
            return;
        if(weapon!=Weapon.Bow)
        {
            stamina -= attackStamina;
            StartCoroutine(MinusStamina());
            if (attacking)
            {
                anim.SetBool("DoubleAttack", true);
                return;
            }
            else
            {
                anim.SetBool("DoubleAttack", false);
                anim.Play("Attack");
            }
            attacking = true;
            canAttack = false;
        }
        else
        {
            ShoutArrow();
        }
    }
    public void ShoutArrow()
    {
        var go=Instantiate(arrowPrefab);
        var pos = transform.position;
        pos.y += 2f;
        go.transform.position = pos;
        go.transform.localRotation = transform.localRotation;
        go.tag = "PlayerWeapon";
    }
    public void GoRolling()
    {
        if (stamina < rollingStamina||rolling||attacking)
            return;
        stamina -= rollingStamina;
        StartCoroutine(MinusStamina());
        anim.Play("Roll");
        rolling = true;
        gameObject.layer = 8;
    }
}
