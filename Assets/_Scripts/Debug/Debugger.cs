using UnityEngine;

namespace Debugger
{


    public class Debugger : MonoBehaviour
    {
        public static GameObject solidLine; //change height
        public static GameObject ArrowSprite; //change height

        public void DrawTangent(Vector3 _origin, Vector3 _tangentDir, float _tangentLength, float _baseLength, Color _baseColor, Color _tangentColor)
        {
            _tangentLength = _tangentLength > 0 ? _tangentLength : 2f;
            _baseLength = _baseLength > 0 ? _baseLength : 4f;
            
            
        }
    }
}