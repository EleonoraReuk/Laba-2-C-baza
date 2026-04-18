using System;
using System.Globalization;

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

    public class Mammal : Animal
    {
        public bool HasFur { get; set; }
        public Mammal(string name, int age, string habitat, string diet, bool hasFur)
            : base(name, age, habitat, diet)
        {
            HasFur = hasFur;
        }
        public override string Getinfo()
        {
            return $"{base.Getinfo()}, Тип: Млекопитающее, Шерсть: {(HasFur ? "есть" : "нет")}";
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



