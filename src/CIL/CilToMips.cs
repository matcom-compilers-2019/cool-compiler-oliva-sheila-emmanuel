﻿using System;
using System.Collections.Generic;
using AST;

namespace CIL
{
    public class Scope
    {
        public Scope father;
        public Dictionary<string, int> VarInStack;

        public Scope()
        {
            VarInStack = new Dictionary<string, int>();
        }
    }
    
    public class CilToMips : IVisitor
    {
        public string Data = ".data\n";
        public string Text = ".globl main\n.text\n";
        public Scope CurrentScope;
        public Dictionary<string, IType> Clases;
        private static int _indx = 0;
        private Dictionary<string, int> VT;

        public int GetNum()
        {
            _indx++;
            return _indx;
        }

        public CilToMips(Dictionary<string, IType> clases)
        {
            Clases = clases;
            VT = new Dictionary<string, int>();
        }
        
        public void Accept(CIL_Program prog)
        {
            Text += PlantillaMIPS.StringLength + "\n" +
                    PlantillaMIPS.StringConcat + "\n" +
                    PlantillaMIPS.StringSubstring + "\n" +
                    PlantillaMIPS.InputString + "\n" +
                    PlantillaMIPS.Copy + "\n" +
                    PlantillaMIPS.ZeroException + "\n";
            
            prog.Types.Accept(this);
            prog.Data.Accept(this);
            prog.Code.Accept(this);  
         
            Text += "\nmain:\n" +
                    "\t sw $zero, 0($sp) \n" +
                    "\t sw $ra, -4($sp) \n" +
                    "\t sw $fp, -8($sp) \n" +
                    "\t subu $sp, $sp, 12\n" +
                    "\t addu $fp, $sp, 12\n\n" +
                    "\t jal Main.constructor \n" +
                    "\t sw $v0, -12($fp) \n" +
                    "\t sw $v0, -12($sp) \n" +
                    "\t jal __init_Main \n" +
                    "\t sw $v0, -12($fp) \n" +
                    "\t sw $v0, -12($sp) \n" +
                    "\t jal main_Main \n" +
                    "\n\t lw $ra, -4($fp) \n" +
                    "\t lw $fp, -8($fp) \n" +
                    "\t addu $sp, $sp, " + 12 + "\n" +
                    "\t jr $ra \n\n";
        }

        public void Accept(CIL_Node node)
        {
            throw new System.NotImplementedException();
        }

        public void Accept(CIL_OneType ot)
        {
            Data += "vt." + ot.name + ": .word ";

            if (Clases[ot.name].Father != null)
                Data += "vt." + Clases[ot.name].Father.Name + ", ";
            else
                Data += "0, ";
            
            int offset = 1;
            // ot.Methods en Item1 tiene los nombres de los metodos como tal
            // y en Item2 esta el nombre de los metodos dentro de la clase
            foreach (var method in ot.Methods)
            {
                Data += method.Item2 + ", ";

                if (!VT.ContainsKey(method.Item1))
                    VT[method.Item1] = offset;

                offset++;
            }

            Data += "0 \n";
            
            string msgName = "str" + GetNum();
            Data += msgName + ": .asciiz \"" + ot.name + "\"\n";
            
            Text += ot.name + ".constructor:\n";

            Text += "\t sw $zero, 0($sp) \n" +
                    "\t sw $ra, -4($sp) \n" +
                    "\t sw $fp, -8($sp) \n" +
                    "\t subu $sp, $sp, " + 12 + "\n" +
                    "\t addu $fp, $sp, " + 12 + "\n\n";

            int space = 4 * (ot.Attributes.Count + 3);

            Text += "\t li $v0, 9 \n" +
                    "\t li $a0, " + space + "\n" +
                    "\t syscall \n";

            Text += "\t sw $a0, 0($v0) \n" +
                    "\t la $a0, " + msgName + "\n" +
                    "\t sw $a0, 4($v0) \n";
            
            Text += "\t la $a0, vt." + ot.name + "\n" +
                    "\t sw $a0, 8($v0) \n";

            for (int i = 0; i < ot.Attributes.Count; i++)
            {
                int temp = 4 * (3 + i);
                Text += "\t li $a0, 0 \n" +
                        "\t sw $a0, " + temp + "($v0) \n";
            }
            
            Text += "\n\t lw $ra, -4($fp) \n" +
                    "\t lw $fp, -8($fp) \n" +
                    "\t addu $sp, $sp, " + 12 + "\n" +
                    "\t j $ra \n" +
                    "#########################################################################\n";
        }

