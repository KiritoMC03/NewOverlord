using System;
using System.Collections;

namespace NewOverlord
{
    class Utils
    {
        public static float BetweenOrEnd(float value, int startInclusive, int endExclusive)
        {
            if(value < startInclusive || value > endExclusive)
            {
                return value;
            }
            return endExclusive;
        }

        public static float OutsideOrEnd(float value, int startInclusive, int endExclusive)
        {
            if (value > startInclusive && value < endExclusive)
            {
                return value;
            }
            return endExclusive;
        }

        public static float BetweenOrStart(float value, int startInclusive, int endExclusive)
        {
            if (value < startInclusive || value > endExclusive)
            {
                return value;
            }
            return startInclusive;
        }

        public static float OutsideOrStart(float value, int startInclusive, int endExclusive)
        {
            if (value > startInclusive && value < endExclusive)
            {
                return value;
            }
            return startInclusive;
        }
    }
}