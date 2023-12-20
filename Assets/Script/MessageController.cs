using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    /// <summary>���b�Z�[�W�Ăяo���p�̃C���f�b�N�X</summary>
    [SerializeField] List<int> _indexList;
    /// <summary></summary>
    private Action<List<int>> _messageAction;
    void Start() => _messageAction = GameInfo.Instance.GameManager.ShowMessage;

    public void CallShowMessage() => _messageAction(_indexList);
}
