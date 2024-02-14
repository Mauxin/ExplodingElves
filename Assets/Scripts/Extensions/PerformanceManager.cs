using UnityEngine;
using UnityEngine.Profiling;

namespace Extensions
{
    public static class PerformanceManager
    {
        private const int MB_CONVERTER = 1024 * 1024;
        
        public static int GetFPS()
        {
            return (int)(1f / Time.unscaledDeltaTime);
        }
        
        public static bool IsLowFPS()
        {
            return GetFPS() <= 30 && GetFPS() != 50;
        }
        
        public static long GetTotalMemory()
        {
            return Profiler.GetMonoHeapSizeLong() / MB_CONVERTER;
        }
        
        public static long GetAllocatedMemory()
        {
            return Profiler.GetMonoUsedSizeLong() / MB_CONVERTER;;
        }
        
        public static long GetGraphicsMemory()
        {
            return Profiler.GetTotalReservedMemoryLong() / MB_CONVERTER;;
        }
        
    }
}