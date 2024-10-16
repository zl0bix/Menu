using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

/*
class Program
{
    static string[] texts = new string[] { "Сделай выбор оперции, выбирай стрелками вниз-вверх, что бы поддтвердить нажми - Ентер\n",
                "1 : Поиск файла автоматически", "2 : Перебор папок вручную", "3 : Открытие текстового файла","4 : Выход" };
    static void Main(string[] args)
    {
        Console.SetWindowSize(110, 50);
        foreach (string text in texts)
            Console.WriteLine(text);
        int num = keys();//вызов менюшки 
        switch (num)
        {
            case 1: { Console.WriteLine("выбор №1"); Console.ReadKey(); }; break;
            case 2: { Console.WriteLine("выбор №2"); Console.ReadKey(); } break;
            case 3: { Console.WriteLine("выбор №3"); Console.ReadKey(); } break;
            case 4: { }; break;
        }
    }
    static void Text(int i)//Замена цвета менющки
    {
        if (i == 1)
        {
            Console.Clear();
            Console.WriteLine(texts[0]);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(texts[1]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(texts[2]);
            Console.WriteLine(texts[3]);
            Console.WriteLine(texts[4]);
        }
        if (i == 2)
        {
            Console.Clear();
            Console.WriteLine(texts[0]);
            Console.WriteLine(texts[1]);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(texts[2]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(texts[3]);
            Console.WriteLine(texts[4]);
        }
        if (i == 3)
        {
            Console.Clear();
            Console.WriteLine(texts[0]);
            Console.WriteLine(texts[1]);
            Console.WriteLine(texts[2]);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(texts[3]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(texts[4]);
        }
        if (i == 4)
        {
            Console.Clear();
            Console.WriteLine(texts[0]);
            Console.WriteLine(texts[1]);
            Console.WriteLine(texts[2]);
            Console.WriteLine(texts[3]);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(texts[4]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
    static int keys()//работа менюшки
    {
        int num = 0;
        bool flag = false;
        do
        {
            ConsoleKeyInfo keyPushed = Console.ReadKey();
            if (keyPushed.Key == ConsoleKey.DownArrow)
            {
                num++;
                Text(num);
            }
            if (keyPushed.Key == ConsoleKey.UpArrow)
            {
                num--;
                Text(num);
            }
            if (keyPushed.Key == ConsoleKey.Enter)
            {
                flag = true;
            }
            if (num == 0)
            {
                num = 4;
                Text(4);
            }
            if (num == 5)
            {
                num = 1;
                Text(1);
            }
        } while (!flag);
        return num;
    }
}*/

class Program
{
    static void Main(string[] args)
    {
        var elems = new[] { new Element("Google") { Command = GoogleCommandHandler},
            new Element("One"),
            new Element("Test"),
            new Element("    \nExit\n    "){ Command = ExitCommandHandler } };
        Menu menu = new Menu(elems);
        while (true)
        {
            menu.Draw();
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    menu.SelectPrev();
                    break;
                case ConsoleKey.DownArrow:
                    menu.SelectNext();
                    break;
                case ConsoleKey.Enter:
                    menu.ExecuteSelected();
                    break;
                default: return;
            }
        }
    }

    private static void GoogleCommandHandler()
    {
        Process.Start("http://www.google.ru/");
    }

    private static void ExitCommandHandler()
    {
        Environment.Exit(0);
    }
}

delegate void CommandHandler();
class Menu
{
    public Element[] Elements { get; set; }
    public int Index { get; set; }

    public Menu(Element[] elems)
    {
        this.Index = 0;
        this.Elements = elems;
        Elements[Index].IsSelected = true;
    }

    public void Draw()
    {
        Console.Clear();
        foreach (var element in Elements)
        {
            element.Print();
        }
    }

    public void SelectNext()
    {
        if (Index == Elements.Length - 1) return;
        Elements[Index].IsSelected = false;
        Elements[++Index].IsSelected = true;
    }

    public void SelectPrev()
    {
        if (Index == 0) return;
        Elements[Index].IsSelected = false;
        Elements[--Index].IsSelected = true;
    }

    public void ExecuteSelected()
    {
        Elements[Index].Execute();
    }
}

class Element
{
    public string Text { get; set; }
    public ConsoleColor SelectedForeColor { get; set; }
    public ConsoleColor SelectedBackColor { get; set; }
    public bool IsSelected { get; set; }
    public CommandHandler Command;

    public Element(string text)
    {
        this.Text = text;
        this.SelectedForeColor = ConsoleColor.Black;
        this.SelectedBackColor = ConsoleColor.Gray;
        this.IsSelected = false;
    }

    public void Print()
    {
        if (this.IsSelected)
        {
            Console.BackgroundColor = this.SelectedBackColor;
            Console.ForegroundColor = this.SelectedForeColor;
        }
        Console.WriteLine(this.Text);
        Console.ResetColor();
    }

    public void Execute()
    {
        if (Command == null) return;
        Command.Invoke();
    }
}




/*

static void Text(int i)
{
    Console.Clear();
    for (int n = 0; n < texts.Length; n++)
    {
        if (n == i)
            PrintColored(texts[n], ConsoleColor.Green, ConsoleColor.Blue)
        else
            Console.WriteLine(texts[n]);
    }

    static void PrintColored(string text, ConsoleColor foreground, ConsoleColor background)
    {
        Console.BackgroundColor = background;
        Console.ForegroundColor = foreground;
        Console.WriteLine(text);
        Console.ResetColor();
    }
*/