using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic
{
    public partial class Actor : MonoBehaviour
    {
    }

    public partial class Actor : IDamageable
    {
        public void ApplyDamage(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
