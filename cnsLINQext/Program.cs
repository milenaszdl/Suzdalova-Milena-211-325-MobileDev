var cities = new string[] { "Москва", "Сочи", "Орел", "Самара", "Тула", "Смоленск", "Ялта"};

var x1 = cities.Where(v => v.StartsWith("С") && v.Length > 4).OrderBy(v => v).ToArray();

Console.WriteLine(string.Join(Environment.NewLine, x1));
Console.WriteLine();

var x2 = cities.OrderBy(v => v).Select(v => $"{v.ToUpper()} - {v.Contains('а')}").ToArray();
Console.WriteLine(string.Join(Environment.NewLine, x2));