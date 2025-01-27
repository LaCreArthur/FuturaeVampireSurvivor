public class GameOverDeathBehavior : DeathBehavior
{
    public override void Die() => GameStateManager.SetState(GameState.GameOver);
}
