// Hometask #7

// 1. Class Angle. 1 deg = 60 mins, 1 min = 60 secs. 
//    Overload + and - operators. ✅
// 2. Create a class and create a custom sorting using ... ✅
// 3. Create a utility class ( 3.1. Use static methods) ✅
// 4. Use static constructor ✅

using System;
using static Helper;
using System.IO;
using System.Text;
using System.Collections;


public class App
{
    public static void Main(string[] args)
    {
        Angle a = new Angle(3, 36, 53);
        Angle b = new Angle(4, 27, 45);
        Angle c = new Angle(0, 36, 2);
        Angle d = new Angle(3, 14, 17);
        Angle e = new Angle(0, 21, 45);
        Angle f = new Angle(-23, -21, -45);
        Angle abc = new Angle(20, 102, 71);
        Console.WriteLine(abc.ToString());
        // c = (8, 4, 38)
        //Console.WriteLine(a.ToString());

        Console.WriteLine((a + b).ToString());
        //Console.WriteLine((a - b).ToString());
        Console.WriteLine(c);
        Console.WriteLine(d);
        Console.WriteLine((c - d).ToString());
        Console.WriteLine((e + (c-d)).ToString());


        Console.WriteLine(f);

        
        Console.WriteLine(NearestSquareRoot(5));
        Console.WriteLine(RomanToInteger("XIX"));
        Console.WriteLine(IntToRoman(21));


        Animal[] animals = new Animal[7]
        {
            new Animal {ID = 4, Name = "Tiger", FoodIntake = 3.54 },
            new Animal {ID = 7, Name = "Cat", FoodIntake = 1.02 },
            new Animal {ID = 1, Name = "Owl", FoodIntake = 0.88 },
            new Animal {ID = 6, Name = "Elephant", FoodIntake = 8.32 },
            new Animal {ID = 3, Name = "Dog", FoodIntake = 2.18 },
            new Animal {ID = 5, Name = "Duck", FoodIntake = 1.29 },
            new Animal {ID = 2, Name = "Bear", FoodIntake = 6.98 }
        };

        Console.WriteLine("Initial Input Array: ");
        foreach (Animal animal in animals)
        {
            Console.WriteLine(animal);
        }

        Console.WriteLine("\nArray sorted by Food Intake: ");
        Array.Sort(animals);
        foreach (Animal animal in animals)
        {
            Console.WriteLine(animal);
        }

        Console.WriteLine("\nArray sorted by Name: ");
        Array.Sort(animals, Animal.SortByName);
        foreach (Animal animal in animals)
        {
            Console.WriteLine(animal);
        }
    }
}

public class Angle
{
    public int Deg { get; set; }
    // *
    public int Min { get; set; }
    public int Sec { get; set; }

    static Angle ()
    {
        try
        {
            var lastLine = File.ReadLines("hometask7.txt").Last();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Last Log: " + lastLine);
            Console.ForegroundColor = ConsoleColor.White;
            

            FileStream fileStream = File.Open("hometask7.txt",
                FileMode.Append, FileAccess.Write);
            
            StreamWriter fileWriter = new StreamWriter(fileStream);
            
            fileWriter.WriteLine("Compiled at " + System.DateTime.Now.ToString() + " by " + Environment.UserName + " on " + Environment.OSVersion);
            fileWriter.Flush();
            fileWriter.Close();
            
        }
        catch (IOException ioe)
        {
            Console.WriteLine(ioe);
        }
    }
    public Angle(int deg, int min, int sec)
    {
        this.Deg = deg + min / 60;
        this.Min = (sec / 60) + min % 60;
        this.Sec = sec % 60;
    }

    public Angle (Angle angle)
    {
        Deg = angle.Deg;
        Min = angle.Min;
        Sec = angle.Sec;
    }

    public override string ToString()
    {
        return "T = " + Deg + "° " + Min.ToString("D2") + "\' " + Sec.ToString("D2") + "\" ";
    }

    public static Angle operator +(Angle first, Angle second)
    {
        return new Angle(first.Deg + second.Deg, first.Min + second.Min, first.Sec + second.Sec);
    }

