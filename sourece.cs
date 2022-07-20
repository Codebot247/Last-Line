using System;


namespace ConsoleApp1
{

    class Resource
    {
        public string resourceName;
        public int conversionRate;
        public int convertsTo; //Which useable resource it refines to, like 1 for repair materials
        public int total;
        public int maxAmount;

        public void displayInventory()
        {
            Console.WriteLine("Resource: " + resourceName );
            Console.WriteLine("Total: " + total);
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            // INITIALIZING RESOURCES
            Resource Minerals = new Resource();
            Minerals.resourceName = "Minerals";
            Minerals.conversionRate = 10;
            Minerals.convertsTo = 1;
            Minerals.total = 0;
            Minerals.maxAmount = 0;








            Minerals.displayInventory();

            Console.ReadLine();


            float fuel = 100.0f;
            int crew = 50;
            float engineStatus = 100.0f;
            float shieldStatus = 100.0f;
            float hullStatus = 100.0f;
            float atmosphereStatus = 100.0f;
            int repairMaterials = 50;

            bool engineShutdown = true;
            bool engineBroken = false;
            bool shieldShutdown = true;
            int engineWaitTime = -1;
            int speed = 0;

            // Minerals, Exotic Gas, 
            int[] resources = { 0, 0 };

            int progressToNextLevel = 120;
            int chosenCampaign = 1;
            bool gameStart = false;


            // Event / situation affectors
            bool asteroidField = true;
            int timeToAsteroidImpact = -1;

            bool combatArea = false;
            bool scrapShipArea = false;
            bool nebulaArea = false;
            

            Console.Clear();
            while (true)
            {
                if (gameStart == false)
                {
                    Console.WriteLine("WELCOME, ENGINEER");
                    Console.WriteLine("PLEASE SELECT A POSITION");
                    Console.WriteLine("1. INNER RING TRADING SHIP (EASY)");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        repairMaterials = 150;
                        crew = 100;
                    }

                    Console.Clear();
                }

                

                while (true)
                {
                    // GAMEOVER CONDITIONS

                    if(atmosphereStatus <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("CRITICAL DECOMPRESSION - SHIP DAMAGED BEYOND REPAIR.");
                        Console.WriteLine("PRESS ENTER TO CONTINUE.");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Due to being unable to manage keeping the hull intact the ship has depressurized and suffocated the crew.\n You cling on to the last shreds of oxygen you can before you too asphyxiate, lost to the void.\n Press enter to continue.");
                        Console.ReadLine();
                        return;
                    }
                    if (crew <= 0)
                    {
                        Console.WriteLine("The crew by some means and inaction of yours has died.\n You're left stranded and hepless in a floating coffin as the last of the life support and systems fail.\n You wonder what you could have done better.\n Press enter to continue,");
                        Console.ReadLine();
                    }


                    var rand = new Random();

                    // SYSTEM WARNINGS

                    if (engineStatus <= 50.0 && engineStatus > 25.0)
                    {
                        Console.WriteLine("!! ENGINE STATUS WARNING !!");
                    }
                    else if (engineStatus <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL ENGINE STATUS WARNING !!");
                    }

                    if (shieldStatus <= 50.0 && shieldStatus > 25.0)
                    {
                        Console.WriteLine("!! SHIELD CHARGE WARNING !!");
                    }
                    else if (shieldStatus <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL SHIELD CHARGE WARNING !!");
                    }


                    if (atmosphereStatus <= 50.0 && atmosphereStatus > 25.0)
                    {
                        Console.WriteLine("!! ATMOSPHERIC SAFETY WARNING !!");
                    }
                    else if (atmosphereStatus <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL ATMOSPHERIC SAFETY WARNING !!");
                    }

                    if (hullStatus <= 50.0 && hullStatus > 25.0)
                    {
                        Console.WriteLine("!! MAJOR HULL DAMAGE !!");
                    }
                    else if (hullStatus <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL HULL DAMAGE !!");
                    }

                    if (fuel <= 50.0 && fuel > 25.0)
                    {
                        Console.WriteLine("!! LOW FUEL WARNING !!");
                    }
                    else if (fuel <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL FUEL WARNING !!");
                    }


                    // WAIT TIME FOR ENGINE RESTART
                    if (engineWaitTime > 0)
                    {
                        engineWaitTime -= 1;
                    }

                    if (engineShutdown == true && engineBroken == true)
                    {
                        if (engineWaitTime == 0)
                        {
                            speed = 1;
                            engineWaitTime = -1;
                            engineShutdown = false;
                            Console.WriteLine("!! ENGINE RESTARTED !!");
                        }
                    }


                    // PROGRESSION TO NEXT LEVEL
                    progressToNextLevel -= speed;

                    // SYSTEM DEGREDATION

                    engineStatus -= ((float)rand.NextDouble() * (0.5f * speed)); // Engine

                    if (!shieldShutdown){
                        shieldStatus -= 4.0f + (float)rand.NextDouble(); // Shields
                    }

                    if (atmosphereStatus <= 50.0)
                    {
                        if (rand.Next(0, 10) > 4)
                        {
                            crew -= rand.Next(1, 3 * (int)((100.0 - atmosphereStatus) / 10));   // Crew death
                        }
                    }

                    if (hullStatus <= 75.0)
                    {
                        atmosphereStatus -= ((100.0f - hullStatus) / 11);
                    }

                    fuel -= 0.15f * speed; // Fuel


                    // EVENT CHECKS 
                    if (asteroidField == true)
                    {
                        if (rand.Next(0, 10) > 8)
                        {
                            timeToAsteroidImpact = rand.Next(3, 4);
                        }
                    }
                    
                    if (timeToAsteroidImpact > 0)
                    {
                        Console.WriteLine("PROXIMITY WARNING - SHIELD RECOMMENDED");
                        timeToAsteroidImpact -= 1;
                        if (timeToAsteroidImpact == 0)
                        {
                            timeToAsteroidImpact = -1;
                            if (shieldShutdown == true)
                            {
                                hullStatus -= rand.Next(3, 8);
                                Console.WriteLine("HULL HIT - ASSESS DAMAGE");
                            }
                            else
                            {
                                shieldStatus -= rand.Next(3, 8);
                                Console.WriteLine("SHIELD HIT - MONITOR CHARGE");
                            }
                        }
                    }


                    // SHIELD CHARGE / SHUTDOWN CHECK 
                    if (shieldStatus < 0)
                    {
                        shieldStatus = 0;
                        shieldShutdown = true;
                    }
                    if (shieldShutdown == true)
                    {
                        shieldStatus += 5.5f;
                        if (shieldStatus > 100.0f)
                        {
                            shieldStatus = 100.0f;
                        }
                    }

                    Console.Write(">");

                    string option = Console.ReadLine().ToLower();
                    string optionProcessed = "";
                    string speedToChangeTo = "";

                    // Commands
                    try
                    {
                        for (int i = 0; i != 5; i++)
                        {
                            optionProcessed += option[i];
                        }
                    }
                    catch
                    {
                        while (true)
                        {
                            break;
                        }
                    }

                    if (optionProcessed == "speed")
                    {

                        if (engineShutdown != true)
                        {

                            speedToChangeTo += option[6];

                            if (Int32.Parse((string)speedToChangeTo) > 5)
                            {
                                speed = 5;
                            }
                            else if (Int32.Parse(speedToChangeTo) < 0)
                            {
                                speed = 0;
                            }
                            else
                            {
                                speed = Int32.Parse(speedToChangeTo);
                            }

                            if (speed > 4 && fuel <= 25.0 || speed > 4 && engineStatus <= 25.0)
                            {
                                speed = 4;
                                Console.WriteLine("FOR CREW AND SHIP SAFETY SPEED CANNOT EXCEED SAFE PARAMETERS WHILE FUEL IS LOW OR ENGINE IS DAMAGED");
                            }
                        }
                        else
                        {
                            Console.WriteLine("YOU CANNOT CHANGE SPEED WHILE THE ENGINE IS OFFLINE");
                        }
                    }

                    if (option == "clear")
                    {
                        Console.Clear();
                    }

                    if (option == "help")
                    {
                        Console.WriteLine("STATUS      Displays the status of ship systems.");
                        Console.WriteLine("RESTART ENGINE      Restarts the engine in case of a shutdown. Will time to come back online.");
                        Console.WriteLine("SPEED #      Sets the engine speed to the value given in #, the higher the speed the faster the engine degrades and uses more fuel.");
                        Console.WriteLine("SHIELD      Toggles the shields on and off.");
                        Console.WriteLine("ENGINE      Toggles the engine on and off.");
                        Console.WriteLine("REPAIR ENGINE     Repairs the engine a bit, consuming repair materials in the process.");
                        Console.WriteLine("CLEAR      Clears the console.");

                    }

                    // REPAIR ENGINE //

                    if (option == "inventory")
                    {
                        Minerals.displayInventory();
                    }

                    if (option == "engine")
                    {
                        if (engineBroken)
                        {
                            Console.WriteLine("ENGINE REQUIRES RESTART");
                        }
                        else
                        {
                            engineShutdown = !engineShutdown;
                        }
                    }


                    if (option == "repair hull")
                    {
                        if (hullStatus < 100.0)
                        {
                            if (repairMaterials >= ((100 - hullStatus)))
                            {
                                repairMaterials -= (int)((100 - hullStatus));
                                hullStatus += ((100 - hullStatus));

                                if (hullStatus > 100.0)
                                {
                                    hullStatus = 100;
                                }

                                Console.WriteLine("HULL REPAIRED");
                            }

                            else
                            {
                                Console.WriteLine("!! YOU LACK SUFFICIENT MATERIALS TO FIX IT !!");

                            }
                        }


                        if (option == "repair engine")
                        {
                            if (engineStatus < 100.0)
                            {
                                if (repairMaterials >= ((100 - engineStatus) / 2))
                                {
                                    repairMaterials -= (int)((100 - engineStatus) / 2);
                                    engineStatus += ((100 - engineStatus));

                                    if (engineStatus > 100.0)
                                    {
                                        engineStatus = 100;
                                    }

                                    Console.WriteLine("ENGINE REPAIRED");
                                }

                                else
                                {
                                    Console.WriteLine("YOU LACK SUFFICIENT MATERIALS TO FIX IT");

                                }
                            }
                        }
                    }


                    // RESTART ENGINE //

                    if (option == "restart engine" && engineShutdown == true && engineBroken == true && engineWaitTime < 0)
                    {
                        engineWaitTime = 2;
                    }


                    // SHIELD //

                    if (option == "shield" && shieldStatus > 0.0)
                    {
                        shieldShutdown = !(shieldStatus > 0 && shieldShutdown == true);
                    }



                    // STATUS //

                    if (option == "status")
                    {
                        Console.Write("\n");
                        Console.WriteLine("Fuel: " + fuel.ToString("0.00") + "%");
                        Console.WriteLine("Atmospheric Status: " + atmosphereStatus.ToString("0.00") + "%");
                        Console.WriteLine("Hull Condition:  " + hullStatus.ToString("0.00") + "%");
                        Console.Write("Engine Condition: " + engineStatus.ToString("0.00") + "%");
                        if (engineShutdown == true)
                        {
                            Console.Write(" OFFLINE\n");
                        }
                        else
                        {
                            Console.Write(" ONLINE\n");
                        }
                        Console.WriteLine("Speed: " + speed);

                        Console.Write("Shield Charge: " + shieldStatus.ToString("0.00") + "%");
                        if (shieldShutdown == true)
                        {
                            Console.Write(" OFFLINE\n");
                        }
                        else
                        {
                            Console.Write(" ONLINE\n");
                        }
                        Console.WriteLine("Repair Materials: " + repairMaterials);
                        Console.WriteLine("Crew: " + crew);

                        Console.WriteLine("\nDistance left: " + progressToNextLevel);
                    }
                }
            }
        }
    }
}
