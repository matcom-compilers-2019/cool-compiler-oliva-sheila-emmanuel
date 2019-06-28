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
                Dictionary<string, IType> types = IType.GetAllTypes(ast);

                bool check_inherits = InheritsChecker.Check(ast, types);
                if (check_inherits)
                {
                    ContextType context = new ContextType(types);
                    var SymChecker = new SymCheckerVisitor(context);
                    bool check_sym = SymChecker.Visit(ast);

                    if (check_sym)
                    {
                        context = new ContextType(types);
                        var TypeCheck = new TypeCheckerVisitor(context);
                        TypeCheck.Visit(ast);
                        if (TypeCheck.Logger == "")
                        {
                            Console.WriteLine("Chequeo semantico OK \n");
                            return true;
                        }
                        else Console.WriteLine(TypeCheck.Logger);
                    }
                    else Console.WriteLine(SymChecker.Logger);
                }
            }
            Console.WriteLine("Falla en el chequeo semantico");
            return false;
        }
        
    }
}
