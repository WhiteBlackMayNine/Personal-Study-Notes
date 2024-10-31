using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("监听事件")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO backToMenuEvent;

    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private Character character;

    public Vector2 inputDirection;
    [Header("基本参数")]
    public float speed;
    private float runSpeed;
    private float walkSpeed => speed / 2.5f;
    public float jumpForce;
    public float wallJumpForce;
    public float hurtForce;
    public float slideDistance;
    public float slideSpeed;
    public int slidePowerCost;
    private Vector2 originalOffset;
    private Vector2 originalSize;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("状态")]
    public bool isCrouch;
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool wallJump;
    public bool isSlide;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        character = GetComponent<Character>();

        originalOffset = coll.offset;
        originalSize = coll.size;

        inputControl = new PlayerInputControl();

        //跳跃
        inputControl.Gameplay.Jump.started += Jump;

        #region 强制走路
        runSpeed = speed;
        inputControl.Gameplay.WalkButton.performed += ctx =>
        {
            if (physicsCheck.isGround)
                speed = walkSpeed;
        };

        inputControl.Gameplay.WalkButton.canceled += ctx =>
        {
            if (physicsCheck.isGround)
                speed = runSpeed;
        };
        #endregion

        //攻击
        inputControl.Gameplay.Attack.started += PlayerAttack;

        //滑铲
        inputControl.Gameplay.Slide.started += Slide;
        inputControl.Enable();
    }

    private void OnEnable()
    {
        sceneLoadEvent.LoadRequestEvent += OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
        loadDataEvent.OnEventRaised += OnLoadDataEvent;
        backToMenuEvent.OnEventRaised += OnLoadDataEvent;
    }

    private void OnDisable()
    {
        inputControl.Disable();
        sceneLoadEvent.LoadRequestEvent -= OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
        loadDataEvent.OnEventRaised -= OnLoadDataEvent;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvent;
    }


    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

        CheckState();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
            Move();
    }

    //测试
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log(other.name);
    // }

    //场景加载过程停止控制
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.Gameplay.Disable();
    }

    //读取游戏进度
    private void OnLoadDataEvent()
    {
        isDead = false;
    }

    //加载结束之后启动控制
    private void OnAfterSceneLoadedEvent()
    {
        inputControl.Gameplay.Enable();
    }

    public void Move()
    {
        if (isSlide) return;
        //人物移动
        if (!isCrouch && !wallJump)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);

        //下蹲
        isCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;
        if (isCrouch)
        {
            //修改碰撞体大小和位移
            coll.offset = new Vector2(-0.05f, 0.85f);
            coll.size = new Vector2(0.7f, 1.7f);
        }
        else
        {
            //还原之前碰撞体参数
            coll.size = originalSize;
            coll.offset = originalOffset;
        }
    }


    private void Jump(InputAction.CallbackContext obj)
    {
        // Debug.Log("JUMP");
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            //打断滑铲协程
            isSlide = false;
            StopAllCoroutines();
        }
        else if (physicsCheck.onWall && !wallJump)
        {
            rb.AddForce(new Vector2(-inputDirection.x, 2.5f) * wallJumpForce, ForceMode2D.Impulse);
            wallJump = true;
        }
    }


    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            playerAnimation.PlayAttack();
            isAttack = true;
        }
    }


    private void Slide(InputAction.CallbackContext obj)
    {
        if (!isSlide && physicsCheck.isGround && character.currentPower >= slidePowerCost)
        {
            isSlide = true;

            var targetPos = new Vector3(transform.position.x + slideDistance * transform.localScale.x, transform.position.y);

            gameObject.layer = LayerMask.NameToLayer("Enemy");
            StartCoroutine(TriggerSlide(targetPos));

            character.OnSlide(slidePowerCost);
        }
    }

    private IEnumerator TriggerSlide(Vector3 target)
    {
        do
        {
            yield return null;
            if (!physicsCheck.isGround)
                break;

            //滑铲过程中撞墙
            if (physicsCheck.touchLeftWall && transform.localScale.x < 0f || physicsCheck.touchRightWall && transform.localScale.x > 0f)
            {
                isSlide = false;
                break;
            }

            rb.MovePosition(new Vector2(transform.position.x + transform.localScale.x * slideSpeed, transform.position.y));
        } while (MathF.Abs(target.x - transform.position.x) > 0.1f);

        isSlide = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }


    #region UnityEvent
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
    #endregion

    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;

        if (physicsCheck.onWall)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2f);
        else
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);


        if (wallJump && rb.velocity.y < 0f)
        {
            wallJump = false;
        }

        //避免死亡后敌人继续攻击player
        if (isDead)
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
