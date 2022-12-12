using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Xml.Serialization;

namespace редактор
{
    internal class Program
    {
        public class autors
        {
            public List<autor> str;

            public class autor
            {
                public string name;
                public string[] books;
                public int series;
            }
            public autors()
            {
                autor gleb = new autor();
                gleb.name = "Глебов Макс";
                gleb.books = new string[] { "Звезд не хватит на всех", "Бригадный генерал", "Блюстители хаоса" };
                gleb.series = 5;

                autor fox = new autor();
                fox.name = "Северный Лис";
                fox.books = new string[] { "Мимик", "Приманка", "хаос" };
                fox.series = 3;

                autor mih = new autor();
                mih.name = "Михаил Атаманов";
                mih.books = new string[] { "Искажающие реальность", "Альянс Неудачников", "Тёмный травник" };
                mih.series = 7;

                str = new List<autor>();
                str.Add(gleb);
                str.Add(fox);
                str.Add(mih);
                //первичная инициализация закончена
            }
        }
        public class opensave
        {
            private void saveasjson(string path, List<autors.autor> str)
            {
                string json = JsonConvert.SerializeObject(str);
                File.WriteAllText(path, json);
            }
            private void saveasxml(string path, List<autors.autor> str)
            {
                XmlSerializer xml = new XmlSerializer(typeof(autors));
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, str);
                }
            }
            private void saveastxt(string path, List<autors.autor> str)
            {
                File.WriteAllText(path, str.ToString());
            }
            private autors openasjson(string path)
            {
                return (autors)JsonConvert.DeserializeObject(File.ReadAllText(path), typeof(autors));
            }
            private autors openasxml(string path)
            {
                XmlSerializer xml = new XmlSerializer(typeof(autors));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return (autors)xml.Deserialize(fs);
                }
            }
            /*  private autors openastxt(string path, List<autors.autor> str)
              {
                 return (autors)File.ReadAllText(path);
              } */
            public void save(string path, List<autors.autor> str)
            {
                string l = path.Substring(path.Length - 4, 4);
                if (0 == string.Compare(l, "json")) { saveasjson(path, str); }
                if (0 == string.Compare(l, ".xml")) { saveasxml(path, str); }
                if (0 == string.Compare(l, ".txt")) { saveastxt(path, str); }
            }
            public void open(string path, List<autors.autor> str)
            {
                string l = path.Substring(path.Length - 4, 4);
                if (0 == string.Compare(l, "json")) { openasjson(path); }
                if (0 == string.Compare(l, ".xml")) { openasxml(path); }
                // if (0 == string.Compare(l, ".txt")) { saveastxt(path, str); }
            }


        }
        static void Main(string[] args)
        {
            autors ai = new autors();

            opensave os = new opensave();

            Console.WriteLine("F1 - открыть файл, F2 - сохранить файл, Esc - выход");
            Console.WriteLine("");
            Console.WriteLine("имя файла");
            foreach (autors.autor aut in ai.str)
            {
                Console.WriteLine("автор - " + aut.name);
                foreach (string st in aut.books) { Console.Write(st + " "); }
                Console.WriteLine("");
                Console.WriteLine("циклов - " + aut.series);
            }

            ConsoleKeyInfo cki;
            Console.TreatControlCAsInput = true;
            int i = 0;
            do
            {
                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.F1:
                        Console.SetCursorPosition(1, 1);
                        Console.WriteLine("Введите имя файла");
                        os.open(Console.ReadLine(), ai.str);
                        i = 1;
                        break;
                    case ConsoleKey.F2:
                        Console.SetCursorPosition(1, 1);
                        Console.WriteLine("Введите имя файла");
                        os.save(Console.ReadLine(), ai.str);
                        i = 2;
                        break;
                    case ConsoleKey.Escape: i = 5; break;
                }
            } while (i != 5);



        }
    }
}
/*Необходимо реализовать текстовый редактор в консоли, который будет принимать следующие форматы:
 * txt, json и xml, и сохранять файлы в следующие форматы: txt, json и xml. 
Работа должна идти с какой-либо моделью, которую вы создадите, например, у меня это была модель
"Figure" с названием, шириной и высотой Должен быть выбор файла по его пути перед началом программы
Должна быть выгрузка информации из файла - не просто содержимое, а именно каждая строчка показывает
одно свойство (не должно быть такого, что при загрузке json у вас показывалась вся структура json, 
см. видео) По нажатию на клавишу F1 должно быть сохранение файла. Сохранение должно автоматически 
подбирать нужный формат сериализации, в зависимости от того, что написано в пути файла 
(txt, json или xml)
По нажатию на клавишу Escape программа должна закончить свою работу Структура кода для начальной 
оценки 4:
Должна быть модель, по которой вы будете сериализовывать и десериализовывать данные. 
Внутри этой модели должен быть конструктор
Должен быть отдельный класс для чтения\сохранения файла. 
Методы внутри должны быть приватные и публичные*/
бличные */