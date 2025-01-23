using UnityEngine;

public class GameOverDeathBehavior : DeathBehavior
{
    public override void Die(GameObject owner) => GameStateManager.SetState(GameState.GameOver);
}
