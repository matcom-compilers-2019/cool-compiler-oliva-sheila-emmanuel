using System;
using System.Collections.Generic;
using System.Linq;
using CIL;
using AST;

namespace Logic
{
    public class Take_str
    {
        int id;
        string str;
        public Take_str(string str, int id = 0)
        {
            this.id = id;
            this.str = str;
        }
        public string take(string name)
        {
            return str + '_' + name + id++.ToString();
        }
    }

    public class Scope
    {
        public Scope father;
        public string id;
        Take_str take;
        Dictionary<string, string> locals;

        public Scope(string id, Scope father = null)
        {
            this.id = id;
            this.father = father;
            take = new Take_str(id);
            locals = new Dictionary<string, string>();
        }

        public string Var(string id = "")
        {
            var x = take.take(id);
            return x;
        }

        public Scope New_scope(string id)
        {
            return new Scope(id, this);
        }

        public string Get_var(string key)
        {
            if (locals.ContainsKey(key)) return locals[key];
            if (father == null) return null;
            return father.Get_var(key);
        }

        public void Add_var(string key, string value)
        {
            locals.Add(key, value);
        }
    }

    public class Current_Method
    {
        public Scope current_scope;
        public List<string> args;
        public Dictionary<string, string> locals;
        public List<CIL_Instruction> body;


        public Current_Method()
        {
            args = new List<string>();
            locals = new Dictionary<string, string>();
            body = new List<CIL_Instruction>();
            current_scope = new Scope("");
        }

        public string Add_local(string v, bool is_expr = false)
        {
            var local = current_scope.Var(v);
            if (!is_expr)
            {
                locals.Add(v, local);
                current_scope.Add_var(v, local);
            }
            else locals.Add(local, local);
            return local;
        }

        public void Add_Instruction(CIL_Instruction inst)
        {
            body.Add(inst);
        }


        public void Add_scope(string id)
        {
            current_scope = current_scope.New_scope(id);
        }

        public string Take_var(string name)
        {
            return current_scope.Var(name);
        }

        public void End_scope()
        {
            current_scope = current_scope.father;
        }
    }

    public class GET_CIL_AST
    {
        public static CIL_Program CIL(Program node)
        {
            var comp = new CoolToCil();
            comp.Visit(node);
            List<CIL_Function> code = new List<CIL_Function>(comp.Code.Select(x => x.Value));
            return new CIL_Program(new CIL_Code(code), new CIL_Data(comp.Data), new CIL_Types(comp.Types));
        }
    }

    public class CoolToCil : IVisitorAST<string>
    {
        Take_str take_data;

        public Dictionary<string, string> Data;
        public Dictionary<string, CIL_Function> Code;
        public Dictionary<string, CIL_OneType> Types;

        Current_Method method;
        Dictionary<string, IType> Types_Cool;
        IType current_type;

        public CoolToCil()
        {
            method = new Current_Method();
            take_data = new Take_str("data");
            Data = new Dictionary<string, string>();
            Data[""] = "vacio";
            Code = SystemFunctions.GetAllSysFunctions(); 
            Types = new Dictionary<string, CIL_OneType>();

        }

        public string Visit(Node node)
        {
            throw new NotImplementedException();
        }


        public string Visit(Program node)
        {
            Types_Cool = IType.GetAllTypes(node);

            foreach (var key in Types_Cool.Keys)
            {
                IType type = Types_Cool[key];
                List<string> attrs = new List<string>();
                List<Tuple<string, string>> mtds = new List<Tuple<string, string>>();

                foreach (var attr in type.AllAttributes())
                {
                    attrs.Add(attr.Name);
                }

                foreach (var mtd in type.AllMethods())
                {
                    string mtd_name = mtd.Name + "_" + Types_Cool[key].GetTypeWhereMethod(mtd.Name);
                    mtds.Add(new Tuple<string, string>(mtd.Name, mtd_name));
                }

                Types[key] = new CIL_OneType(key, attrs, mtds);
            }

            foreach (var item in node.list)
            {
                item.Visit(this);
            }
            return "";
        }

