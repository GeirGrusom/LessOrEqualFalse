using System;
using System.Reflection.Emit;
using Xunit;
using static Xunit.Assert;

namespace BoolEqual
{
    public class LessOrEqualTests
    {
        public bool Compare(int a, int b)
        {
            return a >= b;
        }

        [Fact]
        public void IlGen_LessOrEqual_ReturnsTrue()
        {
            var dyn = new DynamicMethod("LessOrEqual", typeof(bool), new [] { typeof(int), typeof(int)});
            var il = dyn.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);

            il.Emit(OpCodes.Cgt);
            il.Emit(OpCodes.Not);

            il.Emit(OpCodes.Ret);

            var fun = (Func<int, int, bool>)dyn.CreateDelegate(typeof(Func<int, int, bool>));

            Equal(true, fun(1, 1));
        }
    }
}
