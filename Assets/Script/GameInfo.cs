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
    //public GameManager GameManager;

    public GameInfo Set()
    {
        return GameInfo.Instance;
    }
}