        public string Visit(Class_Def node)
        {
            current_type = Types_Cool[node.type.s];
            
            // Formando el metodo init
            method = new Current_Method();
            string father = Types_Cool[node.type.s].Father.Name;
            if (father != "IO" && father != "Object")
            {
                string f = method.Add_local("father", true);
                method.Add_Instruction(new CIL_Allocate(f, father));
                method.Add_Instruction(new CIL_VCall(f, father, "__init", new List<string> {f}));
                foreach (string attr in Types[father].Attributes)
                {
                    string a = method.Add_local("attr", true);
                    method.Add_Instruction(new CIL_GetAttr(a, new CIL_MyVar(f, father), attr));
                    method.Add_Instruction(new CIL_SetAttr(new CIL_MyVar("this", node.type.s), attr, a));
                }
            }
            foreach (var attr in node.attr.list_Node)
            {
                attr.Visit(this);
            }
            
            method.Add_Instruction(new CIL_Return("this"));
            string mtdName = "__init_" + node.type.s;
            Code.Add(mtdName, new CIL_Function(mtdName, new List<string>(), new List<string>(method.locals.Values), method.body));
            
            foreach (Method_Def item in node.method.list_Node)
            {
                Visit(item);
            }
            return "";
        }

        public string Visit(Method_Def node)
        {
            method = new Current_Method();
            method.args = new List<string>(node.args.list_Node.Select(m => m.name.name));
            string solution = node.exp.Visit(this);
            method.Add_Instruction(new CIL_Return(solution));
            string mtdName = node.name.name + "_" + current_type.Name;
            Code.Add(mtdName, new CIL_Function(mtdName, new List<string>(node.args.list_Node.Select(x => x.name.name)), new List<string>(method.locals.Values), method.body));
            return "";
        }

        public string Visit(Attr_Def node)
        {
            var exp = "";
            if (node.exp == null)
            {
                if (node.type.s == "Int") exp = new Const("0").Visit(this);
                else if (node.type.s == "String")
                {
                    exp = method.Add_local("expr", true);
                    method.Add_Instruction(new CIL_Load(exp, Data[""]));
                }
                else if (node.type.s == "Bool") exp = new Const("false").Visit(this);
                else exp = "void";
            }
            else exp = node.exp.Visit(this);
            method.Add_Instruction(new CIL_SetAttr(new CIL_MyVar("this", current_type.Name), node.name.name, exp));
            return "";
        }

        public string Visit(Formal node)
        {
            throw new NotImplementedException();
        }

        public string Visit(Type_cool node)
        {
            throw new NotImplementedException();
        }

        public string Visit(Expr node)
        {
            throw new NotImplementedException();
        }

        public string Visit(Str node)
        {
            if (!Data.ContainsKey(node.s))
            {
                string v = take_data.take(method.current_scope.id);
                Data.Add(node.s, v);
            }
            var exp = method.Add_local("exp", true);
            method.Add_Instruction(new CIL_Load(exp, Data[node.s]));
            return exp;
        }

        public string Visit(Call_Method node)
        {
            List<string> Args = new List<string>();
            foreach (var exp in node.args.list_Node)
            {
                string value = exp.Visit(this);
                Args.Add(value);
            }
            var expr = method.Add_local("expr", true);

            method.Add_Instruction(new CIL_Call(expr, node.name.name, "this",  Args));
            return expr;

        }

        public string Visit(Dispatch node)
        {
            var exp = node.exp.Visit(this);

            List<string> Args = new List<string> {exp};
            foreach (var item in node.call.args.list_Node)
            {
                string value = item.Visit(this);
                Args.Add(value);
            }

            var ret = method.Add_local("expr", true);
            
            if (node.s != "sin castear ")
            {
                method.Add_Instruction(new CIL_VCall(ret, node.s, node.call.name.name, Args));
            }
            else
            {
                method.Add_Instruction(new CIL_Call(ret, node.call.name.name, exp, Args));
            }
            
            return ret;
        }

