using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIL
{
    public static class SystemFunctions
    {
        public static Dictionary<string, CIL_Function> GetAllSysFunctions()
        {
            Dictionary<string, CIL_Function> dic = new Dictionary<string, CIL_Function>();

            //dic["enter_main"] = Enter_main();
            dic["Main___distans"] = Distans();
            dic["Object_abort"] = Fun_abort();
            dic["String_concat"] = Fun_concat();
            dic["Object_copy"] = Fun_copy();
            dic["IO_in_int"] = Fun_in_int();
            dic["IO_in_string"] = Fun_in_string();
            dic["String_length"] = Fun_length();
            dic["IO_out_int"] = Fun_out_int();
            dic["IO_out_string"] = Fun_out_string();
            dic["String_substr"] = Fun_substr();
            dic["Object_type_name"] = Fun_type_name();

            return dic;
        }

        public static CIL_Function Enter_main()
        {
            List<string> args = new List<string>();
            List<string> locals = new List<string>();
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Allocate("this", "Main"));
            inst.Add(new CIL_Call("this", "main", "this",  new List<string>()));
            inst.Add(new CIL_Return("this"));

            return new CIL_Function("enter_main", args, locals, inst);
        }

        public static CIL_Function Distans()
        {
            List<string> par = new List<string> { "s", "p" };
            var lis = new List<CIL_Instruction>()
            {
                new CIL_Allocate("object", "Object"),
                new CIL_Typeof("obj", "object"),
                new CIL_Assig("ret", "0"),
                new CIL_Label("for"),
                new CIL_ArithExpr("expr","s", "p", "="),
                new CIL_ConditionalJump("expr", "finish"),
                new CIL_ArithExpr("expr","p", "obj", "="),
                new CIL_ConditionalJump("expr", "not_father"),
                new CIL_Father("p2", "p"),
                new CIL_Assig("p", "p2"),
                new CIL_ArithExpr("ret","ret", "1", "+"),
                new CIL_Goto("for"),
                new CIL_Label("not_father"),
                new CIL_Assig("ret", "9999"),
                new CIL_Label("finish"),
                new CIL_Return("ret")
            };
            return new CIL_Function("__distans_Main", par, new List<string>() { "object", "ret", "expr", "obj", "p2" }, lis);
        }

        #region IO
        public static CIL_Function Fun_out_string()
        {
            List<string> args = new List<string> { "x" };
            List<string> locals = new List<string>();
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Print_Str("x"));
            inst.Add(new CIL_Return("this"));

            return new CIL_Function("out_string_IO", args, locals, inst);
        }

        public static CIL_Function Fun_out_int()
        {
            List<string> args = new List<string> { "x" };
            List<string> locals = new List<string>();
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Print_Int("x"));
            inst.Add(new CIL_Return("this"));

            return new CIL_Function("out_int_IO", args, locals, inst);
        }

        public static CIL_Function Fun_in_string()
        {
            List<string> args = new List<string> {};
            List<string> locals = new List<string>();
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Read_Str("this"));
            inst.Add(new CIL_Return("this"));

            return new CIL_Function("in_string_IO", args, locals, inst);
        }

        public static CIL_Function Fun_in_int()
        {
            List<string> args = new List<string>();
            List<string> locals = new List<string>();
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Read_Int("this"));   //Ver si poner 2 nodos del AST de read(str,int)
            inst.Add(new CIL_Return("this"));

            return new CIL_Function("in_int_IO", args, locals, inst);
        }
        #endregion

        #region String
        public static CIL_Function Fun_concat()
        {
            // TODO: preguntar como le van a pasar los dos strings a esta funcion ya que si es
            //       this pues tengo que ponerle otra propiedad a los objetos de tipo string
            List<string> args = new List<string> { "s" };
            List<string> locals = new List<string> { "local_string_concat_ret" };
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Concat("local_string_concat_ret", "this", "s"));
            inst.Add(new CIL_Return("local_string_concat_ret"));

            return new CIL_Function("concat_String", args, locals, inst);
        }

        public static CIL_Function Fun_length()
        {
            // TODO: preguntar igual
            List<string> args = new List<string> {};
            List<string> locals = new List<string> { "local_string_length_ret" };
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Length("local_string_length_ret", "this"));
            inst.Add(new CIL_Return("local_string_length_ret"));

            return new CIL_Function("length_String", args, locals, inst);
        }

        public static CIL_Function Fun_substr()
        {
            // TODO: la misma
            List<string> args = new List<string> { "i", "l" };
            List<string> locals = new List<string> { "local_string_substr_ret" };
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Substring("local_string_substr_ret", "this", "i", "l"));

            return new CIL_Function("substr_String", args, locals, inst);
        }

        #endregion

        #region Object

        public static CIL_Function Fun_type_name()
        {
            List<string> args = new List<string> {};
            List<string> locals = new List<string> { "local_object_type_name_ret" };
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            //inst.Add(new CIL_TypeName("local_object_type_name_ret", "this"));
            inst.Add(new CIL_Return("local_object_type_name_ret"));

            return new CIL_Function("type_name_Object", args, locals, inst);
        }

        public static CIL_Function Fun_abort()
        {
            List<string> args = new List<string> {};
            List<string> locals = new List<string> { };
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Abort());
            inst.Add(new CIL_Return(""));

            return new CIL_Function("abort_Object", args, locals, inst);
        }

        public static CIL_Function Fun_copy()
        {
            List<string> args = new List<string> {};
            List<string> locals = new List<string> { "local_object_copy_ret" };
            List<CIL_Instruction> inst = new List<CIL_Instruction>();

            inst.Add(new CIL_Copy("local_object_copy_ret", "this"));
            inst.Add(new CIL_Return("local_object_copy_ret"));

            return new CIL_Function("copy_Object", args, locals, inst);
        }

        #endregion

    }
}
