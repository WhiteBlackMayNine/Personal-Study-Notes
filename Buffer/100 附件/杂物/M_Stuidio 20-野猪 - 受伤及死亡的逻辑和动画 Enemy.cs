
using System.Collections;
using AnimatorParameters;
using General;
using Manage.TimeManage;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        protected Rigidbody2D Rb2d;
        protected Animator Animator;
        protected PhysicsCheck PhysicsCheck;
        public Transform attacker;
        
        [Header("基本参数")]
        public float normalSpeed;
        public float chaseSpeed;
        public float currentSpeed;
        public Vector3 faceDir;
        public float hurtForce;

        [Header("状态")] 
        public bool isHurt;
        public bool isDead;

        

        private bool IsWallScale =>
            (PhysicsCheck.touchLeftWall && faceDir.x < 0) || (PhysicsCheck.touchRightWall && faceDir.x > 0);
        

        private void Awake()
        {
            Rb2d = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            PhysicsCheck = GetComponent<PhysicsCheck>();
            currentSpeed = normalSpeed;
        }

        private void Update()
        {
            faceDir = new Vector3(-transform.localScale.x, 0, 0);

            if (IsWallScale)
            {
                Animator.SetBool(AnimationID.BoarWalkID, false);
                GameTimeManage.MainInstance.TryGetOneTime(2,WallScale);
            }
        }
        

        private void FixedUpdate()
        {
            Move();
        }

        private void WallScale()
        {
            if (IsWallScale)
            {
                transform.localScale = new Vector3(faceDir.x,1,1);
            }
        }
        
        protected virtual void Move()
        {
            if(isHurt || isDead) return;
            Rb2d.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, Rb2d.velocity.y);
        }

        public void OnTakeDamage(Transform attackTrans)
        {
            attacker = attackTrans;
            if (attackTrans.transform.position.x - transform.position.x > 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            
            if (attackTrans.transform.position.x - transform.position.x < 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }
            isHurt = true;
            
            Animator.SetTrigger(AnimationID.BoarHurtID);
            
            var dir = new Vector2(transform.position.x - attackTrans.position.x,0).normalized;
            
            StartCoroutine(OnHurt(dir));//开始协程
        }

        private IEnumerator OnHurt(Vector2 dir)
        {
            //等待0.45S后再去执行下面的逻辑（时间按照自己需要去改就行）
            //如果不希望去等待那么就写 null
            //发现 dir 无法访问（毕竟是个局域变量）
            //所以我们把这个变量当成一个参数去传递
            //执行的时候把 dir 这个参数传递进来就好了
            Rb2d.AddForce(dir * hurtForce , ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.6f);
            isHurt = false;
        }

        public void OnDeath()
        {
            gameObject.layer = 2;
            Animator.SetBool(AnimationID.BoarDeadID,true);
            isDead = true;
        }
        
        public void DestroyAfterAnimation()
        {
            Destroy(this.gameObject);
        }
        
    }
}