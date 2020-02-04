using System;
using ASF.Node.Block;

namespace nodechain
{
    public class Program
    {
        static void Main(string[] args)
        {
            Int32SiblingBlockChain chain = new Int32SiblingBlockChain(43, 
                "b3c903359afb3c480ccbb9e0f5b5652d0ba3c5837ed2f048af7f03fcac0a9d0817c83903b7be82f4da28e26409ac85c67a55d62a8d3c7daa4da36492f7cfe553");

            for(int i = 0; i < 10; i++)
                chain.Add(new SHA512SiblingBlockEntry<Int32>(i));

            Console.WriteLine("Test [\n{0}\n]", chain.ToString());
        }
    }
}
