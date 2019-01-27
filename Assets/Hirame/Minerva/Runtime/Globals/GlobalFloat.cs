using JetBrains.Annotations;
using UnityEngine;

namespace Hirame.Minerva
{
    [CreateAssetMenu (menuName = "Hirame/Globals/Float")]
    public sealed class GlobalFloat : GlobalValue<float>
    {
        [UsedImplicitly]
        public void Increment () => RuntimeValue++;

        [UsedImplicitly]
        public void Decrement () => RuntimeValue--;

        [UsedImplicitly]
        public void Add (float value) => RuntimeValue += value;

        [UsedImplicitly]
        public void Subtract (float value) => RuntimeValue -= value;

        [UsedImplicitly]
        public void Multiply (float multiplier) => RuntimeValue *= multiplier;

        [UsedImplicitly]
        public void Divide (float divider) => RuntimeValue /= divider;


        [UsedImplicitly]
        public void Add (int value) => RuntimeValue += value;

        [UsedImplicitly]
        public void Subtract (int value) => RuntimeValue -= value;

        [UsedImplicitly]
        public void Multiply (int multiplier) => RuntimeValue *= multiplier;

        [UsedImplicitly]
        public void Divide (int divider) => RuntimeValue /= divider;

        
        [UsedImplicitly]
        public void Add (GlobalFloat gFloat) => RuntimeValue += gFloat.RuntimeValue;

        [UsedImplicitly]
        public void Subtract (GlobalFloat gFloat) => RuntimeValue += gFloat.RuntimeValue;

        [UsedImplicitly]
        public void Multiply (GlobalFloat gFloat) => RuntimeValue *= gFloat.RuntimeValue;

        [UsedImplicitly]
        public void Divide (GlobalFloat gFloat) => RuntimeValue /= gFloat.RuntimeValue;
        

        [UsedImplicitly]
        public void Add (GlobalInt gInt) => RuntimeValue += gInt.RuntimeValue;

        [UsedImplicitly]
        public void Subtract (GlobalInt gInt) => RuntimeValue += gInt.RuntimeValue;
        
        [UsedImplicitly]
        public void Multiply (GlobalInt gInt) => RuntimeValue *= gInt.RuntimeValue;

        [UsedImplicitly]
        public void Divide (GlobalInt gInt) => RuntimeValue /= gInt.RuntimeValue;
    }
}
