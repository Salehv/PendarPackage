using Pender.ColorSettings;
using UnityEngine;

namespace Pender
{
    public class Pendar
    {
        private static Pendar _;
        public static PendarColorSettings ColorSettings;
        
        static Pendar()
        {
            ColorSettings = Resources.Load<PendarColorSettings>("Pendar/PendarColorSettings");
        }
    }
}
