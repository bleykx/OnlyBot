using Microsoft.EntityFrameworkCore;
using OnlyBot_DataAccess;
using OnlyBot_Models;
using OnlyBot_Models.Enums;

namespace OnlyBot
{
    public static class PrepDb
    {
        public async static Task PrepBot(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                await SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
            }
        }

        public async static Task SeedData(ApplicationDbContext context)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Appling Migrations...");

            await context.Database.MigrateAsync();
            if (context.Database.GetPendingMigrations().Any())
            {

            }

            if (!context.Bots.Any())
            {

                Console.WriteLine("Adding data - seeding Scripts...");
                var scriptSell = new Script()
                {
                    Id = Guid.NewGuid(),
                    Name = "ScriptSeller",
                    Description = "Script de test seller n°1",
                    Path = "C:/Users/jeanb/Documents/OnlyBot/OnlyBot_Scripts/SELL.cs",
                    Type = ScriptTypeEnum.Sell
                };

                var scriptBank = new Script()
                {
                    Id = Guid.NewGuid(),
                    Name = "ScriptBank",
                    Description = "Script de test banker n°1",
                    Path = "C:/Users/jeanb/Documents/OnlyBot/OnlyBot_Scripts/BANK.cs",
                    Type = ScriptTypeEnum.Bank
                };

                var scriptFight = new Script()
                {
                    Id = Guid.NewGuid(),
                    Name = "ScriptFight",
                    Description = "Script de test fighter n°1",
                    Path = "C:/Users/jeanb/Documents/OnlyBot/OnlyBot_Scripts/FIGHT.cs",
                    Type = ScriptTypeEnum.Fight
                };

                var scriptGather = new Script()
                {
                    Id = Guid.NewGuid(),
                    Name = "ScriptGather",
                    Description = "Script de test gatherer n°1",
                    Path = "C:/Users/jeanb/Documents/OnlyBot/OnlyBot_Scripts/GATHER.cs",
                    Type = ScriptTypeEnum.Gather
                };
                context.Scripts.AddRange(scriptSell, scriptBank, scriptFight, scriptGather);

                Console.WriteLine("Seeding Scripts done...");

                Console.WriteLine("Adding data - seeding Proxies...");

                var proxy1 = new Proxy()
                {
                    Id = Guid.NewGuid(),
                    Name = "Super le Proxy",
                    IP = "192.168.1.42",
                    Port = 420,
                    Username = "Jean-Proxy",
                    Password = "Jean-MDP",
                    SocketType = "Socks5",
                    PlanExpirationDate = DateTime.Now.AddDays(420),
                    IsBanned = false,
                    Provider = "Jean-Bibi"
                };
                var proxy2 = new Proxy()
                {
                    Id = Guid.NewGuid(),
                    Name = "Pas ouf le Proxy",
                    IP = "192.168.1.69",
                    Port = 69,
                    SocketType = "HTTPS",
                    PlanExpirationDate = DateTime.Now.AddDays(69),
                    IsBanned = true,
                    Provider = "Jean-Foin"
                };
                context.Proxies.AddRange(proxy1, proxy2);


                Console.WriteLine("Adding data - seeding Bots...");
                var bot1 = new Bot()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "jeanbwatmail@jeanmail.fr",
                    Password = "Boubout0",
                    Name = "Jean-Lubulule",
                    Description = "Bot de test n°1",
                    Breed = BreedEnum.Iop,
                    Gender = "Male",
                    Script = scriptBank,
                    ScriptIsRunning = true,
                    Proxy = proxy1,
                    Server = ServerEnum.Draconiros,
                    IsConnected = true,
                    Level = 69,
                    Life = 42,
                    MaxLife = 420,
                    Energy = 4444,
                    MaxEnergy = 10000,
                    Experience = 50,
                    ExperienceFloor = 50,
                    ExperienceNextFloor = 100,
                    ExperiencePercent = 50
                };
                var bot2 = new Bot()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "jeanbwatmiel@jeanmail.fr",
                    Password = "Boubout0",
                    Name = "Jean-Kassouleyw",
                    Description = "Bot de test n°2",
                    Breed = BreedEnum.Steamer,
                    Gender = "Female",
                    Script = scriptSell,
                    ScriptIsRunning = false,
                    Proxy = proxy1,
                    Server = ServerEnum.Draconiros,
                    IsConnected = false,
                    Level = 150,
                    Life = 441,
                    MaxLife = 4210,
                    Energy = 4444,
                    MaxEnergy = 10000,
                    Experience = 50,
                    ExperienceFloor = 50,
                    ExperienceNextFloor = 100,
                    ExperiencePercent = 50
                };
                var bot3 = new Bot()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "jeanbouatmail@jeanmail.fr",
                    Password = "Boubout0",
                    Name = "Jean-Bouyabece",
                    Description = "Bot de test n°3",
                    Breed = BreedEnum.Osamodas,
                    Gender = "Male",
                    Script = scriptSell,
                    ScriptIsRunning = false,
                    Proxy = proxy1,
                    Server = ServerEnum.Imagiro,
                    IsConnected = true,
                    Level = 200,
                    Life = 4411,
                    MaxLife = 5210,
                    Energy = 3141,
                    MaxEnergy = 10000,
                    Experience = 50,
                    ExperienceFloor = 50,
                    ExperienceNextFloor = 100,
                    ExperiencePercent = 50
                };
                var bot4 = new Bot()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "jeanleuleu@jeanmail.fr",
                    Password = "Boubout0",
                    Name = "Jean-Poucave",
                    Description = "Bot de test n°4",
                    Breed = BreedEnum.Cra,
                    Gender = "Female",
                    Script = scriptFight,
                    ScriptIsRunning = true,
                    Proxy = proxy1,
                    Server = ServerEnum.Imagiro,
                    IsConnected = true,
                    Level = 200,
                    Life = 4411,
                    MaxLife = 5210,
                    Energy = 3141,
                    MaxEnergy = 10000,
                    Experience = 50,
                    ExperienceFloor = 50,
                    ExperienceNextFloor = 100,
                    ExperiencePercent = 50
                };
                var bot5 = new Bot()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "jeanbeuhgueu@jeanmail.fr",
                    Password = "Boubout0",
                    Name = "Jean-Prout",
                    Description = "Bot de test n°5",
                    Breed = BreedEnum.Cra,
                    Gender = "Male",
                    Script = scriptFight,
                    ScriptIsRunning = true,
                    Proxy = proxy1,
                    Server = ServerEnum.Imagiro,
                    IsConnected = true,
                    Level = 200,
                    Life = 4411,
                    MaxLife = 5210,
                    Energy = 3141,
                    MaxEnergy = 10000,
                    Experience = 50,
                    ExperienceFloor = 50,
                    ExperienceNextFloor = 100,
                    ExperiencePercent = 50
                };
                var bot6 = new Bot()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "jeandarme@jeanmail.fr",
                    Password = "Boubout0",
                    Name = "Jean-Papier",
                    Description = "Bot de test n°6",
                    Breed = BreedEnum.Cra,
                    Gender = "Female",
                    Script = scriptFight,
                    Proxy = proxy2,
                    ScriptIsRunning = true,
                    Server = ServerEnum.Imagiro,
                    IsConnected = true,
                    Level = 200,
                    Life = 4411,
                    MaxLife = 5210,
                    Energy = 3141,
                    MaxEnergy = 10000,
                    Experience = 50,
                    ExperienceFloor = 50,
                    ExperienceNextFloor = 100,
                    ExperiencePercent = 50
                };
                var bot7 = new Bot()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "jeanfumeunlong@jeanmail.fr",
                    Password = "Boubout0",
                    Name = "Jean-roulhun",
                    Description = "Bot de test n°7",
                    Breed = BreedEnum.Enutrof,
                    Gender = "Male",
                    Script = scriptGather,
                    ScriptIsRunning = false,
                    Proxy = proxy2,
                    Server = ServerEnum.Draconiros,
                    IsConnected = true,
                    Level = 200,
                    Life = 4411,
                    MaxLife = 5210,
                    Energy = 3141,
                    MaxEnergy = 10000,
                    Experience = 50,
                    ExperienceFloor = 50,
                    ExperienceNextFloor = 100,
                    ExperiencePercent = 50
                };
                var bot8 = new Bot()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "jeanhehee@jeanmail.fr",
                    Password = "Boubout0",
                    Name = "Jean-HeeHee",
                    Description = "Bot de test n°8",
                    Breed = BreedEnum.Sadida,
                    Gender = "Female",
                    Script = scriptGather,
                    ScriptIsRunning = true,
                    Proxy = proxy2,
                    Server = ServerEnum.Draconiros,
                    IsConnected = true,
                    Level = 200,
                    Life = 4411,
                    MaxLife = 5210,
                    Energy = 3141,
                    MaxEnergy = 10000,
                    Experience = 50,
                    ExperienceFloor = 50,
                    ExperienceNextFloor = 100,
                    ExperiencePercent = 50
                };

                context.Bots.AddRange(bot1, bot2, bot3, bot4, bot5, bot6, bot7, bot8);
                Console.WriteLine("Seeding Bots done...");

                Console.WriteLine("Adding data - seeding Inventories...");
                context.Inventories.AddRange(
                    new Inventory()
                    {
                        BotId = bot1.Id,
                        Kamas = 1000000,
                        Type = InventoryTypeEnum.Bag
                    },
                    new Inventory()
                    {
                        BotId = bot2.Id,
                        Kamas = 2000000,
                        Type = InventoryTypeEnum.Bag
                    },
                    new Inventory()
                    {
                        BotId = bot2.Id,
                        Kamas = 30000000,
                        Type = InventoryTypeEnum.Bank
                    },
                    new Inventory()
                    {
                        BotId = bot1.Id,
                        Kamas = 40000000,
                        Type = InventoryTypeEnum.Bank
                    },
                    new Inventory()
                    {
                        BotId = bot3.Id,
                        Kamas = 1130000000,
                        Type = InventoryTypeEnum.Bank
                    },
                    new Inventory()
                    {
                        BotId = bot3.Id,
                        Kamas = 420,
                        Type = InventoryTypeEnum.Bag
                    },
                    new Inventory()
                    {
                        BotId = bot4.Id,
                        Kamas = 424580,
                        Type = InventoryTypeEnum.Bag
                    },
                    new Inventory()
                    {
                        BotId = bot5.Id,
                        Kamas = 1000000,
                        Type = InventoryTypeEnum.Bag
                    },
                    new Inventory()
                    {
                        BotId = bot6.Id,
                        Kamas = 2000000,
                        Type = InventoryTypeEnum.Bag
                    },
                    new Inventory()
                    {
                        BotId = bot7.Id,
                        Kamas = 7444120,
                        Type = InventoryTypeEnum.Bag
                    },
                    new Inventory()
                    {
                        BotId = bot8.Id,
                        Kamas = 424580,
                        Type = InventoryTypeEnum.Bag
                    }
                );

                Console.WriteLine("Seeding Inventories done...");

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("No seeding required...");
            }



            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
