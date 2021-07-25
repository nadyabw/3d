using NaughtyAttributes;
using UnityEngine;

namespace Snowman
{
    public class Test : MonoBehaviour
    {
        public Transform first;
        public Transform second;

        [Button()]
        private void Log()
        {
            Debug.Log($"General forward <{Vector3.forward}>");
            Debug.Log($"First <{first.name}> position <{first.position}> localPosition <{first.localPosition}> " +
                $"forward <{first.forward}> parent <{first.parent == null}>");
            Debug.Log($"Second <{second.name}> position <{second.position}> localPosition <{second.localPosition}> " +
                $"forward <{second.forward}> parent <{second.parent}>");

            var pps = second.position;
            pps.y = 1.5f;
            // second.position = pps;
            second.localPosition = pps;
        }
    }
}
