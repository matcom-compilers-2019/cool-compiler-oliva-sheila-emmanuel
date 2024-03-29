﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CIL
{
    public abstract class CIL_Node
    {
        public int dir;
        public virtual void Accept(IVisitor visitor) => visitor.Accept(this);
    }

    public class CIL_Program : CIL_Node
    {
        public CIL_Code Code;
        public CIL_Data Data;
        public CIL_Types Types;

        public CIL_Program(CIL_Code code, CIL_Data data, CIL_Types types)
        {
            Code = code;
            Data = data;
            Types = types;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += Data.ToString() + "\n";
            ret += Types.ToString() + "\n";
            ret += Code.ToString() + "\n";
            return ret;
        }

    }

    public class CIL_OneType : CIL_Node
    {
        public string name;
        public List<string> Attributes;
        public List<Tuple<string, string>> Methods;

        public CIL_OneType(string name, List<string> attributes, List<Tuple<string, string>> methods)
        {
            this.name = name;
            Attributes = attributes;
            Methods = methods;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += "type " + name + ' ' + "{\n";
            foreach (var item in Attributes)
            {
                ret += "attribute " + item + " ;\n";
            }
            foreach (var item in Methods)
            {
                ret += "method " + item.Item2 + " : " + item.Item1 + ";\n";
            }
            ret += "}\n";
            return ret;
        }
    }

    public class CIL_Types : CIL_Node
    {
        public Dictionary<string, CIL_OneType> _types { get; }

        public CIL_Types(Dictionary<string, CIL_OneType> types)
        {
            _types = types;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += ".TYPES\n";
            foreach (var item in _types.Select(x => x.Value))
            {
                ret += item.ToString();
            }
            return ret;
        }

    }

    public class CIL_Data : CIL_Node
    {
        public Dictionary<string, string> _stringVars;

        public CIL_Data(Dictionary<string, string> vars)
        {
            _stringVars = vars;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += ".DATA\n";
            foreach (var item in _stringVars)
            {
                ret += string.Format(" {0} = {1};\n", item.Key, item.Value);
            }
            return ret;
        }

    }

    public abstract class CIL_Instruction : CIL_Node
    {
        public override void Accept(IVisitor visitor) => visitor.Accept(this);
    }

    public class CIL_Function : CIL_Node
    {
        private string _name;
        public string Name => _name;
        public List<string> Args;   // estos son los nombres de las variables y el lugar que ocupan en
        public List<string> Locals; // memoria en MIPS
        public List<CIL_Instruction> Instructions;

        public CIL_Function(string name, List<string> args, List<string> locals, List<CIL_Instruction> inst)
        {
            _name = name;
            Args = args;
            Locals = locals;
            Instructions = inst;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += "function " + Name + " {\n";
            foreach (var item in Args)
            {
                ret += "ARG " + item + ";\n";
            }
            ret += "\n";
            foreach (var item in Locals)
            {
                ret += "LOCAL " + item + ";\n";
            }
            foreach (var item in Instructions)
            {
                ret += item.ToString();
            }
            ret += "}\n";
            return ret;
        }

    }

    public class CIL_Code : CIL_Node
    {
        public List<CIL_Function> Funcs;

        public CIL_Code(List<CIL_Function> funcs)
        {
            Funcs = funcs;
        }

        public void AddFunc(CIL_Function func)
        {
            Funcs.Add(func);
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += ".CODE\n";
            foreach (var item in Funcs)
            {
                ret += item.ToString();
            }
            return ret;
        }

    }

    public class CIL_Assig : CIL_Instruction
    {
        public string Dest;
        public string RigthMem;

        public CIL_Assig(string dest, string rigthMem)
        {
            Dest = dest;
            RigthMem = rigthMem;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = {1} ;\n", Dest, RigthMem);
            return ret;
        }
    }


    public class CIL_Concat : CIL_Instruction
    {
        public string Dest;
        public string Msg1;
        public string Msg2;

        public CIL_Concat(string dest, string msg1, string msg2)
        {
            Dest = dest;
            Msg1 = msg1;
            Msg2 = msg2;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = CONCAT {1} {2}\n", Dest, Msg1, Msg2);
            return ret;
        }

    }

    public class CIL_Substring : CIL_Instruction
    {
        public string Dest;
        public string Msg;
        public string Index;
        public string Length;

        public CIL_Substring(string dest, string msg, string index, string length)
        {
            Dest = dest;
            Msg = msg;
            Index = index;
            Length = length;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = SUBSTRING {1} {2} ;\n", Dest, Index, Length);
            return ret;
        }

    }

    public class CIL_UnaryExpr : CIL_Instruction
    {
        public string dest, op;
        public string expr;
        public CIL_UnaryExpr(string dest, string op, string exp)
        {
            this.dest = dest;
            this.op = op;
            expr = exp;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = {1} {2} ;\n", dest, op, expr);
            return ret;
        }

    }

    public class CIL_ArithExpr : CIL_Instruction
    {
        public string Dest;
        public string RigthOp;
        public string LeftOp;
        public string Op;

        public CIL_ArithExpr(string dest, string leftOp, string rigthOp, string op)
        {
            Dest = dest;
            RigthOp = rigthOp;
            LeftOp = leftOp;
            Op = op;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = {1} {2} {3} ;\n", Dest, LeftOp, Op, RigthOp);
            return ret;
        }

    }


    public class CIL_GetAttr : CIL_Instruction
    {
        public string Dest;
        public CIL_MyVar Instance;
        public string Attr;

        public CIL_GetAttr(string dest, CIL_MyVar instance, string attr)
        {
            Dest = dest;
            Instance = instance;
            Attr = attr;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = GETATTR {1} {2} ;\n", Dest, Instance, Attr);
            return ret;
        }

    }

    public class CIL_SetAttr : CIL_Instruction
    {
        public CIL_MyVar Instance;
        public string Attr;
        public string Value;

        public CIL_SetAttr(CIL_MyVar instance, string attr, string value)
        {
            Instance = instance;
            Attr = attr;
            Value = value;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("SETATTR {0} {1} {2} ;\n", Instance, Attr, Value);
            return ret;
        }

    }

    public class CIL_VCall : CIL_Instruction
    {
        public string Dest;
        public string MyType;
        public string Name;
        public List<string> Args;

        public CIL_VCall(string dest, string myType, string name, List<string> args)
        {
            Dest = dest;
            MyType = myType;
            Name = name;
            Args = args;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            foreach (var item in Args)
            {
                ret += "   ARG " + item + " ;\n";
            }
            ret += string.Format("{0} = VCALL {1} {2} ;\n", Dest, MyType, Name);
            return ret;
        }

    }

    public class CIL_Typeof : CIL_Instruction
    {
        public string dest;
        public string expr;
        public CIL_Typeof(string dest, string expr)
        {
            this.dest = dest;
            this.expr = expr;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = TYPEOF {1} ;\n", dest, expr);
            return ret;
        }

    }

    public class CIL_Father : CIL_Instruction
    {
        public string instans;
        public string dest;
        public CIL_Father(string dest, string instans)
        {
            this.dest = dest;
            this.instans = instans;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = FATHER {1} ;\n", dest, instans);
            return ret;
        }
    }

    public class CIL_Call : CIL_Instruction
    {
        public string Dest;
        public string Name;
        public string This;
        public List<string> Args;

        public CIL_Call(string dest, string name, string @this, List<string> args)
        {
            Dest = dest;
            Name = name;
            This = @this;
            Args = args;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            foreach (var item in Args)
            {
                ret += "   ARG " + item + " ;\n";
            }
            ret += string.Format("{0} = CALL {1} ;\n", Dest, Name);
            return ret;
        }
    }

    public class CIL_Allocate : CIL_Instruction
    {
        public string Dest;
        public string MyType;

        public CIL_Allocate(string dest, string myType)
        {
            Dest = dest;
            MyType = myType;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = ALLOCATE {1} ;\n", Dest, MyType);
            return ret;
        }
    }

    public class CIL_Load : CIL_Instruction
    {
        public string Dest;
        public string Msg;

        public CIL_Load(string dest, string msg)
        {
            Dest = dest;
            Msg = msg;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = LOAD {1} ;\n", Dest, Msg);
            return ret;
        }
    }

    public class CIL_Length : CIL_Instruction
    {
        public string Dest;
        public string Msg;

        public CIL_Length(string dest, string msg)
        {
            Dest = dest;
            Msg = msg;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = LENGTH {1} ;\n", Dest, Msg);
            return ret;
        }
    }

    public class CIL_Str : CIL_Instruction
    {
        public string Dest;
        public string MyVar;

        public CIL_Str(string dest, string myVar)
        {
            Dest = dest;
            MyVar = myVar;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = STR {1} ;\n", Dest, MyVar);
            return ret;
        }

    }

    public class CIL_Label : CIL_Instruction
    {
        public string _label;

        public CIL_Label(string label)
        {
            _label = label;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("LABEL {0} ;\n", _label);
            return ret;
        }
    }

    public class CIL_Return : CIL_Instruction
    {
        public string value;

        public CIL_Return(string value)
        {
            this.value = value;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("RETURN {0} ;\n", (value == null) ? "" : value);
            return ret;
        }

    }

    public class CIL_Read_Str : CIL_Instruction
    {
        public string _var;

        public CIL_Read_Str(string myVar)
        {
            _var = myVar;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = READ ;\n", _var);
            return ret;
        }
    }
    
    public class CIL_Read_Int : CIL_Instruction
    {
        public string _var;

        public CIL_Read_Int(string myVar)
        {
            _var = myVar;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} = READ ;\n", _var);
            return ret;
        }
    }

    public class CIL_Print_Str : CIL_Instruction
    {
        public string _var;

        public CIL_Print_Str(string myVar)
        {
            _var = myVar;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("PRINT {0} ;\n", _var);
            return ret;
        }
    }

    public class CIL_Print_Int : CIL_Instruction
    {
        public string _var;

        public CIL_Print_Int(string myVar)
        {
            _var = myVar;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("PRINT {0} ;\n", _var);
            return ret;
        }

    }

    public class CIL_Goto : CIL_Instruction
    {
        public string _label;

        public CIL_Goto(string label)
        {
            _label = label;
        }

        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("GOTO {0} ;\n", _label);
            return ret;
        }
    }
    public class CIL_ConditionalJump : CIL_Instruction
    {
        public string cond;
        public string label;
        public CIL_ConditionalJump(string cond, string label)
        {
            this.cond = cond;
            this.label = label;
        }
        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("IF {0} GOTO {1} ;\n", cond, label);
            return ret;
        }
    }

    public class CIL_is_void : CIL_Instruction
    {
        public string arg;
        public string ret;
        public CIL_is_void(string ret, string arg)
        {
            this.ret = ret;
            this.arg = arg;
        }
        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} ISVOID {1} ;\n", ret, arg);
            return ret;
        }

    }

    public class CIL_TypeName : CIL_Instruction
    {
        public override void Accept(IVisitor visitor) => visitor.Accept(this);
    }

    public class CIL_Abort : CIL_Instruction
    {
        public override void Accept(IVisitor visitor) => visitor.Accept(this);
    }
    
    public class CIL_Copy : CIL_Instruction
    {
        public string Dest;
        public string Instance;

        public CIL_Copy(string dest, string instance)
        {
            Dest = dest;
            Instance = instance;
        }
        
        public override void Accept(IVisitor visitor) => visitor.Accept(this);
    }

    public class CIL_MyVar : CIL_Instruction
    {
        public string Value;
        public string Type;

        public CIL_MyVar(string value, string type)
        {
            Value = value;
            Type = type;
        }
        
        public override void Accept(IVisitor visitor) => visitor.Accept(this);

        public override string ToString()
        {
            string ret = "";
            ret += string.Format("{0} MyVar {1} {2}", ret, Value, Type);
            return ret;
        }
    }

    
}