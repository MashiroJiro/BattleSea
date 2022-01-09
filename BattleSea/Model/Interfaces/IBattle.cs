using System.Collections.Generic;

namespace BattleSea.Model.Interfaces
{
    interface IBattle
    {
        bool Shoot(Point point, IBattle enemy);
        Ship CreateShip(Point start);
        bool IsAllShipAdded();
        bool IsAllShipDead();
        List<Ship> GetShips();
    }
}
