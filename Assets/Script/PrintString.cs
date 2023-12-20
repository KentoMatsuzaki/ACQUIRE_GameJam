using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrintString : MonoBehaviour
{
    ///<summary>�\������e�L�X�g�̔z��</summary>
    [SerializeField] string[] texts = null;
    ///<summary>�\������e�L�X�g</summary>
    [SerializeField] Text _printText = null;
    ///<summary>�e�L�X�g�̕\������</summary>
    [SerializeField] float _printDuration = 0.5f;

    private string _currentText;
    private int _stringIndex;


    private void Awake()
    {
        GameInfo.Instance.Printer = this;
    }
    /// <summary>�e�L�X�g��\�����郁�\�b�h</summary>
    /// <param name="textnum">�\������e�L�X�g�̗v�f�ԍ�</param>
    IEnumerator PrintOneByOne(int textnum)
    {
        _printText.text = null;
        _currentText = texts[textnum];
        _stringIndex = _currentText.Length;

        for (int i = 0; i < _stringIndex; i++)
        {
            _printText.text += _currentText[i];
            yield return new WaitForSeconds(_printDuration);
        }

        _currentText = null;
    }

    public void CallPrintOneByOne(int textnum)
    {
        StartCoroutine(PrintOneByOne(textnum));
    }
}
