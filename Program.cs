using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace Program_Notes
{
    internal class Program
    {
        //Программа “Заметки”
        //Программа хранит список заметок, пользователь может добавлять, редактировать и удалять заметки.

        //Функционал:

        //•Вывод всех заметок
        //•Добавление новой заметки в список
        //•Удаление существующей заметки из списка
        //•Удаление всех заметок.
        //•Изменение существующей заметки
        //•(Дополнительно) сохранение заметок в файл и загрузка

        static private List<string> notes = new List<string>(5);

        static void Main(string[] args)
        {
            Menu(notes);
        }

        static void Menu(List<string> notes, int noteIndex = 0, string newNote = "")
        {
            Console.WriteLine("\n\t----Выберите действие----");
            Console.WriteLine("\t[A] - Посмотреть все заметки");
            Console.WriteLine("\t[N] - Добавить новую заметку");
            Console.WriteLine("\t[C] - Изменить существующую заметку");
            Console.WriteLine("\t[D] - Удалить заметку");
            Console.WriteLine("\t[S] - Сохранить заметки в файл Notes.txt");
            Console.WriteLine("\t[L] - Загрузить заметки из файла Notes.txt");
            Console.WriteLine("\t[Delete] - Удалить все заметки");
            Console.WriteLine("\t[Escape] - Завершить работу программы\n");

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A:
                    Console.Clear();

                    PrintNotes(notes);

                    break;

                case ConsoleKey.N:
                    Console.Clear();

                    Console.WriteLine("Введите новую заметку:");

                    newNote = Console.ReadLine();

                    NewNote(notes, newNote);

                    PrintNotes(notes);

                    break;

                case ConsoleKey.C:
                    Console.Clear();

                    PrintNotes(notes);

                    Console.WriteLine("Заметку с каким номером изменить?");

                    try
                    {
                        noteIndex = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\n[Ошибка!] Неверный номер заметки. Введите целое число\n");

                        break;
                    }

                    Console.WriteLine("Новый текст заметки:");

                    newNote = Console.ReadLine();

                    ChangeNote(notes, noteIndex, newNote);

                    PrintNotes(notes);

                    break;

                case ConsoleKey.D:
                    Console.Clear();

                    PrintNotes(notes);

                    Console.WriteLine("Заметку с каким номером удалить?");

                    try
                    {
                        noteIndex = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\n[Ошибка!] Неверный номер заметки. Введите целое число\n");

                        break;
                    }

                    DeleteNote(notes, noteIndex);

                    PrintNotes(notes);

                    break;

                case ConsoleKey.S:
                    Console.Clear();

                    SaveToDisc(notes);

                    Console.WriteLine("Заметки сохранены в файл Notes.txt в папке программы");

                    PrintNotes(notes);

                    break;

                case ConsoleKey.L:
                    Console.Clear();

                    LoadFromDisc(ref notes);

                    Console.WriteLine("Заметки загружены из файла Notes.txt в папке программы");

                    PrintNotes(notes);

                    break;

                case ConsoleKey.Delete:
                    Console.Clear();

                    DeleteAllNotes(notes);

                    PrintNotes(notes);

                    break;

                case ConsoleKey.Escape:
                    Process.GetCurrentProcess().Kill();

                    break;

                default:
                    Console.Clear();

                    Console.WriteLine("\n[Ошибка!] Вы нажали неверную клавишу. Выберете один из вариантов:\n");

                    break;
            }

            Menu(notes);
        }

        static void PrintNotes(List<string> notes)
        {
            if (notes.Count == 0)
            {
                Console.WriteLine("У вас нет заметок");
            }
            else
            {
                Console.WriteLine("Ваши заметки:");

                foreach (var item in notes)
                {
                    Console.WriteLine((notes.IndexOf(item) + 1) + ". " + item);
                }
            }
        }

        static void NewNote(List<string> notes, string newNote)
        {
            notes.Add(newNote);
        }

        static void ChangeNote(List<string> notes, int noteIndex, string newNote)
        {
            if (noteIndex > notes.Count || noteIndex == 0)
            {
                Console.WriteLine("\n[Ошибка!] Заметки с таким номером не существует\n");
            }
            else
            {
                notes[noteIndex - 1] = newNote;
            }
        }

        static void DeleteNote(List<string> notes, int noteIndex)
        {
            if (noteIndex > notes.Count || noteIndex == 0)
            {
                Console.WriteLine("\n[Ошибка!] Заметки с таким номером не существует\n");
            }
            else
            {
                notes.RemoveAt(noteIndex - 1);
            }
        }

        static void SaveToDisc(List<string> notes)
        {
            try
            {
                File.WriteAllLines("Notes.txt", notes);
            }
            catch (Exception)
            {
                Console.WriteLine("\n[Ошибка!] Не удалось сохранить файл на диск\n");
            }
        }

        static void LoadFromDisc(ref List<string> notes)
        {
            try
            {
                notes = File.ReadAllLines("Notes.txt").ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("\n[Ошибка!] Не удалось загрузить с диска\n");
            }
        }

        static void DeleteAllNotes(List<string> notes)
        {
            notes.Clear();
        }
    }
}
