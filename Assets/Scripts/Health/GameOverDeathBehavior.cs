public class GameOverDeathBehavior : DeathBehavior
{
    public override void OnDeath() => GameStateManager.SetState(GameState.GameOver);
}