        public string Visit(Let_In node)
        {
            List<string> attrs = new List<string>();
            foreach (var attr in node.attrs.list_Node)
            {
                var a = "";
                if (attr.exp == null)
                {
                    if (attr.type.s == "Int") a = new Const("0").Visit(this);
                    else if (attr.type.s == "String")
                    {
                        a = method.Add_local("expr", true);
                        method.Add_Instruction(new CIL_Load(a, Data[""]));
                    }
                    else if (attr.type.s == "Bool") a = new Const("false").Visit(this);
                    else a = "void";
                }
                else a = attr.exp.Visit(this);
                attrs.Add(a);
            }

            method.Add_scope("let");

            for (int i = 0; i < attrs.Count; i++)
            {
                string name = method.Add_local(node.attrs.list_Node[i].name.name);
                if (attrs[i] != null) method.Add_Instruction(new CIL_Assig(name, attrs[i]));
            }
            var exp = node.exp.Visit(this);
            var ret = method.Add_local("expr", true);
            method.Add_Instruction(new CIL_Assig(ret, exp));
            method.End_scope();
            return ret;
        }

        public string Visit(If_Else node)
        {
            var cond = node.cond.Visit(this);
            var then = node.then.Visit(this);

            var ret = method.Add_local("ret_if", true);
            var begin_if = method.Take_var("begin_if");
            var end_if = method.Take_var("end_if");
            method.Add_Instruction(new CIL_ConditionalJump(cond, begin_if));

            if (node.elsse != null)
            {
                var elsse = node.elsse.Visit(this);
                method.Add_Instruction(new CIL_Assig(ret, elsse));
                method.Add_Instruction(new CIL_Goto(end_if));
            }
            method.Add_Instruction(new CIL_Label(begin_if));
            method.Add_Instruction(new CIL_Assig(ret, then));
            method.Add_Instruction(new CIL_Label(end_if));
            return ret;
        }

        public string Visit(While_loop node)
        {
            var begin_while = method.Take_var("begin_while");
            var body_while = method.Take_var("body_while");
            var end_while = method.Take_var("end_while");

            var ret = method.Add_local("ret_while", true);
            var cond = node.exp1.Visit(this);
            var body = node.exp2.Visit(this);
            method.Add_Instruction(new CIL_Label(begin_while));
            method.Add_Instruction(new CIL_ConditionalJump(cond, body_while));
            method.Add_Instruction(new CIL_Goto(end_while));
            method.Add_Instruction(new CIL_Label(body_while));
            method.Add_Instruction(new CIL_Assig(ret, body));
            method.Add_Instruction(new CIL_Goto(begin_while));
            method.Add_Instruction(new CIL_Label(end_while));
            return ret;

        }

        public string Visit(Body node)
        {
            string s = "";
            foreach (var item in node.list.list_Node)
            {
                s = item?.Visit(this);
            }
            return s;
        }

        public string Visit(New_type node)
        {
            var ret = method.Add_local("expr", true);
            method.Add_Instruction(new CIL_Allocate(ret, node.type.s));
            method.Add_Instruction(new CIL_VCall(ret, node.type.s, "__init", new List<string> {ret}));
            return ret;
        }

        public string Visit(IsVoid node)
        {
            var v = method.Add_local("expr", true);
            string exp = node.exp.Visit(this);
            method.Add_Instruction(new CIL_is_void(v, exp));
            return v;
        }

        public string Visit(BinaryExpr node)
        {
            var left = node.left.Visit(this);
            var right = node.right.Visit(this);
            var ret = method.Add_local("expr", true);

            method.Add_Instruction(new CIL_ArithExpr(ret, left, right, node.op));
            return ret;
        }

        public string Visit(UnaryExpr node)
        {
            var exp = node.exp.Visit(this);
            var ret = method.Add_local("expr", true);
            method.Add_Instruction(new CIL_UnaryExpr(ret, node.op, exp));
            return ret;
        }

