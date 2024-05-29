using UnityEngine;


// 싱글톤
public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance // 외부 접근
    {
        // 씬이 시작 됐을 때 가져오기
        get // _instance 반환, 방어 코드
        {
            // 비어 있으면
            if (_instance == null)
            {
                // 게임오브젝트 생성 및 스크립트 붙여주기
                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    public Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else // _instance가 null이 아니고
        {
            if (_instance == this) // 기존의 _instance가 지금 넣으려는 나와 같다면
            {
                Destroy(gameObject); // 현재 것을 파괴
            }
        }
    }
}
