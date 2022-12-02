public interface IPlayerCombat
{
    void EnemyHitPlayer(int enmyDmg, int plyrDef, float pLife);

}

public interface IEnemyCombat
{
    void PlayerHitEnemy(int wpnDmg, int plyrDmg, int enemyDef, float cRate, float eLife);

}

public interface IShowDamage
{
    void ShowText(string txt, UnityEngine.Color txtColor, float damage);
}


