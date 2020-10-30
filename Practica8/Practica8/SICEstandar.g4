grammar SICEstandar;


//TIPOS
prog : (inicio| medio| final);
//PRIMERO
inicio : ETIQ? 'START' DIRECCION;
//INTERMEDIO
medio : (instruccion | directiva)+;
instruccion :  ETIQ? CODOP ETIQ INDICE? | 'RSUB' ;
directiva :   ETIQ? 'BYTE' BCHAR	 |ETIQ? 'BYTE' BHEX |ETIQ? 'WORD' DIRECCION	 |ETIQ? 'RESW' DIRECCION |ETIQ? 'RESB' DIRECCION;	
//FINAL
final : 'END' ETIQ?;
//INSTRUCCIONES
CODOP : ('ADD'|'AND'|'COMP'|'DIV'|'J'|'JEQ'|'JGT'|'JLT'|'JSUB'|'LDA'|'LDCH'|'LDL'|'LDX'|'MUL'|'OR'|'RD'|'STA'|'STCH'|'STL'|'STSW'|'STX'|'SUB'|'TD'|'TIX'|'WD');
//TERMINALES
DIRECCION:		(NUMDEC|NUMHEX('H'|'h'));
//SALTO DE LINEA
FINL:	('\n' | '\r')+;
//INDICE
INDICE:',x'|', x'|',X'|', X';
//ESPACIOS
WHITESPACE:			(' '|'\t')+ -> skip;
//VALORES
ETIQ:[A-Z|a-z|0-9]+ ;
NUMDEC:	[0-9]+;
NUMHEX:	[0-9A-F]+;
BHEX:	('X'|'x')'\''('a'..'f'|'A'..'F'|'0'..'9')+ '\'';
BCHAR:	('C'|'c')'\''('A'..'Z'|'a'..'z'|'0'..'9')+ '\'';