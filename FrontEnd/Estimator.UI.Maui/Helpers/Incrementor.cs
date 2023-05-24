using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimator.UI.Maui.Helpers
{
    public static class Incrementor
    {
        private static int _current = 0;
        public static int Next()
        {
            _current++;
            return _current;
        }
    }
}
