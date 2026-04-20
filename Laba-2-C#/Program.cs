using System;
using System.ComponentModel.Design;

public abstract class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Habitat { get; set; }
    public string Diet { get; set; }

    protected Animal(string name, int age, string habitat, string diet)
    {
        Name = name;
        Age = age;
        Habitat = habitat;
        Diet = diet;
    }

    public virtual string Getinfo()
    {
        return $"Кличка: {Name}, Возраст: {Age}, Среда обитания: {Habitat}, Питание: {Diet}";
    }
}
public class Mammal : Animal
    {
        public bool HasFur { get; set; }
        public string FurColor { get; set; }
        public Mammal(string name, int age, string habitat, string diet, bool hasFur, string furColor = "Неизвестно")
            : base(name, age, habitat, diet)
        {
            HasFur = hasFur;
            FurColor = furColor;
        }
        public override string Getinfo()
        {
            if (HasFur)
            {
                return $"{base.Getinfo()}, Тип: Млекопитающее, Шерсть: есть, Окрас шерсти: {FurColor}";
            }
            else
            {
            return $"{base.Getinfo()}, Тип: Млекопитающее, Шерсть: нет";
            }
           
        }
    }

public class Bird : Animal
{
    public double WingSpan { get; set; }
    public Bird(string name, int age, string habitat, string diet, double wingSpan)
        : base(name, age, habitat, diet)
    {
        WingSpan = wingSpan;
    }
    public override string Getinfo()
    {
        return $"{base.Getinfo()}, Тип: Птица, Размах крыльев: {WingSpan} м";
    }
}

public class Fish : Animal
{
    public string WaterType {  get; set; }
    public Fish(string name, int age, string habitat, string diet, string waterType)
        : base(name,age, habitat, diet)
    {
        WaterType = waterType;
    }
    public override string Getinfo()
    {
        return $"{base.Getinfo()}, Тип: Рыба, Тип воды: {WaterType}";
    }
}

public class Reptile : Animal
{
    public bool IsVenomous { get; set; }
    public Reptile(string name, int age, string habitat, string diet, bool isVenomous)
        : base(name, age, habitat, diet)
    {
        IsVenomous = isVenomous;
    }
    public override string Getinfo()
    {
        return $"{base.Getinfo()}, Тип: Пресмыкающееся, Ядовитое: {(IsVenomous ? "да" : "нет")}";
    }
}

public class Amphibian : Animal
{
    public string SkinMoisture { get; set; }
    public Amphibian(string name, int age, string habitat, string diet, string skinMoisture)
        : base(name, age, habitat, diet)
    {
        SkinMoisture = skinMoisture;
    }

    public override string Getinfo()
    {
        return $"{base.Getinfo()}, Тип: Земноводное, Влажность кожи: {SkinMoisture}";
    }
}

public class AnimalManager
{
    private static AnimalManager instance;
    private static readonly object lockObject = new object();
    private List<Animal> animals;

