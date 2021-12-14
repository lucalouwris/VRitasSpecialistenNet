using UnityEngine;

public class GameStates : MonoBehaviour
{
    private GameStateEnum state = GameStateEnum.Introduction;

    public GameStates(GameStateEnum state)
    {
        this.state = state;
    }

    public GameStateEnum getGameStates()
    {
        return state;
    }

    public void setGameState(GameStateEnum newState)
    {
        state = newState;
    }
}

public enum GameStateEnum
{
    Introduction,
    MiniGameOne,
    MiniGameTwo,
    MiniGameThree,
    Breathing
}

