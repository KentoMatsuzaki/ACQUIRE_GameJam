using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// ���색�C�u����
using SLib.StateSequencer;
using SLib;
using SLib.AI;

public class StatePatroll : IState
{

    #region IsDebuggin
    bool _ISDEBUG_ = true;
    #endregion

    int _cIndex;    // �p�X�̃C���f�b�N�X �i����j
    Transform _cTransform, _tTransform; // ����̍��W �A �ڕW�̍��W
    PathHolder _pathRoot;   // �p�X�̃��[�g�R���|�[�l���g
    List<Vector3> _pPath = new List<Vector3>(); //  ���[�g���瓾���p�X�̓��I�z��

    Rigidbody2D _rb2;
    float _speed;

    public StatePatroll(Transform dTransform, PathHolder pathRoot, Rigidbody2D rb2, float speed)
    {
        _cTransform = dTransform;
        _pathRoot = pathRoot;
        _rb2 = rb2;
        _pPath = _pathRoot.GetPatrollingPath().ToList();
        _cIndex = 0;
        _speed = speed;
    }

    public void FramesTask(Transform selfTransform)    // ���t���[���Ăяo����鏈��
    {
        UpdateTransform(selfTransform); // ���W�X�V
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
        Vector2 pTar, pSelf;
        pTar = _pPath[_cIndex];
        pSelf = _cTransform.position;
        Vector2 dir = (pTar - pSelf).normalized;
        _rb2.velocity = dir * _speed;

        float distance = DistanceSqwared(pTar, pSelf);

        Vector3 rotDiff = (_pPath[_cIndex] - _cTransform.position);
        _cTransform.rotation = Quaternion.FromToRotation(Vector3.right, rotDiff);

        if (distance < 1)
        {
            _cIndex = (_cIndex + 1 < _pPath.Count) ? _cIndex + 1 : 0;
        }

        #region DB_Log
        if (_ISDEBUG_)
        {
            Debug.Log($"StatePatroll=Tick {dir.ToString()}");
        }
        #endregion
    }
}
