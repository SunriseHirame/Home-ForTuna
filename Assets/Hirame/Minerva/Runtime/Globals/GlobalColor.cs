using JetBrains.Annotations;
using UnityEngine;

namespace Hirame.Minerva
{
    [CreateAssetMenu (menuName = "Hirame/Globals/Color")]
    public sealed class GlobalColor : GlobalValue<Color>
    {

        [UsedImplicitly]
        public void Multiply (float multiplier) => RuntimeValue *= multiplier;

        [UsedImplicitly]
        public void Multiply (Color color) => RuntimeValue *= color;
    }
}
