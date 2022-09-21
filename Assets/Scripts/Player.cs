using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject weapon;
    public GameObject player;
    public GameObject gameoverUI;

    public int speed;
    public float jumpForce = 10f;
    public float gravity = -9.81f;
    public Camera followCamera;

    public static int coin;
    public static int life;
    public static int hasGrenades;

    public int startCoin;
    public int startLife;
    public int startHasGrenades;
    public GameObject grenadeObj;

    public int maxCoin;
    public int maxHealth;
    public int maxHasGrenades;

    public GameObject crossHairObj;
    

    float hAxis;
    float vAxis;
    

    bool gDown;
    bool jDown;
    bool fDown;
    bool rDown;

    bool isJump;
    bool isReload;
    bool isFireReady = true;
    bool isBorder;
    bool isDamage;

    Vector3 moveVec;
    Vector3 saveVec;

    Vector3 playerPos;

    Rigidbody rigid;

    Animator anim;

    Weapon equipWeapon;

    public GameObject lifeUI;
    Transform[] lifes;

    CapsuleCollider cap;
    CharacterController charactercontroller;


    float fireDelay;
    float count = 0f;
    float savePoint = 1f;

    private void Awake()
    {

        coin = startCoin;
        life = startLife;
        hasGrenades = startHasGrenades;

        player = GetComponent<GameObject>();
        equipWeapon = weapon.GetComponent<Weapon>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        lifes = lifeUI.GetComponentsInChildren<Transform>(true);
        cap = GetComponent<CapsuleCollider>();
        charactercontroller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init_Cursor();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        //MoveTo(new Vector3(hAxis, 0, vAxis).normalized);
        Move();
        Jump();
        Turn();
        Attack();
        Reload();
        Grenade();
        Aiming();
        PlayerPos();
        Fall();
        //CheckGround();

        //if (charactercontroller.isGrounded == false)
        //{
        //    Debug.Log("낙하");
        //    moveVec.y += gravity * Time.deltaTime;
        //}

        if (hAxis != 0 && vAxis != 0)
        {
            saveVec = moveVec;
        }
    }

    

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");
        gDown = Input.GetButtonDown("Grenade");
        rDown = Input.GetButtonDown("Reload");
        
    }

    void Aiming()
    {
        RectTransform crossHair = crossHairObj.GetComponent<RectTransform>();
        crossHair.transform.position = Input.mousePosition;
    }

    void Init_Cursor()
    {
        crossHairObj.SetActive(true);
        Cursor.visible = false;
    }

    void MoveTo(Vector3 direction)
    {
        if (charactercontroller.isGrounded)
        {
            moveVec = new Vector3(direction.x, 0f, direction.z);
            return;
        }
        moveVec = new Vector3(direction.x, moveVec.y, direction.z);
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // 공격중엔 정지
        //if (!isFireReady)
        //    moveVec = Vector3.zero;

        //if (jDown && charactercontroller.isGrounded == true)
        //{
        //    //rigid.AddForce(Vector3.up * 40, ForceMode.VelocityChange);
        //    moveVec.y = jumpForce;
        //    isJump = true;
        //}



        if (isDamage)
            moveVec = Vector3.zero;
        //뛰기
        if (!isBorder)
            transform.position += moveVec * speed * Time.deltaTime;
        //rigid.MovePosition(moveVec * speed * Time.deltaTime);
        //charactercontroller.Move(moveVec * speed * Time.deltaTime);

        anim.SetBool("isRun", !(hAxis == 0 && vAxis == 0));

    }

    void Turn()
    {
        //#1. 키보드에 의한 회전
        transform.LookAt(transform.position + new Vector3(hAxis, 0, vAxis));

        //#2. 마우스에 의한 회전
        if (fDown && !isReload)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
    
    }

    void Jump()
    {

        if(jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 45, ForceMode.VelocityChange);
            isJump = true;
        }

        //if (jDown && charactercontroller.isGrounded == true)
        //{
        //    Debug.Log("점프");
        //    //rigid.AddForce(Vector3.up * 40, ForceMode.VelocityChange);
        //    moveVec.y = jumpForce;
        //    isJump = true;
        //}

        //if (isJump)
        //{
        //    moveVec.y += gravity * Time.deltaTime;
        //}

        
    } 
    
    void Grenade()
    {
        if(hasGrenades == 0)
        {
            return;
        }
        if(gDown && !isReload)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 2;

                GameObject instantGrenade = Instantiate(grenadeObj, transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(nextVec, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

                hasGrenades--;

            }
        }
    }

    void Attack()
    {

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.attackRate < fireDelay;

        if (fDown && isFireReady && !isReload )
        {

            equipWeapon.GetComponent<Weapon>().Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            fireDelay = 0;
        }
    }

    void Reload()
    {
        if (equipWeapon.type == Weapon.Type.Melee)
            return;

        if (isReload == true)
            return;

        if((rDown && isFireReady && !isReload && equipWeapon.curAmmo < equipWeapon.maxAmmo) || equipWeapon.curAmmo == 0)
        {

            anim.SetTrigger("doReload");
            isReload = true;

            StartCoroutine(("ReloadOut"));
        }
    }

    IEnumerator ReloadOut()
    {
        yield return new WaitForSeconds(2.7f);
        equipWeapon.curAmmo = equipWeapon.maxAmmo;
        isReload = false;
    }


    //캐릭터 회전 버그 해결
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    //벽뚫 버그 해결
    void StopToWall()
    {
        Debug.DrawRay(transform.position, moveVec * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, moveVec, 5, LayerMask.GetMask("Wall", "Obstacle"));
    }

    void CheckGround()
    {
        RaycastHit hit;
        bool cast =
            Physics.SphereCast(transform.position, 1.3f, Vector3.down, out hit, 0.5f, LayerMask.GetMask("Wall", "Obstacle"));

        isJump = cast;
    }
      

    void LifeDown()
    {
        lifes[life+1].gameObject.SetActive(false);
    }

    IEnumerator OnDamage(Vector3 vec)
    {

        gameObject.layer = 17;

        LifeDown();
        anim.SetTrigger("isAttacked");

        //if(isBossAtk)
        //    rigid.AddForce(transform.forward * -25, ForceMode.Impulse);
        //rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);

        rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
        rigid.AddForce(-saveVec * 15, ForceMode.Impulse);

        isDamage = true;
        yield return new WaitForSeconds(0.4f);
        isDamage = false;
        yield return new WaitForSeconds(1.1f);

        //if (isBossAtk)
        //    rigid.velocity = Vector3.zero;

        gameObject.layer = 9;
       
    }

    void PlayerPos()
    {
        
        count += Time.deltaTime;
        if (!isJump && this.transform.position.y > 0)
        {
            if(count >= savePoint)
            {
                playerPos = this.transform.position;
                count = 0f;
            }
        }
    }

    void Fall()
    {
        if(this.transform.position.y < -30)
        {
            life -= 1;
            this.transform.position = playerPos;
        }
    }

    //몬스터가 플레이어와 충돌시 일어나는 버그 해결
    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }

    //캐릭터와 다른 물체가 충돌
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Obstacle")
        {
            isJump = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Coin:
                    AudioManager.instance.Play("CoinGet");
                    coin += item.value;
                    if (coin > maxCoin)
                        coin = maxCoin;
                    break;
                case Item.Type.Heart:
                    AudioManager.instance.Play("Heal");
                    life += item.value;
                    if (life > maxHealth)
                        life = maxHealth;
                    break;
                case Item.Type.Grenade:
                    hasGrenades += item.value;
                    if (hasGrenades > maxHasGrenades)
                        hasGrenades = maxHasGrenades;
                    break;
            }
            Destroy(other.gameObject);
        }
        else if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                Vector3 vec = other.transform.position - this.transform.position;
                Bullet enemyBullet = other.GetComponent<Bullet>();
                life -= enemyBullet.damage;
                bool isBossAtk = other.name == "Boss Melee Area";
                StartCoroutine(OnDamage(vec));

                //게임 오버 구간
                if (life <= 0)
                {
                    gameoverUI.SetActive(true);
                    return;
                }

            }

            if (other.GetComponent<Rigidbody>() != null)
                Destroy(other.gameObject);
        }

    }

}
