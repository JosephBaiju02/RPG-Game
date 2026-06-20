using UnityEngine;
using System.Collections;
using System;

public class Entity : MonoBehaviour
{
    public event Action OnFlipped;



    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    protected StateMachine stateMachine;








    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;
    

    [Header("Collision Detection")]
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }
    [SerializeField] private Transform groundCheck;


    private bool isKnocked;
    private Coroutine knockBackCo;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        
    }

   
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float Yvelocity)
    {
        if (isKnocked)
            return;
        rb.linearVelocity = new Vector2(xVelocity, Yvelocity);
        HandleFlip(xVelocity);
    }
    public void ReciveKnockBack(Vector2 knockBack,float duration)
    {
        if (knockBackCo != null)
            StopCoroutine(knockBackCo);
        StartCoroutine(KnockBackCo(knockBack, duration));
    }
    private IEnumerator KnockBackCo(Vector2 knockBack,float duration )
    {
        isKnocked = true;
        rb.linearVelocity = knockBack;
        yield return new WaitForSeconds(duration);
        rb.linearVelocity = Vector2.zero;
        isKnocked = false;
    }
    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight) Flip();
        else if (xVelocity < 0 && facingRight) Flip();
    }
    public void Flip()
    {

        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir *= -1;
        OnFlipped?.Invoke();
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        if (secondaryWallCheck != null)
        {
        wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer) &&
            Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);

        }
        else
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);

        }
    }

    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public virtual void EntityDeath()
    {

    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        if(secondaryWallCheck!=null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));

    }
}
