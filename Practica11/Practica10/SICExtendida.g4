grammar SICExtendida;

prog : (inicio| medio| final);

inicio : ETIQ? 'START' DIRECCION;
medio : (intruccion | reserva | nocorta)+;
intruccion : (formatouno | formatodos | formatotres | formatocuatro);
formatotres :  simpletres |  otrostres | subtres;
simpletres : ETIQ? FORMATOTRES (ETIQ | DIRECCION) INDICE?;
otrostres : ETIQ? FORMATOTRES DIRECCIONAMIENTO (ETIQ | DIRECCION);
subtres : 'RSUB';
formatocuatro : simplecuatro  | otroscuatro | subcuatro;
simplecuatro : ETIQ? '+'FORMATOTRES (ETIQ | DIRECCION) INDICE?;
otroscuatro : ETIQ? '+'FORMATOTRES DIRECCIONAMIENTO (ETIQ | DIRECCION);
subcuatro : '+RSUB';
nocorta : ETIQ? 'BASE' ETIQ;
formatouno : ETIQ? FORMATOUNO;
formatodos : ETIQ? (FORMATODOSUNREGISTRO REGISTROS 
			| FORMATODOSDOSREGISTRO REGISTROS (',A'|',X'|',L'|',B'|',S'|',T'|',F')
			| FORMATODOSNUMERO REGISTROS (',1'|',2'|',3'|',4'|',5'|',6'|',7'|',8'|',9'|',10'|',11'|',12'|',13'|',14'|',15'|',16')
			| 'SVC' NUMREG);
reserva  :   ETIQ? 'BYTE' BCHAR	
			|ETIQ? 'BYTE' BHEX
			|ETIQ? 'WORD' DIRECCION
			|ETIQ? 'RESW' DIRECCION
			|ETIQ? 'RESB' DIRECCION;	
final : 'END' ETIQ?;


DIRECCION:		(NUMDEC|NUMHEX('H'|'h'));
REGISTROS: ('A'|'X'|'L'|'B'|'S'|'T'|'F');
FORMATOUNO: ('FIX'|'FLOAT'|'HIO'|'NORM'|'SIO'|'TIO');
FORMATODOSUNREGISTRO: ('CLEAR'|'TIXR');
FORMATODOSDOSREGISTRO: ('COMPR'|'ADDR'|'DIVR'|'MULR'|'RMO'|'SUBR');
FORMATODOSNUMERO: ('SHIFTL'|'SHIFTR');
FORMATOTRES: ('ADD'|'ADDF'|'AND'|'COMP'|'COMPF'|'DIV'|'DIVF'|'J'|'JEQ'|'JGT'|'JLT'|'JSUB'|'LDA'|'LDB'|'LDCH'|'LDF'|'LDL'|'LDS'|'LDT'|'LDX'|'LPS'|'MUL'|'MULF'|'OR'|'RD'|'SSK'|'STA'|'STB'|'STCH'|'STF'|'STI'|'STL'|'STS'|'STSW'|'STT'|'STX'|'SUB'|'SUBF'|'TD'|'TIX'|'WD');
ETIQ:[A-Z|a-z|0-9]+;
DIRECCIONAMIENTO: ('@'|'#');
NUMREG: ('1'|'2'|'3'|'4'|'5'|'6'|'7'|'8'|'9'|'10'|'11'|'12'|'13'|'14'|'15'|'16');
NUMDEC:	[0-9]+;
NUMHEX:	[0-9A-F]+;
BHEX:	('X'|'x')'\''('a'..'f'|'A'..'F'|'0'..'9')+ '\'';
BCHAR:	('C'|'c')'\''('A'..'Z'|'a'..'z'|'0'..'9')+ '\'';
SALTO:	('\n' | '\r')+;
INDICE:',x'|', x'|',X'|', X';
WS:			(' '|'\t')+ -> skip;