        public string Visit(Assign node)
        {
            var exp = node.exp.Visit(this);
            if (Data.ContainsValue(exp))
            {
                string dest = method.Add_local("dest", true);
                method.Add_Instruction(new CIL_Load(dest, exp));
                return dest;
            }
            else
            {
                string ret = method.current_scope.Get_var(node.id.name);
                if (ret == null && method.args.Contains(node.id.name))
                {
                    method.Add_Instruction(new CIL_Assig(node.id.name, exp));
                    return exp;
                }
                else if (ret == null && current_type.GetAttribute(node.id.name) != null)
                {
                    method.Add_Instruction(new CIL_SetAttr(new CIL_MyVar("this", node.type.s), node.id.name, exp));
                    return exp;
                }
                else
                {
                    method.Add_Instruction(new CIL_Assig(ret, exp));
                    return exp;
                }
            }
        }

        public string Visit(Id node)
        {
            string var = method.current_scope.Get_var(node.name);

            if (node.name == "self") return "this";

            if (var == null && method.args.Contains(node.name))
            {
                return node.name;
            }
            else if (var == null && current_type.GetAttribute(node.name) != null)
            {
                var attr = method.Add_local("attr", true);
                method.Add_Instruction(new CIL_GetAttr(attr, new CIL_MyVar("this", node.type.s), node.name));
                return attr;
            }
            else
            {
                return var;
            }
        }

        public string Visit(Const node)
        {
            int x;
            if (int.TryParse(node.name, out x))
            {
                return node.name;
            }
            else if (node.name == "true")
            {
                return "1";
            }
            else return "0";
        }

        public string Visit(Lista<Node> node)
        {
            foreach (var item in node.list_Node)
            {
                item.Visit(this);
            }
            return "";
        }
       
        public string Visit(CASE_OF node)
        {
            var exp = node.exp.Visit(this);
            var solution = method.Add_local("expr", true);
            var exp_type = method.Add_local("exp_type", true);
            var match_type = method.Add_local("match", true);
            var cmp = method.Add_local("cmp", true);
            method.Add_Instruction(new CIL_Typeof(exp_type, exp));

            var finish = method.current_scope.Var("finish");
            method.Add_Instruction(new CIL_Assig(match_type, "9999"));
            List<string> distances = new List<string>();
            List<string> labels = new List<string>();
            foreach (var item in node.branches.list_Node)
            {
                labels.Add(method.current_scope.Var("label"));
                string branch_type = method.Add_local("branch_type", true);
                string branch_inst = method.Add_local("branch_inst", true);
                method.Add_Instruction(new CIL_Allocate(branch_inst, item.formal.type.s));
                method.Add_Instruction(new CIL_Typeof(branch_type, branch_inst));
                string distans = method.Add_local("distans", true);
                distances.Add(distans);
                method.Add_Instruction(new CIL_Call(distans, "__distans", "this", new List<string>() { branch_type, exp_type }));
                method.Add_Instruction(new CIL_ArithExpr(cmp, match_type,distans, "<"));
                var _if = method.current_scope.Var("if");
                method.Add_Instruction(new CIL_ConditionalJump(cmp, _if));
                method.Add_Instruction(new CIL_Assig(match_type,distans));

                method.Add_Instruction(new CIL_Label(_if));
            }

            for (int i = 0; i < distances.Count; i++)
            {
                method.Add_Instruction(new CIL_ArithExpr(cmp, match_type, distances[i], "="));
                method.Add_Instruction(new CIL_ConditionalJump(cmp, labels[i]));
            }

            for (int i = 0; i < distances.Count; i++)
            {
                method.Add_Instruction(new CIL_Label(labels[i]));
                method.Add_scope("branch");
                var dest = method.Add_local(node.branches.list_Node[i].formal.name.name);
                var exp_branch = node.branches.list_Node[i].exp.Visit(this);
                method.Add_Instruction(new CIL_Assig(solution, exp_branch));
                method.End_scope();
                method.Add_Instruction(new CIL_Goto(finish));
            }
            method.Add_Instruction(new CIL_Label(finish));
            return solution;
        }

        public string Visit(Branch node)
        {
            throw new NotImplementedException();
        }
    }
}