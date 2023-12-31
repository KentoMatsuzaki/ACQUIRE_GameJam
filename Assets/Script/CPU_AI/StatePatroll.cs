using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// 自作ライブラリ
using SLib.StateSequencer;
using SLib;
using SLib.AI;

public class StatePatroll : IStateMachineState
{

    #region IsDebuggin
    bool _ISDEBUG_ = true;
    #endregion

    int _cIndex;    // パスのインデックス （現状）
    Transform _cTransform, _tTransform; // 現状の座標 、 目標の座標
    PathHolder _pathRoot;   // パスのルートコンポーネント
    List<Vector3> _pPath = new List<Vector3>(); //  ルートから得たパスの動的配列

    Rigidbody2D _rb2;
    SpriteRenderer _spriteRenderer;

    float _speed;

    public StatePatroll(Transform dTransform, PathHolder pathRoot, Rigidbody2D rb2, SpriteRenderer spriteRenderer, float speed)
    {
        _cTransform = dTransform;
        _pathRoot = pathRoot;
        _rb2 = rb2;
        _pPath = _pathRoot.GetPatrollingPath().ToList();
        _cIndex = 0;
        _spriteRenderer = spriteRenderer;
        _speed = speed;
    }

    public void FramesTask(Transform selfTransform)    // 毎フレーム呼び出される処理
    {
        UpdateTransform(selfTransform); // 座標更新
        AudioController.Instance.SePlay(AudioController.SeClass.SE.EnemyWalk1);
        AudioController.Instance.SePlay(AudioController.SeClass.SE.EnemyWalk2);
    }

    void UpdateTransform(Transform transform)
    {
        _cTransform = transform;
    }

    float DistanceSqwared(Vector2 p1, Vector2 p2)
    {
        float dX = p2.x - p1.x;
        float dY = p2.y - p1.y;
        float dd = (dX * dX) + (dY * dY);
        return dd;
    }

    public void Entry()
    {
        #region DB_Log
        if (_ISDEBUG_)
        {
            Debug.Log("StatePatroll=Tick");
        }
        #endregion
    }

    public void Exit()
    {

    }

    public void Tick()
    {
        // 向き計算
        Vector2 pTar, pSelf;
        pTar = _pPath[_cIndex];
        pSelf = _cTransform.position;
        Vector2 dir = (pTar - pSelf).normalized;
        _rb2.velocity = dir * _speed;

        _spriteRenderer.flipY = (dir.x < 0);

        // 目標地点到達かの判定
        float distance = DistanceSqwared(pTar, pSelf);
        if (distance < 1)
        {
            _cIndex = (_cIndex + 1 < _pPath.Count) ? _cIndex + 1 : 0;
        }

        // 目標への回転
        Vector3 rotDiff = (_pPath[_cIndex] - _cTransform.position);
        _cTransform.rotation = Quaternion.FromToRotation(Vector3.right, rotDiff);

        #region DB_Log
        if (_ISDEBUG_)
        {
            Debug.Log($"StatePatroll=Tick {dir.ToString()}");
        }
        #endregion
    }
}
