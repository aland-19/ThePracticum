﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using Application;

namespace GrosvenorInHousePracticum
{
    class Program
    {
        static void Main()
        {
            var server = new Server(new DishManager());
            while (true)
            {
                Console.WriteLine("Please provide your order:");
                var unparsedOrder = Console.ReadLine();
                var output = server.TakeOrder(unparsedOrder);

                string TimeOfDay = Console.ReadLine();
                
                if (TimeOfDay == "Morning" || TimeOfDay == "morning" || TimeOfDay == "Evening" || TimeOfDay == "evening");
                {
                    Console.WriteLine($"{TimeOfDay},");
                }
            }
        }

        public static Menu BuildMenu()
        {
            var morningItems = new List<MenuItems>();
            
            morningItems.Add(new MenuItems
            {
                ItemNumber = 1,
                DishName = "Egg",
                AllowsMany = false
            });
                        
            morningItems.Add(new MenuItems
            {
                ItemNumber = 2,
                DishName = "Toast",
                AllowsMany = false
            });  
            
            morningItems.Add(new MenuItems
            {
                ItemNumber = 3,
                DishName = "Coffee",
                AllowsMany = true
            });
            
            var menu = new Menu();
            menu.Meals.Add("morning", morningItems);
            menu.Meals.Add("evening", new List<MenuItems> {});

            var eveningItems = new List<MenuItems>();

            eveningItems.Add(new MenuItems
            {
                ItemNumber = 1,
                DishName = "Steak",
                AllowsMany = false
            });

            eveningItems.Add(new MenuItems
            {
                ItemNumber = 2,
                DishName = "Potato",
                AllowsMany = true
            });

            eveningItems.Add(new MenuItems
            {
                ItemNumber = 3,
                DishName = "Wine",
                AllowsMany = false
            });

            eveningItems.Add(new MenuItems
            {
                ItemNumber = 4,
                DishName = "Cake",
                AllowsMany = false
            });
            
            return menu;
        }
    }
}
