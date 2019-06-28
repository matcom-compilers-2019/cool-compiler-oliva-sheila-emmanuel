using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AST;
using Logic;
using System.IO;
using CIL;

namespace Compiler
{
    class Program
    {
        public static void Show_AST(Node node, int level = 0)
        {
            if (node == null) return;
            for (int i = 0; i < level; i++) Console.Write(" ");
            Console.Write(":");

            Console.WriteLine("<= {0} {1}", node.GetType().ToString(), node.ToString());

            foreach (var item in node.children) 
                Show_AST(item, level + 3);

            for (int i = 0; i < level; i++) Console.Write(" ");
            Console.Write(":");

            Console.WriteLine("=> {0} ", node.ToString());

        }
        static void Main(string[] args)
        {
            StreamReader x = new StreamReader("../../../cool.cl");
            string adsa = x.ReadToEnd();
            Console.WriteLine(adsa);
            var ast = GetAST.Show(adsa);

            if (ast == null)
            {
                Console.ReadLine();
                return;
            }
            
            var arg = (AST.Program)ast;

            if (Test_Semantic.Check(arg))
            {
                var cil = GET_CIL_AST.CIL(arg);
                Console.WriteLine(cil);

                var visitorCIL = new CilToMips(IType.GetAllTypes(arg));
                visitorCIL.Accept(cil);

                var data = visitorCIL.Data.Split('\n');
                var text = visitorCIL.Text.Split('\n');

                StreamWriter mipStreamWriter = new StreamWriter("../../../mips.s");
                
                foreach (var s in data)
                    mipStreamWriter.WriteLine(s);
                
                foreach (var s in text)
                    mipStreamWriter.WriteLine(s);

                mipStreamWriter.Close();
            }
            Console.ReadLine();
        }
    }
}