    private AnimalManager()
    {
        animals = new List<Animal>();
    }
    public static AnimalManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new AnimalManager();
                    }
                }
            }
            return instance;
        }
    }

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
        Console.WriteLine($"Животное ({animal.Name}) добавлено в список.");
    }

    public void ShowAllAnimals()
    {
        Console.WriteLine("Список всех животных:");
        for (int i = 0; i < animals.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {animals[i].Getinfo()}");
        }
    }

    public void ShowAnimalByName(string name)
    {
        var animal = animals.Find(a =>  a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (animal != null)
        {
            Console.WriteLine($"{animal.Getinfo()}");
        }
        else
        {
            Console.WriteLine($"Животное с кличкой \"{name}\" не найдено.");
        }
    }

    public void ShowAnimalByIndex(int index)
    {
        if (index >= 1 && index <= animals.Count)
        {
            Console.WriteLine($"{animals[index - 1].Getinfo()}");
        }
        else
        {
            Console.WriteLine("Неверный индекс животного.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        //Существующие животные в списке.
        var manager = AnimalManager.Instance;
        manager.AddAnimal(new Mammal("Лиса", 8, "лес", "хищник", true, "рыжий"));
        manager.AddAnimal(new Bird("Сокол", 15, "горы", "хищник", 3));
        manager.AddAnimal(new Fish("Треска", 2, "водоем", "всеядное", "пресная"));

        //Меню
        while (true)
        {
            Console.WriteLine("\nМеню");
            Console.WriteLine("1. Показать всех животных");
            Console.WriteLine("2. Показать животное по имени");
            Console.WriteLine("3. Показать животное по индексу");
            Console.WriteLine("4. Добавить новое животное");
            Console.WriteLine("5. Выйти");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.ShowAllAnimals();
                    break;
                case "2":
                    Console.Write("Введите кличку животного: ");
                    string name = Console.ReadLine();
                    manager.ShowAnimalByName(name);
                    break;
                case "3":
                    Console.Write("Введите индекс животного: ");
                    if (int.TryParse(Console.ReadLine(), out int index))
                    {
                        manager.ShowAnimalByIndex(index);
                    }
                    else
                    {
                        Console.WriteLine("Такого индекса не существует.");
                    }
                    break;
                case "4":
                    AddNewAnimal(manager);
                    break;
                case "5":
                    Console.WriteLine("Программа завершена, спасибо!");
                    return;
                default:
                    Console.WriteLine("Неверно выбранный пункт.");
                    break;
            }
        }
        

    }
    static void AddNewAnimal(AnimalManager manager)
    {
        Console.WriteLine("Добавление нового животного");
        Console.WriteLine("1. Млекопитающее");
        Console.WriteLine("2. Птица");
        Console.WriteLine("3. Рыба");
        Console.WriteLine("4. Пресмыкающееся");
        Console.WriteLine("5. Земноводное");
        Console.Write("Выберите тип животного: ");
        string typeChoice = Console.ReadLine();

        Console.Write("Кличка: ");
        string name = Console.ReadLine();

        Console.Write("Возраст: ");
        int age = int.TryParse(Console.ReadLine(), out int a) ? a : 0;

        Console.Write("Среда обитания (лес, водоём, пустыня): ");
        string habitat = Console.ReadLine();

        Console.Write("Тип питания (хищник, травоядное, всеядное): ");
        string diet = Console.ReadLine();

        Animal newAnimal = null;

        switch (typeChoice)
        {
            case "1":
                Console.Write("Наличие шерсти (есть/нет): ");
                bool hasFur = Console.ReadLine().ToLower() == "есть";
                

                string furColor = "Неизвестно";
                if (hasFur)
                {
                    Console.Write("Окрас шерсти: ");
                    furColor = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(furColor))
                    {
                        furColor = "Неизвестно"; 
                    }
                }
                newAnimal = new Mammal(name, age, habitat, diet, hasFur, furColor);
                break;
            case "2":
                Console.Write("Размах крыльев (в метрах): ");
                double wingSpan = double.TryParse(Console.ReadLine(), out double w) ? w : 0;
                newAnimal = new Bird(name, age, habitat, diet, wingSpan);
                break;
            case "3":
                Console.Write("Тип воды (пресная/морская): ");
                string waterType = Console.ReadLine();
                newAnimal = new Fish(name, age, habitat, diet, waterType);
                break;
            case "4":
                Console.Write("Ядовитое (да/нет): ");
                bool isVenomous  = Console.ReadLine().ToLower() == "да";
                newAnimal = new Reptile(name, age, habitat, diet, isVenomous);
                break;
            case "5":
                Console.Write("Влажность кожи: ");
                string skinMoisture = Console.ReadLine();
                newAnimal = new Amphibian(name, age, habitat, diet, skinMoisture);
                break;
            default:
                Console.WriteLine("Неверный тип животного!");
                return;
                

        }

        if (newAnimal != null)
        {
            manager.AddAnimal(newAnimal);
        }
    }
}