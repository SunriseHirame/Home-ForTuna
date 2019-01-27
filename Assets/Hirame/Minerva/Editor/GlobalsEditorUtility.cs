using UnityEngine;

namespace Hirame.Minerva.Editor
{
    public static class GlobalsEditorUtility
    {
        [RuntimeInitializeOnLoadMethod]
        private static void ResetOnLoad ()
        {
            foreach (var global in Resources.LoadAll<GlobalValueBase> (string.Empty))
            {
                global.Reset ();
            }
        }
    }
}
