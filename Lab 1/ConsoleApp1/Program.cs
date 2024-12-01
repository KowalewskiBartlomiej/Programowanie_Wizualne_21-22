using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*using System;
using Laboratorium_1_3;

/*while(true)
{
    Console.SetCursorPosition(110, 0);
    Console.WriteLine("{0}:{1}:{2}", 14-System.DateTime.Now.Hour, 60-System.DateTime.Now.Minute, 60-System.DateTime.Now.Second);
}

Minutnik minutnik = new Minutnik(new TimeSpan(15, 0, 0));*/

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


int[] liczby = new int[(int)10e6];
//object[] liczby = new object[(int)10e6];
long suma = 0;

var begin = System.DateTime.Now;

for (int i = 0; i < liczby.Length; i++)
{
    liczby[i] = i + 1;
}

var end = System.DateTime.Now;

Console.WriteLine(end-begin);

var begin1 = System.DateTime.Now;

for (int i = 0; i < liczby.Length; i++)
{
    suma += liczby[i];
}

var end1 = System.DateTime.Now;

Console.WriteLine(end1 - begin1);
Console.WriteLine(suma);*/


int Zadanie8_Sum(params int[] arg)
{
    int suma = 0;
    foreach (int i in arg)
        suma += i;
    return suma;
}

//Console.WriteLine(Sum(1, 2, 3, 4, 5));
//Console.WriteLine(Sum(3, 4));


float Zadanie10_SumDiv(ref int x, out float res, params int[] arg)
{
    float suma = 0;
    foreach (int i in arg)
        suma += i;
    if (x < arg.Length)
    {
        res = suma / x;
        return suma;
    } 
    else
    {
        x = arg.Length;
        res = suma / x;
        return suma;
    }
    
}

/*float res = 0;
int x = 5;
Console.WriteLine(Zadanie9_SumDiv(ref x, out res, 1, 2, 3, 4, 5, 6));
Console.WriteLine(res);
Console.WriteLine(Zadanie9_SumDiv(ref x, out res, 1, 2, 3, 4));
Console.WriteLine(res);*/

float Zadanie11_Div(float x, float y = 5)
{
    float iloraz = x / y;
    return iloraz;
}

//Console.WriteLine(Zadanie11_Div(1, 0.2f));

//Console.WriteLine(Zadanie11_Div(y : 1, x : 2));

//Ref - przekazywanie przez referencję zamiast wskaźnika (&)
//Out - zmienna, którą można wcześniej zadeklarować i w wyniku działania funkcji zostanie do niej przypisany wynik

public struct Point
{
    int X;
    int Y;

    void Print()
    {
        Console.WriteLine("X : {1}, Y: {2}", X, Y);
    }
}




