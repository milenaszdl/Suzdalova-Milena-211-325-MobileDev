//Список List

//(A) Обычный список

using System.Collections;

ArrayList arrayList = new ArrayList();
arrayList.Add(100);
arrayList.Add("Hello");
arrayList.Add(3.14);


//(B) Обобщенный список

List<string> cities = new() { "Москва", "Воронеж", "Тула" };
cities.Add("Минск");
cities.AddRange(new string[] { "Севастополь", "Ялта" });
cities.RemoveAt(1);
Console.WriteLine(String.Join(' ', cities));
cities.Sort();
Console.WriteLine(String.Join(' ', cities));