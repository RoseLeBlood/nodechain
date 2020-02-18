using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ASF.Node;
using ASF.Node.Block;

namespace nodechain {
    public class Program {
        static Guid ProgramGuid { get; set; } = new Guid ("9eccdf50-edc0-474a-bab6-2424c71a4f4e");
        static Guid NewOwnerGuid { get; } = new Guid ("35090297-22b8-44ed-b4ab-4c09f84e9b64");

        static void Main (string[] args) {
            Int32SiblingBlockChain chain = new Int32SiblingBlockChain (43,
                "55BF63AB1141E9AE52EBDB9A386D19DABB1C8F142426315332548C55AF71496686465DC4248EA595BAD996A0AA09B06226F9CD2E85E30A3B2F7339D2B680C540",
                ProgramGuid);

            chain.Transfer ("55BF63AB1141E9AE52EBDB9A386D19DABB1C8F142426315332548C55AF71496686465DC4248EA595BAD996A0AA09B06226F9CD2E85E30A3B2F7339D2B680C540",
                ProgramGuid, NewOwnerGuid);

            for (int i = 0; i < 4; i++) {
                chain.Add (new SHA512SiblingBlockEntry<Int32> (i, ProgramGuid));
            }
            Console.WriteLine("{");
            Console.WriteLine ("\"TestChain\": [\n {0}\n]", chain.ToString ());
            Console.WriteLine("}");
        }
    }
}