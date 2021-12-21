using UnityEngine;
using UnityEngine.Events;

public class GameStates : MonoBehaviour
{
    //private GameStateEnum state = GameStateEnum.Introduction;
    [SerializeField] public UnityEvent<string> state = new UnityEvent<string>();

    //public GameStates(GameStateEnum state)
    //{
    //    this.state = state;
    //}

    //public GameStateEnum getGameStates()
    //{
    //    return state;
    //}

    //public void setGameState(GameStateEnum newState)
    //{
    //    state = newState;
    //}

    public void gameState(string newState)
    {
        state.Invoke(newState);
    }
}

/*public enum GameStateEnum
{
    Introduction,
    MiniGameOne,
    MiniGameTwo,
    MiniGameThree,
    Breathing
}

*/