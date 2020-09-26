grammar Combined1;


/*
*opciones de compilacion de la gramatica
*/
options {							
    language=CSharp2;								//lenguaje objetivo de la gramatica
}

/*
*	Reglas del Parser
*/
programa returns[float value]							//el programa retornara un valor entero.
	: stat{System.Console.WriteLine($stat.value);}	//se imprime el valor que calculo el parser.
	;

stat returns[float value]								//la expresion retornara un valor entero al programa.
	:
	c = expresion NEWLINE	{System.Console.WriteLine($c.value);}	//Se imprime el valor adquirido.
|NEWLINE;			//no se hace nada.

expresion returns[float value]						//El valor calculado por la expresion sera regresado como un entero.
	:	
	a = multiplicacion{$value = $a.value;} (		//Se asina el valor que se retornara en la regla.
	MAS b = multiplicacion {$value =$value + $b.value;}				//El valor se suma con el actual en la expresion.
	|
	MENOS b = multiplicacion{$value =$value- $b.value;})*{System.Console.WriteLine($value);}	//El valor se resta con el actual y se imprime el valor.
	;
	
multiplicacion returns[float value]					//La regla retorna un entero.
	:	
	a = numero{$value = $a.value;}  (				//Se asigna el valor que se regresara.
	POR b = numero{$value =$value* $b.value;}		//Se calcula la multiplicacion 
	|
	ENTRE b = numero{$value =$value/ $b.value;})*	//Se calcula la division.
	;
numero returns[float value]							//La regla retonara un entero.
	:	
	INT	{$value = float.Parse($INT.text);}			//se convierte a entero la cadena de entrada de la consola.			
	|	
	PARENI expresion PAREND		{$value = $expresion.value;}		//se asigna el valor de la expresion dentro del parentesis.
	;

/*
*	Reglas del Lexer.
*/
PARENI
	:	'('		//token de parentesis derecho
	;
PAREND
	:	')'		//token de parentesis izquierdo.
	;
MAS 
	: '+'		//token de signo mas
	;
MENOS 
	: '-'		//token de signo menos
	;
POR
	: '*'		//token de signo por
	;
INT
	:('0'..'9')+	//tokens validos para numeros
	;
ENTRE
	: '/'		//token de signo entre
	;

NEWLINE			//token para identificar el final de la expresion.
	: '\n'
	;
WS
	: (' '|'\r'|'\n'|'\t')+ {Skip();}	//tokens que identifican las secuencas de escape.
	;
