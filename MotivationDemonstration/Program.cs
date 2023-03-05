
//using Colorful;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Net;

namespace System
{
    class Program
    {
        public static void Main(string[] args)
        {
            string mainText = "Спящие видят сны, а сны тех, кто предпочел учебу дреме, становятся явью.  Никогда не бывает слишком поздно.Муки учения скоротечны, а муки невежества — нескончаемы.  Учеба измеряется не временем, а старанием.Жизнь предназначена не только для учебы, но если вы не можете справиться с этой частью жизни, то с чем тогда вы способны справиться?. Трудности и испытания могут оказаться приятными.Только справившись с задачей раньше других и приложив максимум усилий, вы сможете радоваться успеху по-настоящему. Не каждому дано преуспевать во всем. Успех сопутствует лишь тем, кто наделен решительностью и способен к самосовершенствованию. Время скоротечно.Сегодняшнее безделье обернется завтра слезами.Реалист — это тот, кто заботится о будущем.Зарплата прямо пропорциональна уровню образования.День сегодняшний никогда не повторится вновь.Даже сейчас ваши враги жадно переворачивают листы книг.Не попотеешь — не заработаешь.День, потраченный впустую, рождает бездну бессмысленных дней.Не откладывайте на завтра то, что можете сделать сегодня.Источник плохой учебы не в нехватке времени, а в недостаточном упорстве.Счастье — это не заслуга, а успех.Наслаждайся страданиями, если не можешь их избежать.Оценка пропорциональна времени, затраченному на учебу.Невозможность — отговорка для тех, кто не пытается.Не придешь сегодня — прибежишь завтра.Нет момента ближе к успеху, чем тот, когда вы думаете, что все кончено.Academic clique is money (Академическая сообщество — это деньги). Без боли успеха не бывает.Не надо дремать, ложитесь спать.Самое важное случается, когда другие спят.Дайте волю смыкающимся векам, и будущее пронесется мимо вас. Дополнительный час учебы определит ваше будущее";
            string[] arrayWithMotivation = mainText.Split('.', StringSplitOptions.RemoveEmptyEntries);

            IServiceCollection serviceCollection = new ServiceCollection()
                .AddSingleton<IRemember, Remember>();

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            Lazy<IRemember> rememberLazy = new Lazy<IRemember>(serviceProvider.GetService<IRemember>());
            if (rememberLazy.Value is not null)
            {
                rememberLazy.Value.DemonstrateMotivation(arrayWithMotivation, GetName());
            }


        }
        private static string GetName()
        {
            var favorites = AnsiConsole.Prompt(
        new MultiSelectionPrompt<string>()
          .PageSize(10)
          .Title("Whats your [green]'name'[/]?")
          .MoreChoicesText("[grey](Move up and down to reveal more 'not needed names')[/]")
          .InstructionsText("[grey](Press [blue]space[/] to toggle a name, [green]enter[/] to accept)[/]")
          .AddChoices(new[] 
          {
            "Nothing", "Fearless", "Dirt", "Void", "Noone", "Iam"
          }));

            var name = favorites.Count == 1 ? favorites[0] : null;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Ok, but if you could only choose [green]one[/]?")
                        .MoreChoicesText("[grey](Move up and down to reveal more name)[/]")
                        .AddChoices(favorites));
            }

            AnsiConsole.MarkupLine("You selected: [yellow]{0}[/]", name);
            return name;
            
            //Console.Write("Yours important name: ");
            //return Console.ReadLine().Length == 0 ? "noone" : "someone";
        }
       
        public interface IRemember
        {
            void DemonstrateMotivation(string?[]phrases, string name);
        }
        public class Remember : IRemember, IDisposable
        {
            private readonly Random rand = new Random();
            private int localValue = 1;
            public Remember() { }


            public void DemonstrateMotivation(string?[] motivationArray, string name)
            {
            
                if (motivationArray != null)
                {
                    //Console.WriteLine($"\n{localValue++}|{motivationArray[rand.Next(0, motivationArray.Length)]}\nВоспользуйся сегодняшним днём - {DateTime.Now.ToShortDateString()}"
                    //    +$"\n\nНажмите Enter/Y для вывода очередной истины");

                    AnsiConsole.Write(
                              new Table()
                            .AddColumn(new TableColumn($"{(localValue++)}) Tip").Centered())
                .AddColumn(new TableColumn("Trully name").Centered())
                            .AddRow($"{motivationArray[rand.Next(0, motivationArray.Length)]}", name));

                    AnsiConsole.Write(
                        new Table()
                        .AddColumn(new TableColumn("Number of chance to change a world").LeftAligned())
                        .AddRow($"Воспользуйся сегодняшним днём - {DateTime.Now.ToShortDateString()}"));

                    ////Figlet figlet = new Figlet(font);
                    ////Thread.Sleep(10);
                    ////Console.WriteLine(figlet.ToAscii($"{name}"));
                    //Console.ReadLine();

                    AnsiConsole.Write(
                        new FigletText(FigletFont.Load("larry3d.flf"), name)/*.Color(Color.Purple3)*/
                       );


                    switch (CheckUserAnswer())
                    {
                        case true:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            DemonstrateMotivation(motivationArray,name);
                            break;
                        
                        default:
                            Console.ReadLine();
                            return;
                    }
                }
            }

            private bool CheckUserAnswer()
            {
                if (!AnsiConsole.Confirm("Do u want to see another truth?"))
                {
                    AnsiConsole.MarkupLine("[DarkRed]See ya[/]");
                    return false;
                }
                return true;
            }
            public void Dispose()
            {
              
            }
        }
    }
}