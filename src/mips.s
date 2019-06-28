.data
vt.Object: .word 0, __init_Object, abort_Object, type_name_Object, copy_Object, 0 
str1: .asciiz "Object"
vt.SELF_TYPE: .word vt.Object, __init_SELF_TYPE, abort_Object, type_name_Object, copy_Object, 0 
str2: .asciiz "SELF_TYPE"
vt.Int: .word vt.Object, __init_Int, abort_Object, type_name_Object, copy_Object, 0 
str3: .asciiz "Int"
vt.String: .word vt.Object, __init_String, abort_Object, type_name_Object, copy_Object, length_String, concat_String, substr_String, 0 
str4: .asciiz "String"
vt.Bool: .word vt.Object, __init_Bool, abort_Object, type_name_Object, copy_Object, 0 
str5: .asciiz "Bool"
vt.IO: .word vt.Object, __init_IO, abort_Object, type_name_Object, copy_Object, out_string_IO, out_int_IO, in_string_IO, in_int_IO, 0 
str6: .asciiz "IO"
vt.Main: .word vt.IO, __init_Main, abort_Object, type_name_Object, copy_Object, out_string_IO, out_int_IO, in_string_IO, in_int_IO, __distans_Main, main_Main, 0 
str7: .asciiz "Main"
vt.A: .word vt.B, __init_A, abort_Object, type_name_Object, copy_Object, 0 
str8: .asciiz "A"
vt.B: .word vt.Object, __init_B, abort_Object, type_name_Object, copy_Object, 0 
str9: .asciiz "B"

buffer:             .space    65536 
zero:               .byte     0 
strsubstrexception: .asciiz "Substring index exception" 
divisionZero:       .asciiz "Division by Zero Exception" 

vacio:	 .asciiz 	""

.globl main
.text
_stringlength:

	sw $ra, -4($sp)
	sw $fp, -8($sp)
	subu $sp, $sp, 12
	addu $fp, $sp, 12

	lw $a0, -12($fp)
	li $v0, 0
_stringlength.loop:
	lb $a1, 0($a0)
	beqz $a1, _stringlength.end
	addiu $a0, $a0, 1
	addiu $v0, $v0, 1
	j _stringlength.loop
_stringlength.end:

	lw $ra, -4($fp)
	lw $fp, -8($fp)
	addu $sp, $sp, 12

	jr $ra

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
    addiu $a0, $a0, -4
    addiu $a1, $a1, 4
    addiu $a3, $a3, 4
    beq $a0, $zero, _copy.end
    j _copy.loop
_copy.end:
    lw $ra, -4($fp)
    lw $fp, -8($fp)
    addu $sp, $sp, 8

    jr $ra


_zeroException: 
	 li $v0, 4 
	 la $a0, divisionZero 
	 syscall 
	 li $v0, 10 
	 syscall 

