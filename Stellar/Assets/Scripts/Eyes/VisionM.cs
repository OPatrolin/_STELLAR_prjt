using UnityEngine;
using static itemS0;

public class VisionM : MonoBehaviour
{
   
        public static VisionM Instance;
        public Material visionMaterial; 

        private void Awake()
        {
            Instance = this;
        }

        public void ApplyVision(VisionType type)
        {
            if (type == VisionType.Normal)
                visionMaterial.SetFloat("_ShiftEnabled", 0);
            else
                visionMaterial.SetFloat("_ShiftEnabled", 1);
        }
    
}