        public void Accept(CIL_Types myType)
        {
            foreach (var value in myType._types.Values)
            {
                value.Accept(this);
            }
        }

        public void Accept(CIL_Data data)
        {
            Data += "\nbuffer:             .space    65536 \n" +
                    "zero:               .byte     0 \n" +
                    "strsubstrexception: .asciiz \"Substring index exception\" \n" +
                    "divisionZero:       .asciiz \"Division by Zero Exception\" \n\n";
            
            foreach (var strVar in data._stringVars.Keys)
                Data += data._stringVars[strVar] + ":\t .asciiz \t" + "\"" + strVar + "\"\n";
        }

        public void Accept(CIL_Instruction ins)
        {
            throw new System.NotImplementedException();
        }

        public void Accept(CIL_Function function)
        {
            var scope = new Scope {father = CurrentScope};
            CurrentScope = scope;
            
            // Las funciones van a recivir los parametros a partir de la
            // tercera posicion del stack (-12($sp)), en las dos primeras
            // van ra (-4($sp)) y fp (-8($sp))
            // Despues de los Args van los Locals

            scope.VarInStack["void"] = 0;
            scope.VarInStack["this"] = -12;
            
            int i = -16;
            foreach (var arg in function.Args)
            {
                scope.VarInStack[arg] = i;
                i -= 4;
            }

            foreach (var local in function.Locals)
            {
                scope.VarInStack[local] = i;
                i -= 4;
            }
            
            int n = 4 * (function.Args.Count + function.Locals.Count + 2); 

            Text += function.Name + ":\n";

            Text += "\t sw $zero, 0($sp) \n" +
                    "\t sw $ra, -4($sp) \n" +
                    "\t sw $fp, -8($sp) \n" +
                    "\t subu $sp, $sp, " + n + "\n" +
                    "\t addu $fp, $sp, " + n + "\n";

            foreach (var inst in function.Instructions)
                inst.Accept(this);
            
            Text += "\t lw $ra, -4($fp) \n" +
                    "\t lw $fp, -8($fp) \n" +
                    "\t addu $sp, $sp, " + n + "\n" +
                    "\t j $ra \n" +
                    "#########################################################################\n";

            CurrentScope = CurrentScope.father;
        }

        public void Accept(CIL_Code code)
        {
            foreach (var cilFunction in code.Funcs)
                cilFunction.Accept(this);
        }

        public void Accept(CIL_Assig assig)
        {
            if (int.TryParse(assig.RigthMem, out int n))
            {
                Text += "\t li $a0, " + n + "\n" +
                        "\t sw $a0, " + CurrentScope.VarInStack[assig.Dest] + "($fp) \n";
            }
            else
            {
                Text += "\t lw $a0, " + CurrentScope.VarInStack[assig.RigthMem] + "($fp) \n" +
                        "\t sw $a0, " + CurrentScope.VarInStack[assig.Dest] + "($fp) \n";
            }
        }

        public void Accept(CIL_Concat concat)
        {
            // -12($sp) = string a, -16($sp) = string b
            Text += "\t la $a0, " + concat.Msg1 + "\n" +
                    "\t sw $a0, -12($sp) \n" +
                    "\t la $a0, " + concat.Msg2 + "\n" +
                    "\t sw $a0, -16($sp) \n" +
                    "\t jal _stringconcat \n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[concat.Dest] + "($sp) \n";
        }

        public void Accept(CIL_Substring substring)
        {
            // -12($sp) = msg, -16($sp) = int index, -20($sp) = int length
            Text += "\t la $a0, " + substring.Msg + "\n" +
                    "\t sw $a0, -12($sp) \n";

            if (int.TryParse(substring.Index, out int index))
                Text += "\t li $a0, " + index + "\n";
            else
                Text += "\t lw $a0, " + CurrentScope.VarInStack[substring.Index] + "($fp) \n";
            
            Text += "\t sw $a0, -16($sp) \n";

            if (int.TryParse(substring.Length, out int length))
                Text += "\t li $a0, " + length + "\n";
            else
                Text += "\t lw $a0, " + CurrentScope.VarInStack[substring.Length] + "($fp) \n";

            Text += "\t sw $a0, -20($sp) \n";

            Text += "\t jal _stringsubstr \n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[substring.Dest] + "($sp) \n";
        }

