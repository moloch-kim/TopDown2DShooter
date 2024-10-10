using System;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    // 실제로 이동이 일어날 컴포넌트
    
    private TopDownController controller;
    private CharacterStatHandler characterStatHandler;
    private Rigidbody2D movementRigidbody;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake() // 주로 내 컴포넌트 안에서 끝나는 것
    {
        // controller랑 TopdownMovement랑 같은 게임오브젝트 안에 있다는 가정
        controller = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void FixedUpdate()
    {
        // FixedUpdate는 물리 업데이트
        // rigidbody의 값을 바꾸니까 FixedUpdate
        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatHandler.CurrentStat.speed;
        movementRigidbody.velocity = direction;
    }
}