Object.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 12
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str1
	 sw $a0, 4($v0) 
	 la $a0, vt.Object
	 sw $a0, 8($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
SELF_TYPE.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 12
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str2
	 sw $a0, 4($v0) 
	 la $a0, vt.SELF_TYPE
	 sw $a0, 8($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
Int.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 12
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str3
	 sw $a0, 4($v0) 
	 la $a0, vt.Int
	 sw $a0, 8($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
String.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 12
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str4
	 sw $a0, 4($v0) 
	 la $a0, vt.String
	 sw $a0, 8($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
Bool.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 12
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str5
	 sw $a0, 4($v0) 
	 la $a0, vt.Bool
	 sw $a0, 8($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
IO.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 12
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str6
	 sw $a0, 4($v0) 
	 la $a0, vt.IO
	 sw $a0, 8($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
Main.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 16
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str7
	 sw $a0, 4($v0) 
	 la $a0, vt.Main
	 sw $a0, 8($v0) 
	 li $a0, 0 
	 sw $a0, 12($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
A.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 12
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str8
	 sw $a0, 4($v0) 
	 la $a0, vt.A
	 sw $a0, 8($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
B.constructor:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 li $v0, 9 
	 li $a0, 12
	 syscall 
	 sw $a0, 0($v0) 
	 la $a0, str9
	 sw $a0, 4($v0) 
	 la $a0, vt.B
	 sw $a0, 8($v0) 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
__distans_Main:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 36
	 addu $fp, $sp, 36
	 jal Object.constructor
	 sw $v0, -24($fp) 
	 lw $a0, -24($fp) # carga instancia 
	 lw $a1, 8($a0) # carga el vt de la instancia 
	 sw $a1, -36($fp) # devuelve una referencia al vt en la variable dest 
	 li $a0, 0
	 sw $a0, -28($fp) 
$for: 
	 lw $t1, -16($fp) 
	 lw $t2, -20($fp) 
	 beq $t1, $t2, $true10 
	 li $t0, 0 
	 j $end10 

$true10: 
	 li $t0, 1 

$end10:

	 sw $t0, -32($fp) 
	 lw $t0, -32($fp) 
	 bne $t0, $zero, $finish
	 lw $t1, -20($fp) 
	 lw $t2, -36($fp) 
	 beq $t1, $t2, $true11 
	 li $t0, 0 
	 j $end11 

$true11: 
	 li $t0, 1 

$end11:

	 sw $t0, -32($fp) 
	 lw $t0, -32($fp) 
	 bne $t0, $zero, $not_father
	 lw $a0, -20($fp) 
	 lw $a1, 0($a0) 
	 sw $a1, -40($fp) 
	 lw $a0, -40($fp) 
	 sw $a0, -20($fp) 
	 lw $t1, -28($fp) 
	 li $t2, 1
	 add $t0, $t1, $t2 
	 sw $t0, -28($fp) 
	 j $for
$not_father: 
	 li $a0, 9999
	 sw $a0, -28($fp) 
$finish: 
	 lw $v0, -28($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 36
	 j $ra 
#########################################################################
abort_Object:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 8
	 addu $fp, $sp, 8
	 li $v0, 10 
	 syscall 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 8
	 j $ra 
#########################################################################
concat_String:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 16
	 addu $fp, $sp, 16
	 la $a0, this
	 sw $a0, -12($sp) 
	 la $a0, s
	 sw $a0, -16($sp) 
	 jal _stringconcat 
	 sw $v0, -20($sp) 
	 lw $v0, -20($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 16
	 j $ra 
#########################################################################
copy_Object:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12
	 lw $a0, -12($fp) 
	 sw $a0, -12($sp) 
	 lw $a1, 0($a0) 
	 sw $a1, -16($sp) 
	 jal _copy 
	 sw $v0, -16($fp) 
	 lw $v0, -16($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
in_int_IO:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 8
	 addu $fp, $sp, 8
	 li $v0, 5 
	 syscall 
	 sw $v0, -12($fp) 
	 lw $v0, -12($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 8
	 j $ra 
#########################################################################
in_string_IO:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 8
	 addu $fp, $sp, 8
	 jal _in_string 
	 sw $v0, -12($fp) 
	 lw $v0, -12($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 8
	 j $ra 
#########################################################################
length_String:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12
	 la $a0, this
	 sw $a0, -12($sp) 
	 jal _stringlength 
	 sw $v0, -16($sp) 
	 lw $v0, -16($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
out_int_IO:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12
	 lw $a0, -16($fp) 
	 li $v0, 1 
	 syscall 
	 lw $v0, -12($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
out_string_IO:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12
	 lw $a0, -16($fp) 
	 li $v0, 4 
	 syscall 
	 lw $v0, -12($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
substr_String:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 20
	 addu $fp, $sp, 20
	 la $a0, this
	 sw $a0, -12($sp) 
	 lw $a0, -16($fp) 
	 sw $a0, -16($sp) 
	 lw $a0, -20($fp) 
	 sw $a0, -20($sp) 
	 jal _stringsubstr 
	 sw $v0, -24($sp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 20
	 j $ra 
#########################################################################
type_name_Object:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12
	 lw $v0, -16($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
__init_Main:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 8
	 addu $fp, $sp, 8
	 lw $v0, -12($fp) 
	 li $a0, 0 
	 sw $a0, 12($v0) 
	 lw $v0, -12($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 8
	 j $ra 
#########################################################################
main_Main:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 76
	 addu $fp, $sp, 76
	 jal A.constructor
	 sw $v0, -16($fp) 
	 lw $a0, -16($fp) 
	 sw $a0, -12($sp) 
	 la $a0, vt.A
	 lw $a1, 4($a0) 
	 jal $a1 
	 sw $v0, -16($fp) 
	 lw $a0, -16($fp) 
	 sw $a0, -20($fp) 
	 lw $a0, -20($fp) # carga instancia 
	 lw $a1, 8($a0) # carga el vt de la instancia 
	 sw $a1, -28($fp) # devuelve una referencia al vt en la variable dest 
	 li $a0, 9999
	 sw $a0, -32($fp) 
	 jal Object.constructor
	 sw $v0, -44($fp) 
	 lw $a0, -44($fp) # carga instancia 
	 lw $a1, 8($a0) # carga el vt de la instancia 
	 sw $a1, -40($fp) # devuelve una referencia al vt en la variable dest 
	 lw $a0, -12($fp) 
	 sw $a0, -12($sp) 
	 lw $a0, -40($fp) 
	 sw $a0, -16($sp) 
	 lw $a0, -28($fp) 
	 sw $a0, -20($sp) 
	 lw $a0, -12($fp) 
	 lw $a1, 8($a0) 
	 lw $a2, 36($a1) 
	 jal $a2 
	 sw $v0, -48($fp) 
	 lw $t1, -32($fp) 
	 lw $t2, -48($fp) 
	 sub $t0, $t1, $t2 
	 bltz $t0, $true13 
	 li $t0, 0 
	 j $end13 

$true13: 
	 li $t0, 1 

$end13:

	 sw $t0, -36($fp) 
	 lw $t0, -36($fp) 
	 bne $t0, $zero, $let_if10
	 lw $a0, -48($fp) 
	 sw $a0, -32($fp) 
$let_if10: 
	 jal B.constructor
	 sw $v0, -56($fp) 
	 lw $a0, -56($fp) # carga instancia 
	 lw $a1, 8($a0) # carga el vt de la instancia 
	 sw $a1, -52($fp) # devuelve una referencia al vt en la variable dest 
	 lw $a0, -12($fp) 
	 sw $a0, -12($sp) 
	 lw $a0, -52($fp) 
	 sw $a0, -16($sp) 
	 lw $a0, -28($fp) 
	 sw $a0, -20($sp) 
	 lw $a0, -12($fp) 
	 lw $a1, 8($a0) 
	 lw $a2, 36($a1) 
	 jal $a2 
	 sw $v0, -60($fp) 
	 lw $t1, -32($fp) 
	 lw $t2, -60($fp) 
	 sub $t0, $t1, $t2 
	 bltz $t0, $true14 
	 li $t0, 0 
	 j $end14 

$true14: 
	 li $t0, 1 

$end14:

	 sw $t0, -36($fp) 
	 lw $t0, -36($fp) 
	 bne $t0, $zero, $let_if15
	 lw $a0, -60($fp) 
	 sw $a0, -32($fp) 
$let_if15: 
	 lw $t1, -32($fp) 
	 lw $t2, -48($fp) 
	 beq $t1, $t2, $true15 
	 li $t0, 0 
	 j $end15 

$true15: 
	 li $t0, 1 

$end15:

	 sw $t0, -36($fp) 
	 lw $t0, -36($fp) 
	 bne $t0, $zero, $let_label6
	 lw $t1, -32($fp) 
	 lw $t2, -60($fp) 
	 beq $t1, $t2, $true16 
	 li $t0, 0 
	 j $end16 

$true16: 
	 li $t0, 1 

$end16:

	 sw $t0, -36($fp) 
	 lw $t0, -36($fp) 
	 bne $t0, $zero, $let_label11
$let_label6: 
	 li $a0, 1
	 sw $a0, -24($fp) 
	 j $let_finish5
$let_label11: 
	 li $a0, 2
	 sw $a0, -24($fp) 
	 j $let_finish5
$let_finish5: 
	 lw $v0, -12($fp) 
	 lw $a0, -24($fp) 
	 sw $a0, 12($v0) 
	 lw $a0, -24($fp) 
	 sw $a0, -72($fp) 
	 lw $v0, -12($fp) 
	 lw $a0, 12($v0) 
	 sw $a0, -76($fp) 
	 lw $a0, -12($fp) 
	 sw $a0, -12($sp) 
	 lw $a0, -76($fp) 
	 sw $a0, -16($sp) 
	 lw $a0, -12($fp) 
	 lw $a1, 8($a0) 
	 lw $a2, 24($a1) 
	 jal $a2 
	 sw $v0, -80($fp) 
	 lw $v0, -80($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 76
	 j $ra 
#########################################################################
__init_A:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12
	 jal B.constructor
	 sw $v0, -16($fp) 
	 lw $a0, -16($fp) 
	 sw $a0, -12($sp) 
	 la $a0, vt.B
	 lw $a1, 4($a0) 
	 jal $a1 
	 sw $v0, -16($fp) 
	 lw $v0, -12($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 j $ra 
#########################################################################
__init_B:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 8
	 addu $fp, $sp, 8
	 lw $v0, -12($fp) 
	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 8
	 j $ra 
#########################################################################

main:
	 sw $zero, 0($sp) 
	 sw $ra, -4($sp) 
	 sw $fp, -8($sp) 
	 subu $sp, $sp, 12
	 addu $fp, $sp, 12

	 jal Main.constructor 
	 sw $v0, -12($fp) 
	 sw $v0, -12($sp) 
	 jal __init_Main 
	 sw $v0, -12($fp) 
	 sw $v0, -12($sp) 
	 jal main_Main 

	 lw $ra, -4($fp) 
	 lw $fp, -8($fp) 
	 addu $sp, $sp, 12
	 jr $ra 


