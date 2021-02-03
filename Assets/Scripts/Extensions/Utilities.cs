using System.Collections;
using UnityEngine;

namespace Extensions
{
    public static class Utilities
    {
        public static IEnumerator Wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
    }
}
