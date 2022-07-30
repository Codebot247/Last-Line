using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    public class Resource
    {
        public string resourceName { get; set; }
        public int conversionRate { get; set; }
        public int convertsTo { get; set; } //Which useable resource it refines to, like 1 for repair materials
        public int sellsFor { get; set; }
        public int total { get; set; }
        public int maxAmount { get; set; }

        public Resource(string ResourceName, int ConversionRate, int ConvertsTo, int SellsFor, int Total, int MaxAmount)
        {
            this.resourceName = ResourceName;
            this.conversionRate = ConversionRate;
            this.convertsTo = ConvertsTo;
            this.sellsFor = SellsFor;
            this.total = Total;
            this.maxAmount = MaxAmount;
        }

        public void saveVar(string[] setVars)
        {

            this.total = Int32.Parse(setVars[4]);
            this.maxAmount = Int32.Parse(setVars[5]);

        }

        public void displayInventory()
        {
            Console.WriteLine(resourceName + " " + total + "/" + maxAmount);
        }

        public string[] returnResource()
        {
            string[] returnResourceString = { total.ToString(),maxAmount.ToString() };
            return returnResourceString;
        }

        public string[] resourceVarsList()
        {
            string[] returnResourceString = { resourceName.ToString(), conversionRate.ToString(), 
                convertsTo.ToString(), sellsFor.ToString(), 
                total.ToString(), maxAmount.ToString() };

            return returnResourceString;
        }

        public void setTotal(int amount)
        {
            this.total = amount;
        }
    }

    public class Resources
    {
        public Resource[] resourceList;
        public int totalResources = 0;

        public void addResource(string ResourceName, int ConversionRate, int ConvertsTo, int SellsFor, int Total, int MaxAmount)
        {
            Array.Resize(ref resourceList, totalResources + 1);
            resourceList[totalResources] = (new Resource(ResourceName, ConversionRate, ConvertsTo, SellsFor, Total, MaxAmount));
            totalResources += 1;
        }
        public string[] getResource(int i)
        {
            return resourceList[i].resourceVarsList();
        }
        public string getResourceVar(int i, int j)
        {
            string[] resourceVars = resourceList[i].resourceVarsList();
            return resourceVars[j];
        }
        public void setResourceTotal(int i, int amount)
        {
            resourceList[i].setTotal(amount);
        }
        public void displayPrices()
        {
            for (int i = 0; i != totalResources; i++)
            {
                string[] prices = resourceList[i].resourceVarsList();
                Console.WriteLine(prices[0] + " " + prices[3] + "c");
            }
        }

    }

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
            // INITIALIZING RESOURCES. 1 = repair materials, 2 = fuel
            Resources listOfResources = new Resources();
            listOfResources.addResource("Minerals", 5, 1, 2, 0, 100);
            listOfResources.addResource("Exotic Gas", 5, 2, 20, 0, 100);

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
            startingScenario.progressToNextLevel = 200;

            startingScenario.combatArea = false;
            startingScenario.scrapShipArea = false;
            startingScenario.nebulaArea = false;

            int scenario = 1;


            Console.Clear();

            bool flight = true;
            bool docked = false;
            float difficulty = 1.0f;

            while (true)
            {
                while (gameStart == false)
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
                        gameStart = true;
                        Console.Clear();
                    }
                    else if (choice == "l")
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
                        flight = bool.Parse(scenarioVariables[6]);
                        docked = bool.Parse(scenarioVariables[7]);
                        difficulty = Int32.Parse(scenarioVariables[8]);


                        string[] resourceVariables = System.IO.File.ReadAllLines(@"S_R1.txt");
                        int iterateVariable = 0;
                        for(int i = 0; i != 2; i++)
                        {
                            listOfResources.resourceList[i].total = Int32.Parse(resourceVariables[0+iterateVariable]);
                            listOfResources.resourceList[i].maxAmount = Int32.Parse(resourceVariables[1+iterateVariable]);
                            iterateVariable += 2;
                        }

                        gameStart = true;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a valid choice");
                    }

                    
                }

                while (docked == true)
                {
                    bool shopping = false;
                    {
                        Console.WriteLine("ENTER LIST TO VIEW AVAILABLE COMMANDS");
                        Console.Write(">");
                        string option = Console.ReadLine().ToLower();
                        if (option == "list")
                        {
                            Console.Write("\n\n");
                            Console.WriteLine("NEWS        Displays the latest headline");
                            Console.WriteLine("SHOP        Displays the raw materials shop");
                            Console.WriteLine("FUEL        Refills fuel at 100c for 10% conversion rate");
                            Console.WriteLine("REPAIR      Repairs hull at 80c for 10% conversion rate");
                            Console.WriteLine("CLEAR       Clears the screen");
                            Console.WriteLine("UNDOCK      Undocks from the station");
                            Console.Write("\n\n");
                        }
                        if (option == "clear")
                        {
                            Console.Clear();
                        }
                        if (option == "news")
                        {
                            Console.WriteLine("Still writing stuff, apologies! :)");
                        }
                        if (option == "fuel")
                        {
                            Console.WriteLine("IT WILL COST " + ((100 - playerShip.fuel) * 100) + "c");
                            Console.WriteLine("YOU HAVE: " + playerShip.money + "c");
                            if (playerShip.money < ((100 - playerShip.fuel) * 100)){
                                Console.WriteLine("YOU DO NOT HAVE ENOUGH TO REFUEL. PRESS ENTER TO EXIT");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("PRESS ENTER TO CONFIRM PURCHASE, OR TYPE EXIT TO LEAVE");
                                option = Console.ReadLine();
                                if (option != "exit")
                                {
                                    playerShip.money -= (int)((100 - playerShip.fuel) * 100);
                                    playerShip.fuel = 100.0f;
                                }
                            }
                        }

                        if (option == "repair")
                        {
                            Console.WriteLine("IT WILL COST " + ((100 - playerShip.hullStatus) * 80) + "c");
                            Console.WriteLine("YOU HAVE: " + playerShip.money + "c");
                            if (playerShip.money < ((100 - playerShip.hullStatus) * 80))
                            {
                                Console.WriteLine("YOU DO NOT HAVE ENOUGH TO REPAIR. PRESS ENTER TO EXIT");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("PRESS ENTER TO CONFIRM PURCHASE, OR TYPE EXIT TO LEAVE");
                                option = Console.ReadLine();
                                if (option != "exit")
                                {
                                    playerShip.money -= (int)((100 - playerShip.hullStatus) * 80);
                                    playerShip.hullStatus = 100.0f;
                                    playerShip.engineStatus = 100.0f;
                                }
                            }
                        }


                        if (option == "shop")
                        {
                            shopping = true;
                            while (shopping = true)
                            {
                                playerShip.money = 200;
                                int choice = 0;
                                Console.Clear();
                                Console.WriteLine("Shop currently has no real use! In development! :)");
                                Console.WriteLine("YOU HAVE: " + playerShip.repairMaterials + "c");
                                Console.WriteLine("\n\nSHOP MATERIALS:\n");
                                listOfResources.displayPrices();
                                Console.WriteLine("\nPLEASE ENTER MATERIAL TO BUY (OR TYPE EXIT TO LEAVE): ");
                                Console.Write(">");
                                option = Console.ReadLine().ToLower();
                                if (option == "minerals") {
                                    choice = 0;
                                }
                                else if (option == "exotic gas") {
                                    choice = 1;
                                }
                                else if (option == "exit") { 
                                    shopping = false;
                                    break;
                                }
                                Console.WriteLine("PLEASE ENTER AMOUNT TO BUY: ");
                                Console.Write(">");
                                int amountToBuy = 0;

                                try
                                {
                                    amountToBuy = Int32.Parse(Console.ReadLine());
                                }
                                catch
                                {
                                    Console.WriteLine("INVALID INPUT");
                                }

                                if (amountToBuy * Int32.Parse(listOfResources.getResourceVar(choice, 3)) > playerShip.money)
                                {
                                    Console.WriteLine("YOU DO NOT HAVE ENOUGH TO BUY THAT");
                                    Console.WriteLine("PRESS ENTER TO CONTINUE");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("THAT WILL COST: " + (amountToBuy * Int32.Parse(listOfResources.getResourceVar(choice, 3))) + "c");
                                    Console.WriteLine("PRESS ENTER TO CONTINUE PURCHASE OR TYPE EXIT TO LEAVE");
                                    option = Console.ReadLine();
                                    if (option == "exit")
                                    {
                                        shopping = false;
                                        break;
                                    }
                                    else
                                    {
                                        playerShip.money -= (amountToBuy * Int32.Parse(listOfResources.getResourceVar(choice, 3)));
                                        listOfResources.setResourceTotal(choice, amountToBuy);
                                    }
                                }
                                  
                            }                       
                        }
                        
                        if (option == "undock")
                        {
                            var rand = new Random();
                            if (rand.Next((int)(0*difficulty),20) > 10)
                            {
                                startingScenario.asteroidField = true;
                            }
                            if (rand.Next((int)(0 * difficulty), 40) > 20)
                            {
                                startingScenario.nebulaArea = true;
                            }
                            if (rand.Next((int)(1 * difficulty), 100) > 50){
                                startingScenario.combatArea = true;
                            }
                            startingScenario.progressToNextLevel = rand.Next((int)(100 * difficulty), (int)(400 * difficulty));
                            docked = false;
                            flight = true;
                        }
                    }
                }
                Console.WriteLine("Press enter to progress time");
                while (flight = true)
                {
                    var rand = new Random();

                    if (startingScenario.progressToNextLevel <= 0)
                    {
                        int payment = (

                            (
                            (((100 - (int)playerShip.fuel)*100) - rand.Next(0, 10))
                            +
                            (((100 - (int)playerShip.hullStatus)*80) - rand.Next(0, 10))
                            +
                            (rand.Next(100, 200) * (int)difficulty)
                            )

                            );
                        difficulty = difficulty * 1.5f;
                        Console.WriteLine("DESTINATION REACHED");
                        Console.WriteLine("--- PAYMENT RECEIVED: " + payment + "c --- ");
                        playerShip.money += payment;
                        flight = false;
                        Random newsRandom = new Random();
                        docked = true;
                        Console.WriteLine("PRESS ENTER TO FINALIZE DOCKING");
                        Console.ReadLine();
                        Console.WriteLine("SUCCESSFULLY INTERFACED WITH DOCK SYSTES, WELCOME ENGINEER");
                        break;
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

                    if (optionProcessed == "speed" && option.Length >= 6)
                    {

                        if (playerShip.engineShutdown != true)
                        {

                            speedToChangeTo += option[6];

                            try
                            {

                                if (Int32.Parse((string)speedToChangeTo) > 5)
                                {
                                    Console.WriteLine("!! CANNOT EXCEED MAXIMUM SPEED !!");
                                    playerShip.speed = 5;
                                    Console.WriteLine("SPEED HAS CHANGED TO 5");
                                }
                                else if (Int32.Parse(speedToChangeTo) < 1)
                                {
                                    Console.WriteLine("CANNOT STOP THE ENGINE WHILE HOT. DISABLE ENGINE TO STOP.");
                                    Console.WriteLine("!! STOPPING ENGINE FREQUENTLY CAN CAUSE DAMAGE !!");
                                    Console.WriteLine("SPEED HAS CHANGED TO 1");
                                    playerShip.speed = 1;
                                }
                                else
                                {
                                    Console.WriteLine("SPEED HAS CHANGED TO " + Int32.Parse(speedToChangeTo));
                                    playerShip.speed = Int32.Parse(speedToChangeTo);
                                }

                                if (playerShip.speed > 4 && playerShip.fuel <= 25.0 || playerShip.speed > 4 && playerShip.engineStatus <= 25.0)
                                {
                                    playerShip.speed = 4;
                                    Console.WriteLine("FOR CREW AND SHIP SAFETY SPEED CANNOT EXCEED SAFE PARAMETERS WHILE FUEL IS LOW OR ENGINE IS DAMAGED");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("PLEASE PROPERLY FORMAT THE COMMAND");
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
                        Console.WriteLine("RESTART ENGINE      Restarts the engine in case of an engine failure. Will take time to come back online.");
                        Console.WriteLine("SPEED #             Sets the engine speed to the value given in # between 1-5    , the higher the speed the faster the engine degrades and uses more fuel.");
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
                        Console.Write("PROGRESS SAVED");
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
                            writer.WriteLine(flight);
                            writer.WriteLine(docked);
                            writer.WriteLine(difficulty);

                        }

                        using (StreamWriter writer = new StreamWriter("S_R1.txt"))
                        {
                            for (int i = 0; i != 2; i++)
                            {
                                writer.WriteLine(listOfResources.resourceList[i].total);
                                writer.WriteLine(listOfResources.resourceList[i].maxAmount);
                            }

                        }

                    }

                    if (option == "inventory")
                    {
                        Console.WriteLine("\n");
                        for (int i = 0; i != 2; i++) {
                            Console.WriteLine(listOfResources.resourceList[i].resourceName + " " + 
                                listOfResources.resourceList[i].total + "/" + listOfResources.resourceList[i].maxAmount);
                        }
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
                            listOfResources.resourceList[0].total += temporaryVar;
                            Console.WriteLine("MINERALS HARVESTED: " + temporaryVar);
                            if (listOfResources.resourceList[0].total > listOfResources.resourceList[0].maxAmount)
                            {
                                listOfResources.resourceList[0].total = listOfResources.resourceList[0].maxAmount;
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
                                Console.WriteLine("ENGINE ENABLED");
                                playerShip.speed = 1;
                            }
                            else
                            {
                                if (playerShip.engineStopsToDamage == 0)
                                {
                                    playerShip.engineStatus -= 5.0f;
                                    Console.WriteLine("ENGINE DAMAGE TAKEN FROM FREQUENT TOGGLING");
                                }
                                Console.WriteLine("ENGINE DISABLED");
                                playerShip.speed = 0;
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

                    if (option == "restart engine" && playerShip.engineShutdown == true && playerShip.engineWaitTime < 0)
                    {
                        if (playerShip.engineBroken == true)
                        {
                            playerShip.engineWaitTime = 2;
                        }
                        else
                        {
                            Console.WriteLine("RESTART ENGINE IS INTENDED TO BE USED WHEN THE ENGINE FAILS, USE ENGINE TO TOGGLE ENGINE");
                        }
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
