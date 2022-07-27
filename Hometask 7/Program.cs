// Hometask #7

// 1. Class Angle. 1 deg = 60 mins, 1 min = 60 secs. 
//    Overload + and - operators. ✅
// 2. Create a class and create a custom sorting using ...
// 3. Create a utility class ( 3.1. Use static methods) ✅
// 4. Use static constructor ✅

using System;
using static Helper;
using System.IO;
using System.Text;


public class App
{
    public static void Main(string[] args)
    {
        //Angle LOG = new Angle();
        Angle a = new Angle(5, 26, 12);
        Angle b = new Angle(3, 7, 34);
        Angle c = new Angle(0, 36, 2);
        Angle d = new Angle(3, 14, 17);
        Angle e = new Angle(0, 21, 45);
        Angle f = new Angle(-23, -21, -45);
        Angle abc = new Angle(20, 102, 71);
        //Console.WriteLine(abc);
        // c = (8, 4, 38)
        //Console.WriteLine(a.ToString());

        //Console.WriteLine((a + b).ToString());
        //Console.WriteLine((a - b).ToString());
        Console.WriteLine(c);
        Console.WriteLine(d);
        Console.WriteLine((c - d).ToString());
        Console.WriteLine((e + (c-d)).ToString());


        Console.WriteLine(f);

        
        Console.WriteLine(NearestSquareRoot(5));
        Console.WriteLine(RomanToInteger("XIX"));
        Console.WriteLine(IntToRoman(21));

    }
}

public class Angle
{
    public int deg { get; set; }
    public int min { get; set; }
    public int sec { get; set; }

    static Angle ()
    {
        try
        {
            // If techcoil.txt exists, seek to the end of the file,
            // else create a new one.
            FileStream fileStream = File.Open("hometask7.txt",
                FileMode.Append, FileAccess.Write);
            // Encapsulate the filestream object in a StreamWriter instance.
            StreamWriter fileWriter = new StreamWriter(fileStream);
            // Write the current date time to the file
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
        Console.ForegroundColor = ConsoleColor.Red;
        this.deg = deg + min / 60;
        this.min = (sec / 60) + min % 60;
        this.sec = sec % 60;
        Console.ForegroundColor = ConsoleColor.White;
    }

    public Angle (Angle angle)
    {
        deg = angle.deg;
        min = angle.min;
        sec = angle.sec;
    }

    public override string ToString()
    {
        return "T = " + deg + "° " + min.ToString("D2") + "\' " + sec.ToString("D2") + "\" ";
    }

    public static Angle operator +(Angle first, Angle second)
    {
        return new Angle(first.deg + second.deg, first.min + second.min, first.sec + second.sec);
    }

    public static Angle operator -(Angle first, Angle second)
    {
        //Console.WriteLine((first.deg.ToString() + first.min.ToString() + first.sec.ToString()));
        int var1 = Int32.Parse(first.deg.ToString() + first.min.ToString() + first.sec.ToString());
        int var2 = Int32.Parse(second.deg.ToString() + second.min.ToString() + second.sec.ToString());
        
        if (var1 - var2 == 0) return new Angle(0, 0, 0);

        int marker = var1 > var2 ? 1 : -1;

        //Console.ForegroundColor= ConsoleColor.Green;
        //Console.WriteLine(marker);
        //Console.ForegroundColor= ConsoleColor.Green;

        int s, m, g; int mt, gt;
        
        if (first.sec < second.sec && first.min < second.min)
        {
            s = (first.sec + 60) - second.sec;
            mt = first.min - 1;
            m = mt - second.min;
            g = first.deg - second.deg;
            m = (first.min + 60) - second.min;
            gt = first.deg - 1;
            g = gt - second.deg;
        } else if (first.sec < second.sec)
        {
            s = (first.sec + 60) - second.sec;
            mt = first.min - 1;
            m = mt - second.min;
            g = first.deg - second.deg;
        } else if (first.min < second.min)
        {
            m = (first.min + 60) - second.min;
            gt = first.deg - 1;
            g = gt - second.deg;
            s = first.sec - second.sec;
        } else
        {
            m = first.min - second.min;
            s = first.sec - second.sec;
            g = first.deg - second.deg;
        }

        //Console.WriteLine("***   " + g + " " + m + " " + s);
        //Console.WriteLine("Marker " + marker);

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