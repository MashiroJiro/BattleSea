using BattleSea.Model;
using BattleSea.Model.Interfaces;
using BattleSea.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleSea.Service
{
    class ShootService : IShootService
    {
        private IBattle battle;
        private IBattle enemy;

        public ShootService(IBattle battle, IBattle enemy)
        {
            this.battle = battle;
            this.enemy = enemy;
        }

        public bool Shoot(Point point)
        {
            return battle.Shoot(point, enemy);
        }

        public bool IsAllShipDead()
        {
            return enemy.IsAllShipDead();
        }
    }
}