        public void Accept(CIL_ArithExpr arithExpr)
        {
            // Calculando la direccion de memoria o el entero
            // que representa al miembro izquierdo de la operacion
            string leftMem = "";
            
            if (int.TryParse(arithExpr.LeftOp, out int n1))
                leftMem = "\t li $t1, " + n1 + "\n";
            else
                leftMem = "\t lw $t1, " + CurrentScope.VarInStack[arithExpr.LeftOp] + "($fp) \n";

            // Calculando la direccion de memoria o el entero
            // que representa al miembro derecho de la operacion
            string rigthMem = "";

            if (int.TryParse(arithExpr.RigthOp, out int n2))
                rigthMem = "\t li $t2, " + n2 + "\n";
            else
                rigthMem = "\t lw $t2, " + CurrentScope.VarInStack[arithExpr.RigthOp] + "($fp) \n";

            string operation = "";
            var num = GetNum();
            switch (arithExpr.Op)
            {
                    case "+":
                        operation += "\t add $t0, $t1, $t2 \n";
                        break;
                    case "-":
                        operation += "\t sub $t0, $t1, $t2 \n";
                        break;
                    case "*":
                        operation += "\t mulo $t0, $t1, $t2 \n";
                        break;
                    case "/":
                        operation += "\t beq $t2, $zero, _zeroException \n" +
                                     "\t div $t0, $t1, $t2 \n";
                        break;
                    case "<":
                        operation += "\t sub $t0, $t1, $t2 \n" +
                                     "\t bltz $t0, $true" + num + " \n" +
                                     "\t li $t0, 0 \n" +
                                     "\t j $end" + num + " \n" +
                                     "\n$true" + num + ": \n" +
                                     "\t li $t0, 1 \n" +
                                     "\n$end" + num + ":\n\n";
                        break;
                    case "<=":
                        operation += "\t sub $t0, $t1, $t2 \n" +
                                     "\t blez $t0, $true" + num + " \n" +
                                     "\t li $t0, 0 \n" +
                                     "\t j $end" + num + " \n" +
                                     "\n$true" + num + ": \n" +
                                     "\t li $t0, 1 \n" +
                                     "\n$end" + num + ":\n\n";
                        break;
                    case "=":
                        operation += "\t beq $t1, $t2, $true" + num + " \n" +
                                     "\t li $t0, 0 \n" +
                                     "\t j $end" + num + " \n" +
                                     "\n$true" + num + ": \n" +
                                     "\t li $t0, 1 \n" +
                                     "\n$end" + num + ":\n\n";
                        break;
                    default:
                        throw new InvalidOperationException("Operacion " + arithExpr.Op + " no definida");
            }

            Text += leftMem +
                    rigthMem +
                    operation +
                    "\t sw $t0, " + CurrentScope.VarInStack[arithExpr.Dest] + "($fp) \n";
        }

        public void Accept(CIL_GetAttr getAttr)
        {
            var type = getAttr.Instance.Type;
            
            var attr = Clases[type].GetAttribute(getAttr.Attr);
            int indx = Clases[type].AllAttributes().IndexOf(attr);

            int pos = 12 + (indx * 4);

            Text += "\t lw $v0, " + CurrentScope.VarInStack[getAttr.Instance.Value] + "($fp) \n" +
                    "\t lw $a0, " + pos + "($v0) \n" +
                    "\t sw $a0, " + CurrentScope.VarInStack[getAttr.Dest] + "($fp) \n";
        }

        public void Accept(CIL_SetAttr setAttr)
        {
            var type = setAttr.Instance.Type;
            
            var attr = Clases[type].GetAttribute(setAttr.Attr);
            int indx = Clases[type].AllAttributes().IndexOf(attr);

            int pos = 12 + (indx * 4);

            Text += "\t lw $v0, " + CurrentScope.VarInStack[setAttr.Instance.Value] + "($fp) \n";

            if (int.TryParse(setAttr.Value, out int n))
            {
                Text += "\t li $a0, " + n + " \n";
            }
            else
            {
                Text += "\t lw $a0, " + CurrentScope.VarInStack[setAttr.Value] + "($fp) \n";
            }
            
            Text += "\t sw $a0, " + pos + "($v0) \n";
        }

