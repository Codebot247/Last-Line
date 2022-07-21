using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{

    [Serializable]
    class Resource
    {
        public string resourceName;
        public int conversionRate;
        public int convertsTo; //Which useable resource it refines to, like 1 for repair materials
        public int sellsFor;
        public int total;
        public int maxAmount;


        public void displayInventory()
        {
            Console.WriteLine(resourceName + " " + total + "/" + maxAmount);
        }

        public string[] returnResource()
        {
            string[] returnResourceString = { total.ToString(),maxAmount.ToString() };
            return returnResourceString;
        }

    }

    [Serializable]
    class Ship
    {
        public float fuel;
        public float fuelMax;
        public int crew;
        public float engineStatus;
        public float shieldStatus;
        public float hullStatus;
        public float atmosphereStatus;
        public int repairMaterials;
        public int money;

        public bool engineShutdown;
        public bool engineBroken;
        public int engineStopsToDamage;
        public bool shieldShutdown;
        public int engineWaitTime;
        public int speed;

        public string[] returnShip()
        {
            string[] returnShipString = { fuel.ToString(), fuelMax.ToString(), crew.ToString(), engineStatus.ToString(), shieldStatus.ToString(),
                    hullStatus.ToString(), atmosphereStatus.ToString(), repairMaterials.ToString(), money.ToString(), engineShutdown.ToString(),
                    engineBroken.ToString(),engineStopsToDamage.ToString(), shieldShutdown.ToString(), engineWaitTime.ToString(), speed.ToString() };
            return returnShipString;
        }
    }

    class Scenario
    {
        public bool asteroidField;
        public int timeToAsteroidImpact;
        public int progressToNextLevel;

        public bool combatArea;
        public bool scrapShipArea;
        public bool nebulaArea;
        public string[] returnScenario()
        {
            string[] returnScenarioString = { asteroidField.ToString(), timeToAsteroidImpact.ToString(),
                progressToNextLevel.ToString(),combatArea.ToString(),scrapShipArea.ToString(),
                nebulaArea.ToString()
            };
            return returnScenarioString;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            // INITIALIZING RESOURCES
            Resource Minerals = new Resource();
            Minerals.resourceName = "Minerals";
            Minerals.conversionRate = 20;
            Minerals.convertsTo = 1;
            Minerals.sellsFor = 5;
            Minerals.total = 0;
            Minerals.maxAmount = 100;

            Resource ExoticGas = new Resource();
            ExoticGas.resourceName = "Exotic Gas";
            ExoticGas.conversionRate = 50;
            ExoticGas.convertsTo = 2;
            ExoticGas.sellsFor = 25;
            ExoticGas.total = 0;
            ExoticGas.maxAmount = 100;




            Ship playerShip = new Ship();
            playerShip.fuel = 100.0f;
            playerShip.fuelMax = 100.0f;
            playerShip.crew = 50;
            playerShip.engineStatus = 100.0f;
            playerShip.shieldStatus = 100.0f;
            playerShip.hullStatus = 100.0f;
            playerShip.atmosphereStatus = 100.0f;
            playerShip.repairMaterials = 50;
            playerShip.money = 50;

            playerShip.engineShutdown = true;
            playerShip.engineBroken = false;
            playerShip.engineStopsToDamage = 3;
            playerShip.shieldShutdown = true;
            playerShip.engineWaitTime = -1;
            playerShip.speed = 0;

            int temporaryVar;


            int chosenCampaign = 1;
            bool gameStart = false;


            // Event / situation affectors
            Scenario startingScenario = new Scenario();
            startingScenario.asteroidField = false;
            startingScenario.timeToAsteroidImpact = -1;
            startingScenario.progressToNextLevel = 500;

            startingScenario.combatArea = false;
            startingScenario.scrapShipArea = false;
            startingScenario.nebulaArea = false;

            int scenario = 1;

            Console.Clear();
            while (true)
            {
                if (gameStart == false)
                {
                    Console.WriteLine("WELCOME, ENGINEER");
                    Console.WriteLine("PLEASE SELECT A POSITION");
                    Console.WriteLine("1. INNER RING TRADING SHIP (EASY)");
                    Console.WriteLine("L. LOAD GAME");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "1")
                    {
                        playerShip.repairMaterials = 150;
                        playerShip.crew = 100;
                    }
                    if (choice == "l")
                    {
                        string[] shipVariables = System.IO.File.ReadAllLines(@"S_P1.txt");
                        playerShip.fuel = float.Parse(shipVariables[0]);
                        playerShip.fuelMax = float.Parse(shipVariables[1]);
                        playerShip.crew = Int32.Parse(shipVariables[2]);
                        playerShip.engineStatus = float.Parse(shipVariables[3]);
                        playerShip.shieldStatus = float.Parse(shipVariables[4]);
                        playerShip.hullStatus = float.Parse(shipVariables[5]);
                        playerShip.atmosphereStatus = float.Parse(shipVariables[6]);
                        playerShip.repairMaterials = Int32.Parse(shipVariables[7]);
                        playerShip.money = Int32.Parse(shipVariables[8]);

                        playerShip.engineShutdown = bool.Parse(shipVariables[9]);
                        playerShip.engineBroken = bool.Parse(shipVariables[10]);
                        playerShip.engineStopsToDamage = int.Parse(shipVariables[11]);
                        playerShip.shieldShutdown = bool.Parse(shipVariables[12]);
                        playerShip.engineWaitTime = int.Parse(shipVariables[13]);
                        playerShip.speed = int.Parse(shipVariables[14]);


                        string[] scenarioVariables = System.IO.File.ReadAllLines(@"S_S1.txt");
                        startingScenario.asteroidField = bool.Parse(scenarioVariables[0]);
                        startingScenario.timeToAsteroidImpact = int.Parse(scenarioVariables[1]);
                        startingScenario.progressToNextLevel = int.Parse(scenarioVariables[2]);

                        startingScenario.combatArea = bool.Parse(scenarioVariables[3]);
                        startingScenario.scrapShipArea = bool.Parse(scenarioVariables[4]);
                        startingScenario.nebulaArea = bool.Parse(scenarioVariables[5]);



                        string[] resourceVariables = System.IO.File.ReadAllLines(@"S_R1.txt");
                        Minerals.total = Int32.Parse(resourceVariables[0]);
                        Minerals.maxAmount = Int32.Parse(resourceVariables[1]);

                        ExoticGas.total = Int32.Parse(resourceVariables[2]);
                        ExoticGas.maxAmount = Int32.Parse(resourceVariables[3]);
                    }

                    Console.Clear();
                }

                Console.WriteLine("Press enter to progress time");

                while (true)
                {

                    if (startingScenario.progressToNextLevel < 0)
                    {
                        Console.WriteLine("You reached your destination safely!\nThank you for playing the demo of my game!\nPlease send feedback to codebot247@gmail.com !\n");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        return;
                    }


                    /*                                   GAMEOVER CONDITIONS                        */

                    if (playerShip.atmosphereStatus <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("CRITICAL DECOMPRESSION - SHIP DAMAGED BEYOND REPAIR.");
                        Console.WriteLine("PRESS ENTER TO CONTINUE.");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Due to being unable to manage keeping the hull intact the ship has depressurized and suffocated the playerShip.crew.\n You cling on to the last shreds of oxygen you can before you too asphyxiate, lost to the void.\n Press enter to continue.");
                        Console.ReadLine();
                        return;
                    }
                    if (playerShip.crew <= 0)
                    {
                        Console.WriteLine("The playerShip.crew by some means and inaction of yours has died.\n You're left stranded and hepless in a floating coffin as the last of the life support and systems fail.\n You wonder what you could have done better.\n Press enter to continue,");
                        Console.ReadLine();
                        return;
                    }

                    /*========================================================================================================*/

                    var rand = new Random();







                    // SYSTEM WARNINGS

                    if (playerShip.engineStatus <= 50.0 && playerShip.engineStatus > 25.0)
                    {
                        Console.WriteLine("!! ENGINE STATUS WARNING !!");
                    }
                    else if (playerShip.engineStatus <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL ENGINE STATUS WARNING !!");
                    }

                    if (playerShip.shieldStatus <= 50.0 && playerShip.shieldStatus > 25.0)
                    {
                        Console.WriteLine("!! SHIELD CHARGE WARNING !!");
                    }
                    else if (playerShip.shieldStatus <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL SHIELD CHARGE WARNING !!");
                    }


                    if (playerShip.atmosphereStatus <= 50.0 && playerShip.atmosphereStatus > 25.0)
                    {
                        Console.WriteLine("!! ATMOSPHERIC SAFETY WARNING !!");
                    }
                    else if (playerShip.atmosphereStatus <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL ATMOSPHERIC SAFETY WARNING !!");
                    }

                    if (playerShip.hullStatus <= 50.0 && playerShip.hullStatus > 25.0)
                    {
                        Console.WriteLine("!! MAJOR HULL DAMAGE !!");
                    }
                    else if (playerShip.hullStatus <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL HULL DAMAGE !!");
                    }

                    if (playerShip.fuel <= 50.0 && playerShip.fuel > 25.0)
                    {
                        Console.WriteLine("!! LOW FUEL WARNING !!");
                    }
                    else if (playerShip.fuel <= 25.0)
                    {
                        Console.WriteLine("!! CRITICAL FUEL WARNING !!");
                    }











                    // WAIT TIME FOR ENGINE RESTART
                    if (playerShip.engineWaitTime > 0)
                    {
                        playerShip.engineWaitTime -= 1;
                    }

                    if (playerShip.engineShutdown == true && playerShip.engineBroken == true)
                    {
                        if (playerShip.engineWaitTime == 0)
                        {
                            playerShip.speed = 1;
                            playerShip.engineWaitTime = -1;
                            playerShip.engineShutdown = false;
                            Console.WriteLine("!! ENGINE RESTARTED !!");
                        }
                    }


                    // PROGRESSION TO NEXT LEVEL
                    startingScenario.progressToNextLevel -= playerShip.speed;

                    // SYSTEM DEGREDATION

                    if (rand.Next(0, 10) > 8)
                    {
                        playerShip.engineStatus -= ((float)rand.NextDouble() * (1.25f * playerShip.speed)); // Engine
                    }

                    if (!playerShip.shieldShutdown)
                    {
                        playerShip.shieldStatus -= 4.0f + (float)rand.NextDouble(); // Shields
                    }

                    if (playerShip.atmosphereStatus <= 50.0)
                    {
                        if (rand.Next(0, 10) > 4)
                        {
                            playerShip.crew -= rand.Next(1, 3 * (int)((100.0 - playerShip.atmosphereStatus) / 10));   // playerShip.crew death
                        }
                    }

                    if (playerShip.hullStatus <= 75.0)
                    {
                        playerShip.atmosphereStatus -= ((100.0f - playerShip.hullStatus) / 11);
                    }

                    playerShip.fuel -= 0.15f * playerShip.speed; // Fuel


                    // EVENT CHECKS 

                    if (startingScenario.asteroidField == true && !playerShip.engineShutdown)
                    {
                        if (rand.Next(0, 10) > 8)
                        {
                            startingScenario.timeToAsteroidImpact = rand.Next(3, 4);
                        }
                    }

                    if (startingScenario.timeToAsteroidImpact > 0 && !playerShip.engineShutdown)
                    {
                        Console.WriteLine("PROXIMITY WARNING - SHIELD RECOMMENDED");
                        startingScenario.timeToAsteroidImpact -= 1;
                        if (startingScenario.timeToAsteroidImpact == 0)
                        {
                            startingScenario.timeToAsteroidImpact = -1;
                            if (playerShip.shieldShutdown == true)
                            {
                                playerShip.hullStatus -= rand.Next(3, 8);
                                Console.WriteLine("HULL HIT - ASSESS DAMAGE");
                            }
                            else
                            {
                                playerShip.shieldStatus -= rand.Next(3, 8);
                                Console.WriteLine("SHIELD HIT - MONITOR CHARGE");
                            }
                        }
                    }


                    // SHIELD CHARGE / SHUTDOWN CHECK 
                    if (playerShip.shieldStatus < 0)
                    {
                        playerShip.shieldStatus = 0;
                        playerShip.shieldShutdown = true;
                    }
                    if (playerShip.shieldShutdown == true)
                    {
                        playerShip.shieldStatus += 5.5f;
                        if (playerShip.shieldStatus > 100.0f)
                        {
                            playerShip.shieldStatus = 100.0f;
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

                        if (playerShip.engineShutdown != true)
                        {

                            speedToChangeTo += option[6];

                            if (Int32.Parse((string)speedToChangeTo) > 5)
                            {
                                playerShip.speed = 5;
                            }
                            else if (Int32.Parse(speedToChangeTo) < 1)
                            {
                                Console.WriteLine("CANNOT STOP THE ENGINE WHILE HOT. DISABLE ENGINE TO STOP.");
                                Console.WriteLine("!! STOPPING ENGINE FREQUENTLY CAN CAUSE DAMAGE !!");
                                playerShip.speed = 1;
                            }
                            else
                            {
                                playerShip.speed = Int32.Parse(speedToChangeTo);
                            }

                            if (playerShip.speed > 4 && playerShip.fuel <= 25.0 || playerShip.speed > 4 && playerShip.engineStatus <= 25.0)
                            {
                                playerShip.speed = 4;
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
                        Console.WriteLine("Press enter to progress time");
                    }

                    if (option == "help")
                    {
                        Console.Write("\n\n");
                        Console.WriteLine("STATUS              Displays the status of ship systems.");
                        Console.WriteLine("RESTART ENGINE      Restarts the engine in case of a shutdown. Will time to come back online.");
                        Console.WriteLine("SPEED #             Sets the engine speed to the value given in #, the higher the speed the faster the engine degrades and uses more fuel.");
                        Console.WriteLine("SHIELD              Toggles the shields on and off.");
                        Console.WriteLine("ENGINE              Toggles the engine on and off.");
                        Console.WriteLine("REPAIR ENGINE       Repairs the engine, consuming repair materials in the process.");
                        Console.WriteLine("REPAIR HULL         Repairs the hull, consuming repair materials in the process.");
                        Console.WriteLine("MINE                Activates mining drones at the expense of 5 fuel to harvest minerals from asteroids.");
                        Console.WriteLine("EVADE               Avoids impact from enemy fire or asteroids, but consumes fuel in the process.");
                        Console.WriteLine("INVENTORY           Displays collected resoruces.");
                        Console.WriteLine("CLEAR               Clears the console.");
                        Console.Write("\n\n");

                    }


                    if (option == "save")
                    {
                        using (StreamWriter writer = new StreamWriter("S_P1.txt"))
                        {
                            foreach (string line in playerShip.returnShip())
                            {
                                writer.WriteLine(line);                      
                            }

                        }

                        using (StreamWriter writer = new StreamWriter("S_S1.txt"))
                        {
                            foreach (string line in startingScenario.returnScenario())
                            {
                                writer.WriteLine(line);
                            }

                        }

                        using (StreamWriter writer = new StreamWriter("S_R1.txt"))
                        {
                            foreach (string line in Minerals.returnResource())
                            {
                                writer.WriteLine(line);
                            }
                            foreach (string line in ExoticGas.returnResource())
                            {
                                writer.WriteLine(line);
                            }

                        }

                    }

                    if (option == "inventory")
                    {
                        Console.WriteLine("\n");
                        Minerals.displayInventory();
                        ExoticGas.displayInventory();
                        Console.WriteLine("\n");
                    }

                    if (option == "evade")
                    {
                        if (startingScenario.timeToAsteroidImpact > 0)
                        {
                            temporaryVar = rand.Next(2, 6);
                            startingScenario.timeToAsteroidImpact = -1;
                            playerShip.fuel -= temporaryVar;
                            Console.WriteLine("!! SUCCESSFULLY AVOIDED OBJECT !!");
                            Console.WriteLine(temporaryVar + " FUEL CONSUMED IN EVASION");

                        }
                        else
                        {
                            Console.WriteLine("NOTHING TO EVADE");
                        }
                    }

                    // MINING ASTEROIDS
                    if (option == "mine")
                    {
                        if (startingScenario.timeToAsteroidImpact > 0)
                        {
                            temporaryVar = rand.Next(10, 15);
                            playerShip.fuel -= 5.0f;
                            Minerals.total += temporaryVar;
                            Console.WriteLine("MINERALS HARVESTED: " + temporaryVar);
                            if (Minerals.total > Minerals.maxAmount)
                            {
                                Minerals.total = Minerals.maxAmount;
                            }
                        }
                        else
                        {
                            Console.WriteLine("NOT IN PROXIMITY TO ASTEROID");
                        }
                    }

                    if (option == "engine")
                    {
                        if (playerShip.engineBroken)
                        {
                            Console.WriteLine("ENGINE REQUIRES RESTART");
                        }



                        else
                        {
                            if (playerShip.engineShutdown == true)
                            {
                                if (playerShip.engineStopsToDamage == 0)
                                {
                                    playerShip.engineStatus -= 5.0f;
                                }
                                playerShip.speed = 1;
                            }
                            playerShip.engineShutdown = !playerShip.engineShutdown;
                        }
                    }


                    if (option == "repair hull")
                    {
                        if (playerShip.hullStatus < 100.0)
                        {
                            if (playerShip.repairMaterials >= ((100 - playerShip.hullStatus)))
                            {
                                playerShip.repairMaterials -= (int)((100 - playerShip.hullStatus));
                                playerShip.hullStatus += ((100 - playerShip.hullStatus));

                                if (playerShip.hullStatus > 100.0)
                                {
                                    playerShip.hullStatus = 100;
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
                            if (playerShip.engineStatus < 100.0)
                            {
                                if (playerShip.repairMaterials >= ((100 - playerShip.engineStatus) / 2))
                                {
                                    playerShip.repairMaterials -= (int)((100 - playerShip.engineStatus) / 2);
                                    playerShip.engineStatus += ((100 - playerShip.engineStatus));

                                    if (playerShip.engineStatus > 100.0)
                                    {
                                        playerShip.engineStatus = 100;
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

                    if (option == "restart engine" && playerShip.engineShutdown == true && playerShip.engineBroken == true && playerShip.engineWaitTime < 0)
                    {
                        playerShip.engineWaitTime = 2;
                    }


                    // SHIELD //

                    if (option == "shield")
                    {
                        if (playerShip.shieldStatus >= 25.0 && playerShip.shieldShutdown == true)
                        {
                            playerShip.shieldShutdown = false;
                        }
                        else if (playerShip.shieldShutdown == false)
                        {
                            playerShip.shieldShutdown = true;
                        }
                        else
                        {
                            Console.WriteLine("SHIELD MUST HAVE A MINIMUM CHARGE OF 25.0% TO ENABLE");
                        }
                    }




                    // STATUS //

                    if (option == "status")
                    {
                        Console.Write("\n");
                        Console.WriteLine("Fuel: " + playerShip.fuel.ToString("0.00") + "%" + "/" + playerShip.fuelMax.ToString("0.00") + "%");
                        Console.WriteLine("Atmospheric Status: " + playerShip.atmosphereStatus.ToString("0.00") + "%");
                        Console.WriteLine("Hull Condition:  " + playerShip.hullStatus.ToString("0.00") + "%");
                        Console.Write("Engine Condition: " + playerShip.engineStatus.ToString("0.00") + "%");
                        if (playerShip.engineShutdown == true)
                        {
                            Console.Write(" OFFLINE\n");
                        }
                        else
                        {
                            Console.Write(" ONLINE\n");
                        }
                        Console.WriteLine("Speed: " + playerShip.speed);

                        Console.Write("Shield Charge: " + playerShip.shieldStatus.ToString("0.00") + "%");
                        if (playerShip.shieldShutdown == true)
                        {
                            Console.Write(" OFFLINE\n");
                        }
                        else
                        {
                            Console.Write(" ONLINE\n");
                        }
                        Console.WriteLine("Repair Materials: " + playerShip.repairMaterials);
                        Console.WriteLine("Crew: " + playerShip.crew);

                        Console.WriteLine("\nDistance left: " + startingScenario.progressToNextLevel);
                        Console.Write("\n\n");
                    }
                }
            }
        }
    }
}
