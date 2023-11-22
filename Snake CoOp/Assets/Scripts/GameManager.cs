using UnityEngine;
using UnityEngine.SceneManagement;

public enum State
{
    NONE,
    ALIVE,
    DEAD
}

public class GameManager : MonoBehaviour
{
    #region --------- Public Variables ---------
    public static GameManager Instance = null;
    #endregion ------------------

    #region --------- Private Variables ---------
    private State state = State.NONE;
    #endregion ------------------

    #region --------- Monobehavior Methods ---------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    #endregion ------------------

    #region --------- Public Methods ---------
    public void SetState(State state)
    {
        this.state = state;
    }

    public State GetState()
    {
        return state;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    #endregion ------------------


}