        public void Accept(CIL_VCall vCall)
        {
            int i = -12;

            foreach (var arg in vCall.Args)
            {
                if (int.TryParse(arg, out int n))
                {
                    Text += "\t li $a0, " + n + "\n" +
                            "\t sw $a0, " + i + "($sp) \n";
                }
                else
                {
                    Text += "\t lw $a0, " + CurrentScope.VarInStack[arg] + "($fp) \n" +
                            "\t sw $a0, " + i + "($sp) \n";
                }

                i -= 4;
            }

            var attr = Clases[vCall.MyType].GetMethod(vCall.Name);
            var indx = Clases[vCall.MyType].AllMethods().IndexOf(attr);

            Text += "\t la $a0, vt." + vCall.MyType + "\n" +
                    "\t lw $a1, " + (4 * (indx + 1)) + "($a0) \n" +
                    "\t jal $a1 \n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[vCall.Dest] + "($fp) \n";
        }

        public void Accept(CIL_Allocate allocate)
        {
            Text += "\t jal " + allocate.MyType + ".constructor\n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[allocate.Dest] + "($fp) \n";
        }

        public void Accept(CIL_Call call)
        {
            Text += "\t lw $a0, " + CurrentScope.VarInStack[call.This] + "($fp) \n" +
                    "\t sw $a0, -12($sp) \n";
            
            int i = -16;
            
            foreach (var arg in call.Args)
            {
                if (int.TryParse(arg, out int n))
                {
                    Text += "\t li $a0, " + n + "\n" +
                            "\t sw $a0, " + i + "($sp) \n";
                }
                else
                {
                    Text += "\t lw $a0, " + CurrentScope.VarInStack[arg] + "($fp) \n" +
                            "\t sw $a0, " + i + "($sp) \n";
                }

                i -= 4;
            }

            var indx = VT[call.Name];

            Text += "\t lw $a0, " + CurrentScope.VarInStack[call.This] + "($fp) \n" + // carga la instancia de la clase
                    "\t lw $a1, 8($a0) \n" + // carga la referencia a la vt de la clase
                    "\t lw $a2, " + (4 * indx) + "($a1) \n" + // carga la referencia al metodo que quiere llamar
                    "\t jal $a2 \n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[call.Dest] + "($fp) \n";
        }

        public void Accept(CIL_Load load)
        {
            Text += "\t la $t0, " + load.Msg + "\n" +
                    "\t sw $t0, " + CurrentScope.VarInStack[load.Dest] + "($fp) \n";
        }

        public void Accept(CIL_Length length)
        {
            // -12($sp) = string a
            Text += "\t la $a0, " + length.Msg + "\n" +
                    "\t sw $a0, -12($sp) \n" +
                    "\t jal _stringlength \n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[length.Dest] + "($sp) \n";
        }

        public void Accept(CIL_Str str)
        {
            throw new NotImplementedException();
        }

        public void Accept(CIL_Label label)
        {
            Text += "$" + label._label + ": \n";
        }

        public void Accept(CIL_Goto _goto)
        {
            Text += "\t j $" + _goto._label + "\n";
        }

        public void Accept(CIL_Return _return)
        {
            if (_return.value == "" || _return.value == "0")
            {}
            else if (int.TryParse(_return.value, out int n))
            {
                Text += "\t li $v0, " + n + "\n";
            }
            else
            {
                Text += "\t lw $v0, " + CurrentScope.VarInStack[_return.value] + "($fp) \n";
            }
        }

        public void Accept(CIL_Read_Int read)
        {
            Text += "\t li $v0, 5 \n" +
                    "\t syscall \n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[read._var] + "($fp) \n";
        }

        public void Accept(CIL_Read_Str read)
        {
            Text += "\t jal _in_string \n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[read._var] + "($fp) \n";
        }

        public void Accept(CIL_Print_Int print)
        {
            Text += "\t lw $a0, " + CurrentScope.VarInStack[print._var] + "($fp) \n" +
                    "\t li $v0, 1 \n" +
                    "\t syscall \n";
        }

