using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject PlayerPrefab;
    public GameObject[] levels;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject panelGameOver;


    public Text txtScore;
    public Text txtLife;
    public Text txtLevel;
    public Text txtHightScore;

    public enum State { MENU, INIT, PLAY, LOAD_LEVEL, GAME_OVER, LEVEL_COMPLETED };
    State _state;

    GameObject _currentBall;
    GameObject _currentPlayer;
    GameObject _currentLevel;

    private int _hightScore;
    private int _score;
    private int _life;
    private int _level;
    bool _isStateSwitch = false;

    public static GameManager Instance { get; private set; }

    public int Score
    {
        get { return _score; }
        set 
        { 
            _score = value;
            txtScore.text = "SCORE : " + _score;
        }
    }

    public int Life
    {
        get { return _life; }
        set 
        {
            _life = value;
            txtLife.text = "LIFE : " + _life;
        }
    }

    public int Level
    {
        get { return _level; }
        set 
        {
            _level = value;
            txtLevel.text = "LEVEL : " + (_level + 1);
        }
    }

    public void ClickPlay() 
    {
        SwitchState(State.INIT);
    }

    void Start()
    {
        _hightScore = 0;
        _isStateSwitch = false;
        Instance = this;
        SwitchState(State.MENU);
    }


    public void SwitchState(State state, float delay = 0f)
    {
        StartCoroutine(SwitchDelay(state, delay));
    }

    IEnumerator SwitchDelay(State state, float delay)
    {
        _isStateSwitch = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = state;
        BeginState(state);
        _isStateSwitch = false;
    }

    void BeginState(State state)
    {
        switch (state)
        {
            case State.MENU:
                Cursor.visible = true;
                panelMenu.SetActive(true);
                txtHightScore.text = "HIGH SCORE: " + _hightScore;
                break;
            case State.INIT:
                Cursor.visible = false;
                panelPlay.SetActive(true);
                Score = 0;
                Life = 3;
                Level = 0;
                _currentPlayer = Instantiate(PlayerPrefab);
                _currentPlayer.name = "Player";
                SwitchState(State.LOAD_LEVEL);
                break;
            case State.PLAY:
                break;
            case State.LOAD_LEVEL:
                if (Level >= levels.Length)
                {
                    SwitchState(State.GAME_OVER);
                }
                else
                {
                    _currentPlayer = Instantiate(PlayerPrefab);
                    panelPlay.SetActive(true);
                    _currentLevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }
                break;
            case State.LEVEL_COMPLETED:
                Destroy(_currentBall);
                Destroy(_currentLevel);
                Destroy(_currentPlayer);
                Level++;
                panelLevelCompleted.SetActive(true);
                SwitchState(State.LOAD_LEVEL, 1f);
                break;
            case State.GAME_OVER:
                if(_currentBall != null) Destroy(_currentBall);
                if (_currentLevel != null) Destroy(_currentLevel);
                if (_currentPlayer != null) Destroy(_currentPlayer);
                panelGameOver.SetActive(true);
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(_currentBall == null)
                {
                    if(Life > 0)
                    {
                        _currentBall = Instantiate(BallPrefab);
                    }
                    else
                    {
                        SwitchState(State.GAME_OVER);
                    }
                }
                if(_currentLevel != null && _currentLevel.transform.childCount == 0 && _isStateSwitch == false) 
                {
                    SwitchState(State.LEVEL_COMPLETED);
                }
                break;
            case State.LOAD_LEVEL:
                break;
            case State.LEVEL_COMPLETED:
                break;
            case State.GAME_OVER:
                if(Input.anyKey) 
                {
                    if(_hightScore < Score) 
                    {
                        _hightScore = Score;
                    }
                    SwitchState(State.MENU);
                }
                break;
        }
    }

    void EndState()
    {
        switch(_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LOAD_LEVEL:
                break;
            case State.LEVEL_COMPLETED:
                panelPlay.SetActive(false);
                panelLevelCompleted.SetActive(false);
                break;
            case State.GAME_OVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;
        }
    }
}
