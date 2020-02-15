using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ASF.Node;
using ASF.Node.Block;

namespace nodechain {
    public class Program {
        static Guid ProgramGuid { get; set; } = new Guid ("9eccdf50-edc0-474a-bab6-2424c71a4f4e");
        static String JsonText;

        static void Main (string[] args) {

            String FileName = String.Format("{0}.json", ProgramGuid);

            Int32SiblingBlockChain chain = new Int32SiblingBlockChain (43,
                "b3c903359afb3c480ccbb9e0f5b5652d0ba3c5837ed2f048af7f03fcac0a9d0817c83903b7be82f4da28e26409ac85c67a55d62a8d3c7daa4da36492f7cfe553",
                ProgramGuid);

            for (int i = 0; i < 10; i++)
                chain.Add (new SHA512SiblingBlockEntry<Int32> (i, ProgramGuid));


            JsonText = "{\n" + String.Format ("\"{0}\":\n[", ProgramGuid);
            chain.Travers(TraversOrder.ListOrder, ChainTraversToJson, chain.Root);
            JsonText += "]\n}";

            System.IO.File.WriteAllText(FileName , JsonText);
            Console.WriteLine("Chain written to \"{0}\".", FileName);
        }
        public static void ChainTraversToJson(GenericBlockChain<int, SHA512SiblingBlockEntry<int>> root) {
            var option = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            JsonText += String.Format ("{0}\n", JsonSerializer.Serialize  (root.Data, option));
            if(root.Next != null) JsonText += ",";
        }
    }
}