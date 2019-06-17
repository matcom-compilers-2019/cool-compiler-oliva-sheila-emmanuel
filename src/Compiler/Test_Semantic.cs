using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AST;
using Logic;
using Logic.CheckSemantic;
using System.IO;

namespace Compiler
{
    class Test_Semantic
    {
        public static bool Check(AST.Program ast)
        {
            var DefChecker = new DefinitionsChecker();
            bool check = DefChecker.Visit(ast);
            Console.WriteLine(DefChecker.Logger);
            if (check)
            {
                Console.WriteLine("Definiciones OK");
                Dictionary<string, IType> types = IType.GetAllTypes(ast);

                bool check_inherits = InheritsChecker.Check(ast, types);
                if (check_inherits)
                {
                    Console.WriteLine("Herencia OK");
                    ContextType context = new ContextType(types);
                    var SymChecker = new SymCheckerVisitor(context);
                    bool check_sym = SymChecker.Visit(ast);
                    Console.WriteLine(SymChecker.Logger);

                    if (check_sym)
                    {
                        Console.WriteLine("Simbolos OK");

                        context = new ContextType(types);
                        var TypeCheck = new TypeCheckerVisitor(context);
                        TypeCheck.Visit(ast);
                        Console.WriteLine(TypeCheck.Logger);
                        return true;
                    }
                    else Console.WriteLine("Simbolos al berro");
                }
                else Console.WriteLine("Herencia al berro");
            }
            else Console.WriteLine("Definiciones al berro");
            return false;
        }
        
    }
}
