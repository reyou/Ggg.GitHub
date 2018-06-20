using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain.GroupingBlocks
{
    /// <summary>
    /// The JoinBlock<T1,T2> and JoinBlock<T1,T2,T3> classes collect input elements and propagate 
    /// out System.Tuple<T1,T2> or System.Tuple<T1,T2,T3> objects that contain those elements. 
    /// The JoinBlock<T1,T2> and JoinBlock<T1,T2,T3> classes do not inherit from ITargetBlock<TInput>. 
    /// Instead, they provide properties, Target1, Target2, and Target3, that implement ITargetBlock<TInput>.
    /// </summary>
    class JoinBlockExample
    {
        /// <summary>
        /// The following basic example demonstrates a case in which a JoinBlock<T1,T2,T3> 
        /// object requires multiple data to compute a value. This example creates a 
        /// JoinBlock<T1,T2,T3> object that requires two Int32 values and a Char value to 
        /// perform an arithmetic operation.
        /// </summary>
        public static void Run()
        {
            // Create a JoinBlock<int, int, char> object that requires
            // two numbers and an operator.
            /*Provides a dataflow block that joins across multiple dataflow sources,
             which are not necessarily of the same type, waiting for one item to arrive for each type before 
             they’re all released together as a tuple that contains one item per type*/
            JoinBlock<int, int, char> joinBlock = new JoinBlock<int, int, char>();

            // Post two values to each target of the join.
            joinBlock.Target1.Post(3);
            joinBlock.Target1.Post(6);

            joinBlock.Target2.Post(5);
            joinBlock.Target2.Post(4);

            joinBlock.Target3.Post('+');
            joinBlock.Target3.Post('-');

            // Receive each group of values and apply the operator part
            // to the number parts.
            for (int i = 0; i < 2; i++)
            {
                Tuple<int, int, char> data = joinBlock.Receive();
                switch (data.Item3)
                {
                    case '+':
                        Console.WriteLine("{0} + {1} = {2}", data.Item1, data.Item2, data.Item1 + data.Item2);
                        break;
                    case '-':
                        Console.WriteLine("{0} - {1} = {2}", data.Item1, data.Item2, data.Item1 - data.Item2);
                        break;
                    default:
                        Console.WriteLine("Unknown operator '{0}'.", data.Item3); break;
                }

            }

        }
    }
}