    public static Angle operator -(Angle first, Angle second)
    {
        int var1 = Int32.Parse(first.Deg.ToString() + first.Min.ToString() + first.Sec.ToString());
        int var2 = Int32.Parse(second.Deg.ToString() + second.Min.ToString() + second.Sec.ToString());
        
        if (var1 - var2 == 0) return new Angle(0, 0, 0);

        int marker = var1 > var2 ? 1 : -1;

        int s, m, g; int mt, gt;
        
        if (first.Sec < second.Sec && first.Min < second.Min)
        {
            s = (first.Sec + 60) - second.Sec;
            mt = first.Min - 1;
            m = mt - second.Min;
            g = first.Deg - second.Deg;
            m = (first.Min + 60) - second.Min;
            gt = first.Deg - 1;
            g = gt - second.Deg;
        } else if (first.Sec < second.Sec)
        {
            s = (first.Sec + 60) - second.Sec;
            mt = first.Min - 1;
            m = mt - second.Min;
            g = first.Deg - second.Deg;
        } else if (first.Min < second.Min)
        {
            m = (first.Min + 60) - second.Min;
            gt = first.Deg - 1;
            g = gt - second.Deg;
            s = first.Sec - second.Sec;
        } else
        {
            m = first.Min - second.Min;
            s = first.Sec - second.Sec;
            g = first.Deg - second.Deg;
        }

        return new Angle(marker * Math.Abs(g), marker * Math.Abs(m), marker * Math.Abs(s));
    }
}



class Helper
{
    public static int NearestSquareRoot(int n)
    {
        return (int)Math.Pow(Math.Round(Math.Sqrt(n)), 2);
    }

    private static Dictionary<char, int> RomanMap = new Dictionary<char, int>()
    {
        {'I', 1},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000}
    };

    public static int RomanToInteger(string roman)
    {
        int number = 0;
        for (int i = 0; i < roman.Length; i++)
        {
            if (i + 1 < roman.Length && RomanMap[roman[i]] < RomanMap[roman[i + 1]])
            {
                number -= RomanMap[roman[i]];
            }
            else
            {
                number += RomanMap[roman[i]];
            }
        }
        return number;
    }

    public static string IntToRoman(int number)
    {
        if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("Insert value betwheen 1 and 3999");
        if (number < 1) return string.Empty;
        if (number >= 1000) return "M" + IntToRoman(number - 1000);
        if (number >= 900) return "CM" + IntToRoman(number - 900);
        if (number >= 500) return "D" + IntToRoman(number - 500);
        if (number >= 400) return "CD" + IntToRoman(number - 400);
        if (number >= 100) return "C" + IntToRoman(number - 100);
        if (number >= 90) return "XC" + IntToRoman(number - 90);
        if (number >= 50) return "L" + IntToRoman(number - 50);
        if (number >= 40) return "XL" + IntToRoman(number - 40);
        if (number >= 10) return "X" + IntToRoman(number - 10);
        if (number >= 9) return "IX" + IntToRoman(number - 9);
        if (number >= 5) return "V" + IntToRoman(number - 5);
        if (number >= 4) return "IV" + IntToRoman(number - 4);
        if (number >= 1) return "I" + IntToRoman(number - 1);
        throw new ArgumentOutOfRangeException("Something bad happened");
    }

}


class Animal : IComparable
{
    public int ID { get; set; }

    public string Name { get; set; }    

    public double FoodIntake { get; set; }

    public override string ToString()
    {
        return String.Format("ID: {0, 3} Name: {1, -9} Food Intake: {2, 3}", ID, Name, FoodIntake);
    }

    public static IComparer SortByName
    {
        get
        {
            return new NameComparer();
        }
    }
    

    public int CompareTo(object? obj)
    {
        Animal tmp = (Animal)obj;
        if (this.FoodIntake > tmp.FoodIntake) return 1;
        if (this.FoodIntake < tmp.FoodIntake) return -1; 
        else return 0;
    }
}

class NameComparer : IComparer
{
    int IComparer.Compare(object x, object y)
    {
        Animal animal1 = (Animal)x;
        Animal animal2 = (Animal)y;
        return String.Compare(animal1.Name, animal2.Name);
    }
}
