using System;
using UnityEngine;

namespace Konzit.GameSystem
{
    public interface IDataGetter
    {
        IDataRecord GetData(Type type);
    }
}
