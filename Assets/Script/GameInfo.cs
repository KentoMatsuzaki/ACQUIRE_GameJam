using UnityEngine;

public class GameInfo : MonoBehaviour
{
    static GameInfo _instance;
    public static GameInfo Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType<GameInfo>();
                if(!_instance)
                {
                    Debug.LogError("GameInfo�X�N���v�g���V�[����ɑ��݂��܂���");
                }
            }
            return _instance;
        }
    }

    //public Player Player;
    private GameManager _gameManager;
    public GameManager GameManager
    {
        get => _gameManager; set => _gameManager = value;
    }

    public GameInfo Set()
    {
        return GameInfo.Instance;
    }
}
