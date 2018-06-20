using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataFlowMain.ExecutionBlocks
{
    /// <summary>
    /// The TransformManyBlock<TInput,TOutput> class resembles the TransformBlock<TInput,TOutput> 
    /// class, except that TransformManyBlock<TInput,TOutput> produces zero or more output 
    /// values for each input value, instead of only one output value for each input value.
    /// </summary>
    class TransformManyBlockExample
    {
        /// <summary>
        /// The following basic example creates a TransformManyBlock<TInput,TOutput> object 
        /// that splits strings into their individual character sequences. The TransformManyBlock<TInput,TOutput> 
        /// object takes String values as input and produces Char values as output.
        /// </summary>
        /// <param name="type"></param>
        public static void Run(int type = 1)
        {
            // Create a TransformManyBlock<string, char> object that splits
            // a string into its individual characters.
            TransformManyBlock<string, char> transformManyBlock = new TransformManyBlock<string, char>(s =>
            {
                char[] charArray = s.ToCharArray();
                return charArray;
            });

            // Post two messages to the first block.
            transformManyBlock.Post("Hello");
            transformManyBlock.Post("World");
            // Receive all output values from the block.
            if (type == 0)
            {
                for (int i = 0; i < ("Hello" + "World").Length; i++)
                {
                    Console.WriteLine(transformManyBlock.Receive());
                }
            }
            else if (type == 1)
            {
                do
                {
                    if (transformManyBlock.InputCount == 0 && transformManyBlock.OutputCount == 0)
                    {
                        return;
                    }
                    char receive = transformManyBlock.Receive();
                    Console.WriteLine("transformManyBlock.Receive:");
                    Console.WriteLine(receive);
                    Console.WriteLine();
                } while (true);

            }


        }
    }
}
