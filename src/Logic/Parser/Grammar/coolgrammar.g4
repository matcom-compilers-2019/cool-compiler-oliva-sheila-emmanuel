
grammar coolgrammar;

program : (class ';')+;

class	: CLASS TYPE ( INHERITS TYPE)? '{' (feature ';')* '}' ;

feature : attr											# f_attr
		| method										# f_method						
		;

method  : ID '(' args_def ')' ':' TYPE '{' expr '}' ;						

attr    : formal ('<-' expr)? ;																							

formal  : ID ':' TYPE ;

expr    : expr ('@' TYPE)? '.' ID '(' args_call ')'								# dispatch
		| ID '(' args_call ')'												    # call_method
		| IF expr THEN expr FI													# if_then
		| IF expr THEN expr ELSE expr FI				                        # if_else
		| WHILE expr LOOP expr POOL						                        # while
		| '{' expr_list '}'								                        # body
		| CASE expr OF (branch ';')+ ESAC										# case_of 
		| NEW TYPE										                        # new_type
		| op = '~' expr															# neg_bit
		| ISVOID expr									                        # isvoid
		| expr op = ('*' | '/') expr					                        # multdiv
        | expr op = ('+' | '-') expr 				                            # sumaresta
        | expr op = ('<' | '<=' | '=') expr				                        # comp
        | NOT expr											                    # not_exp
        | '(' expr ')'									                        # parentesis
		| INTEGER										                        # int
		| cons = (TRUE | FALSE)						                            # bool
		| STR																	# string
		| ID '<-' expr								                            # assign
		| LET attr (',' attr )* IN expr										    # let
		| ID											                        # id
		;


branch  : formal '=>' expr ;
expr_list : (expr ';')+ ;
args_def  : ( formal(',' formal)*)? ;	
args_call : (expr (','expr)* )?;

STR : '"' (ESC | ~ ["\\])* '"';


CLASS : 'class';
INHERITS : 'inherits';
LET : 'let';
IN : 'in';
WS : [ \t\n\r]+ -> skip;
IF : 'if' ;
THEN : 'then';
ELSE : 'else';
FI : 'fi';
WHILE : 'while';
LOOP : 'loop';
POOL : 'pool';
NEW : 'new';
TYPE : [A-Z][_0-9A-Za-z]*;														
ISVOID : 'isvoid';
INTEGER : [0-9]+;
TRUE : 'true';
FALSE : 'false';
NOT : 'not';
CASE : 'case';
OF : 'of';
ESAC : 'esac';

ID : [a-z][_0-9A-Za-z]*;

fragment ESC : '\\' (["\\/bfnrt] | UNICODE);

fragment UNICODE : 'u' HEX HEX HEX HEX;

fragment HEX : [0-9a-fA-F];