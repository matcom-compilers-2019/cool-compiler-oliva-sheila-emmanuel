namespace CIL
{
    public class PlantillaMIPS
    {
        // -12($sp) = string a
        public const string StringLength = 
"_stringlength:\n\n" +

    "\tsw $ra, -4($sp)\n" +
    "\tsw $fp, -8($sp)\n" +
    "\tsubu $sp, $sp, 12\n" +
    "\taddu $fp, $sp, 12\n\n" +

    "\tlw $a0, -12($fp)\n" +
    "\tli $v0, 0\n" +
"_stringlength.loop:\n" +
    "\tlb $a1, 0($a0)\n" +
    "\tbeqz $a1, _stringlength.end\n" +
    "\taddiu $a0, $a0, 1\n" +
    "\taddiu $v0, $v0, 1\n" +
    "\tj _stringlength.loop\n" +
"_stringlength.end:\n\n" +

    "\tlw $ra, -4($fp)\n" +
    "\tlw $fp, -8($fp)\n" +
    "\taddu $sp, $sp, 12\n\n" +

    "\tjr $ra\n";

        // -12($sp) = string a, -16($sp) = string b
        public const string StringConcat =
@"
_stringconcat:

    sw $ra, -4($sp)
    sw $fp, -8($sp)
    subu $sp, $sp, 16
    addu $fp, $sp, 16

    lw $a0, -12($fp)
    sw $a0, -12($sp)
    jal _stringlength
    move $v1, $v0

    lw $a0, -16($fp)
    sw $a0, -16($sp)
    jal _stringlength
    add $v1, $v1, $v0
    addi $v1, $v1, 1
    li $v0, 9
    move $a0, $v1
    syscall
    move $v1, $v0
    lw $a0, -12($fp)
_stringconcat.loop1:
    lb $a1, 0($a0)
    beqz $a1, _stringconcat.end1
    sb $a1, 0($v1)
    addiu $a0, $a0, 1
    addiu $v1, $v1, 1
    j _stringconcat.loop1
_stringconcat.end1:
    lw $a0, -16($fp)
_stringconcat.loop2:
    lb $a1, 0($a0)
    beqz $a1, _stringconcat.end2
    sb $a1, 0($v1)
    addiu $a0, $a0, 1
    addiu $v1, $v1, 1
    j _stringconcat.loop2
_stringconcat.end2:
    sb $zero, 0($v1)

    lw $ra, -4($fp)
    lw $fp, -8($fp)
    addu $sp, $sp, 16

    jr $ra
";

        // -12($sp) = msg, -16($sp) = int a, -20($sp) = int b
        public const string StringSubstring =
@"
_stringsubstr:

    sw $ra, -4($sp)
    sw $fp, -8($sp)
    subu $sp, $sp, 20
    addu $fp, $sp, 20

    lw $a0, -20($sp)
    addiu $a0, $a0, 1
    li $v0, 9
    syscall
    move $v1, $v0
    lw $a0, -12($sp)
    lw $a1, -16($sp)
    add $a0, $a0, $a1
    lw $a2, -20($sp)
_stringsubstr.loop:
    beqz $a2, _stringsubstr.end
    lb $a1, 0($a0)
    beqz $a1, _substrexception
    sb $a1, 0($v1)
    addiu $a0, $a0, 1
    addiu $v1, $v1, 1
    addiu $a2, $a2, -1
    j _stringsubstr.loop
_stringsubstr.end:
    sb $zero, 0($v1)

    lw $ra, -4($fp)
    lw $fp, -8($fp)
    addu $sp, $sp, 20

    jr $ra
            
_substrexception:
    la $a0, strsubstrexception
    li $v0, 4
    syscall
    li $v0, 10
    syscall
";

        public const string InputString = 
@"
_in_string:

    sw $ra, -4($sp)
    sw $fp, -8($sp)
    subu $sp, $sp, 8
    addu $fp, $sp, 8

    la $a0, buffer
    li $a1, 65536
    li $v0, 8
    syscall

    sw $a0, -12($sp)
    jal _stringlength

    move $a2, $v0
    addiu $a2, $a2, -1
    move $a0, $v0

    li $v0, 9
    syscall

    move $v1, $v0
    la $a0, buffer
_in_string.loop:
    beqz $a2, _in_string.end
    lb $a1, 0($a0)
    sb $a1, 0($v1)
    addiu $a0, $a0, 1
    addiu $v1, $v1, 1
    addiu $a2, $a2, -1
    j _in_string.loop
_in_string.end:
    sb $zero, 0($v1)

    lw $ra, -4($fp)
    lw $fp, -8($fp)
    addu $sp, $sp, 8

    jr $ra
";

        public const string Copy =
@"
_copy:

    sw $ra, -4($sp)
    sw $fp, -8($sp)
    subu $sp, $sp, 16
    addu $fp, $sp, 16

    lw $a1, -12($fp)
    lw $a0, -16($fp)
    li $v0, 9
    syscall
    lw $a0, -16($fp)
    move $a3, $v0
_copy.loop:
    lw $a2, 0($a1)
    sw $a2, 0($a3)
    addiu $a0, $a0, -1
    addiu $a1, $a1, 4
    addiu $a3, $a3, 4
    beq $a0, $zero, _copy.end
    j _copy.loop
_copy.end:
    move $v0, $a3

    lw $ra, -4($fp)
    lw $fp, -8($fp)
    addu $sp, $sp, 8

    jr $ra
";
    }
}