using UnityEngine;

namespace Extensions
{
    public static class CameraExtensions
    {
        private const float MinAspectRatio = 4f / 3f;
        private const float MaxAspectRatio = 23f / 9f;
        
        public static Vector3 AspectRatioPercentage(this Camera self, Vector3 min, Vector3 max)
        {
            var x = self.AspectRatioPercentage(min.x, max.x);
            var y = self.AspectRatioPercentage(min.y, max.y);
            var z = self.AspectRatioPercentage(min.z, max.z);
            
            return new Vector3(x, y, z);
        }
        
        public static float AspectRatioPercentage(this Camera self, float min, float max) {
            var percentage = self.AspectRatioPercentage();
            return Mathf.Lerp(min, max, percentage);
        }
    
        public static float AspectRatioPercentage(this Camera self) {
            var aspect = self.aspect;
            var limitedAspect = Mathf.Clamp(aspect, MinAspectRatio, MaxAspectRatio);

            var range = MaxAspectRatio - MinAspectRatio;
            return (limitedAspect - MinAspectRatio) / range;
        }
    }
}
