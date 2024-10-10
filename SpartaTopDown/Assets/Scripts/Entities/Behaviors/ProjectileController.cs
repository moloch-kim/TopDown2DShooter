using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    private bool isReady;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private RangedAttackSO attackData;
    private float currentDuration;
    private Vector2 direction;

    private bool fxOnDestroy = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }
        currentDuration += Time.deltaTime;

        if(currentDuration > attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }
        rigidbody.velocity = direction * attackData.speed;
    }

    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        UpdateProjectilSprite();
        trailRenderer.Clear();
        currentDuration = 0;
        spriteRenderer.color = attackData.projectileColor;

        transform.right = this.direction;

        isReady = true;
    }


    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // TODO : ParticleSystem에 대해 배우고, 무기 NameTag에 해당하는 Fx 가져오기
        }
        gameObject.SetActive(false);
    }
    private void UpdateProjectilSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestroyProjectile(destroyPosition, fxOnDestroy);
            
        }
        else if(IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            // TODO : 데미지 준다
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}

