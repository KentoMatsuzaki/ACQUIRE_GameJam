using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���색�C�u����
using SLib.StateSequencer;
using SLib.AI;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary> �GAI�̋@�\��񋟂��� </summary>
public class EnemyCPU : MonoBehaviour
{
    // ���C���[�}�X�N�� �v���C���[�̌��m Physics.CheckSphere() ���g��
    [SerializeField,
        Header("�p�g���[���̓��؂̃I�u�W�F�N�g���A�^�b�`")]
    PathHolder _pRoot;
    [SerializeField,
        Header("�ړ����x")]
    float _mSpeed = 3;
    [SerializeField,
        Header("�v���C���[�̃��C���[")]
    LayerMask _pLayer;
    [SerializeField,
        Header("�v���C���[")]
    GameObject _player;

    StateSequencer _sSeqencer = new StateSequencer();   // �X�e�[�g�}�V��
    StatePatroll _patroll;  //  �p�g���[���X�e�[�g
    StateDetectPlayer _detect;      // �m�ۃX�e�[�g

    Rigidbody2D _rb2d;
    SpriteRenderer _sRenderer;

    bool _isDetectedPlayer;

    #region TransitionNames
    const string PatrollToDetect = "DetectP";
    #endregion

    void Hoge(string s) { }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sRenderer = GetComponent<SpriteRenderer>();

        _patroll = new StatePatroll(transform, _pRoot, _rb2d, _sRenderer, _mSpeed);
        _detect = new StateDetectPlayer(_rb2d);

        // �X�e�[�g�o�^
        _sSeqencer.ResistStates(new List<IStateMachineState>() { _patroll, _detect });

        // �J�ړo�^
        _sSeqencer.MakeTransition(_patroll, _detect, PatrollToDetect);

        // �_�~�[�̓o�^ �iNull�Q�Ƃ�h���j
        _sSeqencer.OnEntered += Hoge;
        _sSeqencer.OnUpdated += Hoge;
        _sSeqencer.OnExited += Hoge;

        // �N��
        _sSeqencer.InitStateMachine();
    }

    private void FixedUpdate()
    {
        // �J�ڏ����]��

        // �e�X�e�[�g�e���̍X�V����
        _patroll.FramesTask(transform);

        // �X�e�[�g�V�[�P���T�[�̊e�J�ڂ̕]���ƑJ�ڍX�V
        _sSeqencer.UpdateTransition(PatrollToDetect, ref _isDetectedPlayer, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _player.layer)
        {
            _isDetectedPlayer = true;
            GameInfo.Instance.GameManager.SceneChange(GameManager.SceneState.GameOver);
            //collision.gameObject.SetActive(false);
        }
    }
}
