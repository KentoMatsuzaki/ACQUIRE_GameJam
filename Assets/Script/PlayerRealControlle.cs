using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
/// <summary>Player�̖{�̂̃R���g���[���[</summary>
public class PlayerRealControlle : MonoBehaviour, IState
{
    [SerializeField]
    [Header("���s���x")]
    float _moveSpeed;
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    Animator _anim = null;
    /// <summary>�ړ�����Y</summary>
    float _moveDirY = 0;
    /// <summary>�ړ�����X</summary>
    float _moveDirX = 0;
    /// <summary>���A�ɉB��Ă��邩�ǂ���</summary>
    bool _isHide = false;

    public bool IsHide => _isHide;
    public void OnStart()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnEnter()
    {
        _spriteRenderer.sortingOrder = 1;
    }
    public void OnUpdate()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        _moveDirY = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HadeAction();
        }
    }
    public void OnFixedUpdate()
    {
        if(!_isHide)
        {
            if (_moveDirX == 0 || _moveDirY == 0)
            {
                _rb.velocity = new Vector2(_moveDirX * _moveSpeed, _moveDirY * _moveSpeed);
            }
        }
    }

    public void OnExit()
    {
        _spriteRenderer.sortingOrder = 0;
        _rb.velocity = Vector2.zero;
    }
    public void HadeAction()
    {
        _isHide = !_isHide;
        _rb.velocity = Vector2.zero;
    }

}

    
