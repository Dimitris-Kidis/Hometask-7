using System;


public class App
{
    public static void Main(string[] args)
    {
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



    }
}

public class Angle
{
    public int deg { get; set; }
    public int min { get; set; }
    public int sec { get; set; }

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