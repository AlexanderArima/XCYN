using System.Collections;

namespace XCYN.Common
{
    public class StackHelper
    {
        public static void Insert(Stack stack,string[] array)
        {
            stack.Clear();
            foreach (var item in array)
            {
                stack.Push(item);
            }
        }

        public static void Insert(Stack stack,string str)
        {
            stack.Clear();
            var array = str.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                stack.Push(array[i].ToString());
            }
        }
    }
}
