//массив

string[] cities = { "Тула", "Уфа", "Ялта", "Сочи"};
Console.WriteLine(cities[0]);
Console.WriteLine(cities[^1]); //галочка - идем с обратной стороны, индексы справа начианются с 1
Console.WriteLine("------------");

for (int i = 0; i < cities.Length; i++)
{
    Console.WriteLine(cities[i]);
}
Console.WriteLine();
Console.WriteLine("------------");


foreach (var city in cities)
{
    Console.WriteLine(city);
}
Console.WriteLine("------------");

//изменить размер массива 
Array.Resize<string>(ref cities, 3);
Console.WriteLine(String.Join(' ', cities));

//пустой массив 
string[] m1 = { };
string[] m2 = Array.Empty<string>();