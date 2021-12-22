using UnityEngine;
using UnityEngine.Events;

public class GameStates : MonoBehaviour
{
    //private GameStateEnum state = GameStateEnum.Introduction;
    //[SerializeField] public UnityEvent<string> state = new UnityEvent<string>();
    private string state;

    //public GameStates(GameStateEnum state)
    //{
    //    this.state = state;
    //}

    public string getGameStates()
    {
      return state;
    }

    //public void setGameState(GameStateEnum newState)
    //{
    //    state = newState;
    //}
    
    private void OnEnable()
    {
        BrianSays.brianSpeaking += gameState;
        ExerciseState.state += gameState;
    }

    private void OnDisable()
    {
        BrianSays.brianSpeaking -= gameState;
        ExerciseState.state -= gameState;
    }

    public void gameState(string newState)
    {
        //state.Invoke(newState);
        state = newState;
        Debug.Log(state);
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