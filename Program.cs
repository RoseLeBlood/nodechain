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

            chain.Add(new SHA512SiblingBlockEntry<Int32>(45, 
                "35545195c7e19e54bef151b0c997891584c94f11387276a7837026333bdf1a6e46777f74b8518b01eb468aad1a9a0afc1d6d947f7e832fe8401f058778fc6472") );

            Console.WriteLine("Hello World!");
        }
    }


    


}
