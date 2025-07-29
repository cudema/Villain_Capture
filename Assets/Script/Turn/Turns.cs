using UnityEngine;

namespace Turns
{
    class EnemyTurnSetting : TurnBase<BattleManager>
    {
        public override void OnEnter()
        {
            //BattleManager.OnEnemyTrun?.Invoke();
        }
        public override void OnExit()
        {
        }
    }
    class EnemyTurn : TurnBase<BattleManager>
    {
        public override void OnEnter()
        {
        }
        public override void OnExit()
        {
        }
    }
    class PlayerTrun : TurnBase<BattleManager>
    {
        public override void OnEnter()
        {
        }
        public override void OnExit()
        {
        }
    }
}
