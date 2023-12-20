using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �h�A�̃��b�N�R���\�[���̃N���X�B </summary>
public class DoorLockingConsole : MonoBehaviour
{
    [SerializeField,
        Header("������A�N�e�B�u�ɂ��邽�ߕK�v�ȃu���[�J�[")]
    List<Breaker> _breakers = new List<Breaker>();

    [SerializeField]
    Animator _anim;

    int _activeCount;

    bool _isActive = false;

    public void NotifyActivated()
    {
        if (_activeCount + 1 <= _breakers.Count)
        {
            _activeCount++;
        }

        if (_activeCount == _breakers.Count && !_isActive)
        {
            _isActive = true;
            _anim?.Play("OpenDoor");
        }
    }
}
