using System;
using System.Collections.Generic;

namespace NewOverlord
{
    interface IPooledObject
    {
        ObjectPooler.ObjectInfo.ObjectType Type { get; }
    }
}
