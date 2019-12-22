using CoverallsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoverallsWeb
{
    public static class Data
    {
        public static void Initialize(CoverallsContext context)
        {
            if (!context.Types.Any())
            {
                context.Types.AddRange(
                    new CoverallsType
                    {
                        Name = "Верхняя одежда"
                    },
                    new CoverallsType
                    {
                        Name = "Обувь"
                    }
                );
                context.SaveChanges();
            }
            if (!context.Coveralls.Any())
            {
                context.Coveralls.AddRange(
                    new Coveralls
                    {
                        Name = "Куртка утеплённая",
                        Type = context.Types.Where(x => x.Name == "Верхняя одежда").First(),
                        Size = 56,
                        Height = 180
                    },
                    new Coveralls
                    {
                        Name = "Куртка для руководителя",
                        Type = context.Types.Where(x => x.Name == "Верхняя одежда").First(),
                        Size = 45,
                        Height = 176
                    },
                    new Coveralls
                    {
                        Name = "Ботинки мужские утеплённые",
                        Type = context.Types.Where(x => x.Name == "Обувь").First(),
                        Size = 43
                    },
                    new Coveralls
                    {
                        Name = "Ботинки кирзовые",
                        Type = context.Types.Where(x => x.Name == "Обувь").First(),
                        Size = 44
                    },
                    new Coveralls
                    {
                        Name = "Жилет защитный",
                        Type = context.Types.Where(x => x.Name == "Верхняя одежда").First(),
                        Size = 45,
                        Height = 179
                    }

                );
                context.SaveChanges();
            }
            if (!context.Storage.Any())
            {
                context.Storage.AddRange(
                    new Storage
                    {
                        Coveralls = context.Coveralls.Where(x => x.Name == "Куртка утеплённая").First(),
                        Count = 56
                    },
                    new Storage
                    {
                        Coveralls = context.Coveralls.Where(x => x.Name == "Куртка для руководителя").First(),
                        Count = 5
                    },
                    new Storage
                    {
                        Coveralls = context.Coveralls.Where(x => x.Name == "Ботинки кирзовые").First(),
                        Count = 34
                    },
                    new Storage
                    {
                        Coveralls = context.Coveralls.Where(x => x.Name == "Ботинки мужские утеплённые").First(),
                        Count = 20
                    },
                    new Storage
                    {
                        Coveralls = context.Coveralls.Where(x => x.Name == "Жилет защитный").First(),
                        Count = 15
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