        public void Accept(CIL_Print_Str print)
        {
            Text += "\t lw $a0, " + CurrentScope.VarInStack[print._var] + "($fp) \n" +
                    "\t li $v0, 4 \n" +
                    "\t syscall \n";
        }

        public void Accept(CIL_ConditionalJump cj)
        {
            Text += "\t lw $t0, " + CurrentScope.VarInStack[cj.cond] + "($fp) \n" +
                    "\t bne $t0, $zero, $" + cj.label + "\n";
        }

        public void Accept(CIL_TypeName tn)
        {
            Text += "\t lw $a0, -12($fp) \n" +
                    "\t lw $a1, 0($a0) \n" +
                    "\t sw $a1, 0($sp) \n" +
                    "\t lw $v0, 0($sp) \n" +
                    "\t jr $ra \n";
        }

        public void Accept(CIL_Abort abort)
        {
            Text += "\t li $v0, 10 \n" +
                    "\t syscall \n";
        }
        
        public void Accept(CIL_Copy copy)
        {
            Text += "\t lw $a0, " + CurrentScope.VarInStack[copy.Instance] + "($fp) \n" +
                    "\t sw $a0, -12($sp) \n" +
                    "\t lw $a1, 0($a0) \n" +
                    "\t sw $a1, -16($sp) \n" +
                    "\t jal _copy \n" +
                    "\t sw $v0, " + CurrentScope.VarInStack[copy.Dest] + "($fp) \n";
        }

        public void Accept(CIL_Typeof tp)
        {
            Text += "\t lw $a0, " + CurrentScope.VarInStack[tp.expr] + "($fp) # carga instancia \n" +
                    "\t lw $a1, 8($a0) # carga el vt de la instancia \n" +
                    "\t sw $a1, " + CurrentScope.VarInStack[tp.dest] + "($fp) # devuelve una referencia al vt en la variable dest \n";
        }

        public void Accept(CIL_Father father)
        {
            Text += "\t lw $a0, " + CurrentScope.VarInStack[father.instans] + "($fp) \n" +
                    "\t lw $a1, 0($a0) \n" +
                    "\t sw $a1, " + CurrentScope.VarInStack[father.dest] + "($fp) \n";
        }

        public void Accept(CIL_UnaryExpr uExpr)
        {
            string expr = "";
            
            if (int.TryParse(uExpr.expr, out int n1))
                expr = "\t li $t1, " + n1 + "\n";
            else
                expr = "\t lw $t1, " + CurrentScope.VarInStack[uExpr.expr] + "($fp) \n";

            string operation = "";
            var num = GetNum();
            switch (uExpr.op)
            {
                    case "~":
                        operation += "\t li $t2, 0 \n" + 
                                     "\t nor $t0, $t1, $t2 \n";
                        break;
                    case "not": //booleano
                        operation += "\t beq $t1, $zero, $false" + num + "\n" +
                                     "\t la $t0, 0 \n" +
                                     "\t j $end" + num + "\n" +
                                     "\n$false" + num + ": \n" +
                                     "\t la $t0, 1 \n" +
                                     "\n$end" + num + ":\n\n";
                        break;
                    default:
                        throw new InvalidOperationException("Operacion " + uExpr.op + " no definida");
            }

            Text += expr +
                    operation +
                    "\t sw $t0, " + CurrentScope.VarInStack[uExpr.dest] + "($fp) \n";
        }

        public void Accept(CIL_is_void isVoid)
        {
            if (int.TryParse(isVoid.arg, out int n))
            {
                Text += "\t la $a0, " + n + "\n";
            }
            else
            {
                Text += "\t lw $a0, " + CurrentScope.VarInStack[isVoid.arg] + "($fp) \n";
            }

            var num = GetNum();

            Text += "\t beq $a0, $zero, $true" + num + "\n" +
                    "\t la $t0, 0 \n" +
                    "\t j $end" + num + "\n" +
                    "\n$true" + num + ": \n" +
                    "\t la $t0, 1 \n" +
                    "\n$end" + num + ":\n\n" +
                    "\t sw $t0, " + CurrentScope.VarInStack[isVoid.ret] + "($fp) \n";
        }
    }
}