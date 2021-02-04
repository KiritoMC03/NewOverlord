using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewOverlord
{
    interface INavMeshAgent
    {
        void FindNextPosition();
        void GoNext();
    }
}