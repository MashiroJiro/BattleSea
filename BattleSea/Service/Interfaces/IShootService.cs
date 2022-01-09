using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSea.Model;

namespace BattleSea.Service.Interfaces
{
    interface IShootService
    {
        bool Shoot(Point point);
        bool IsAllShipDead();

    }
}
