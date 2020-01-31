using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.RepairTheme
{
    public delegate void RepairObjectDestroyedHandler();
    public interface IRepairs
    {

        void DestorySelf();

        event RepairObjectDestroyedHandler RepairDestroyed;
    }
}