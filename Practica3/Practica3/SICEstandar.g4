grammar SICEstandar;

/*Division*/
prog : (inicio| medio| final);


/*Inicio*/
inicio : ETIQ? 'START' DIRECCION;

/*Medio*/
medio : (instruccion | directiva)+;
instruccion :  ETIQ? CODOP ETIQ INDICE? | 'RSUB' ;
directiva :   ETIQ? 'BYTE' BCHAR	 |ETIQ? 'BYTE' BHEX |ETIQ? 'WORD' DIRECCION	 |ETIQ? 'RESW' DIRECCION |ETIQ? 'RESB' DIRECCION;	

/*Final*/
final : 'END' ETIQ?;

/* Terminales*/

DIRECCION:		(NUMDEC|NUMHEX('H'|'h'));

/*Conjunto de instrucciones*/
CODOP : ('ADD'|'AND'|'COMP'|'DIV'|'J'|'JEQ'|'JGT'|'JLT'|'JSUB'|'LDA'|'LDCH'|'LDL''LDX'|'MUL'|'OR'|'RD'|'STA'|'STCH'|'STL'|'STSW'|'STX'|'SUB'|'TD'|'TIX'|'WD');

/*Valores*/
ETIQ:[A-Z|a-z|0-9]+ ;
NUMDEC:	[0-9]+;
NUMHEX:	[0-9A-F]+;
BHEX:	('X'|'x')'\''('a'..'f'|'A'..'F'|'0'..'9')+ '\'';
BCHAR:	('C'|'c')'\''('A'..'Z'|'a'..'z'|'0'..'9')+ '\'';

/*SALTO DE LINEA*/
FINL:	('\n' | '\r')+;

/*Formas de indice*/
INDICE:',x'|', x'|',X'|', X';
/*Quita espacions*/
WHITESPACE:			(' '|'\t')+ -> skip;
