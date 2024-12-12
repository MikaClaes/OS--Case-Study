using System;
using System.Collections.Generic;

namespace ClaesMikaCaseStudy
{
    public class Program
    {
        // Main program
        public static void Main(string[] args)
        {
            //Retrieve password length by asking the user how long it should be
            Console.Write("What should the password length be?: ");
            int passwordLength = int.Parse(Console.ReadLine());

            //Making the password at least 4 characters long if the user submits less
            if (passwordLength < 4)
            {
                Console.WriteLine("Password that will be generated will have a minimum length of 4");
                passwordLength = 4;
            }

            //Asking user if he wants to include special characters, uppercase letters, and digits
            Console.Write("Include special characters? (yes/no): ");
            bool includeSpecialChars = Console.ReadLine().Trim().ToLower() == "yes"; //if the user's input equals to "yes" (after getting rid of spaces at the end and putting the input in lowercase letters) --> becomes True

            Console.Write("Include uppercase letters? (yes/no): ");
            bool includeUppercase = Console.ReadLine().Trim().ToLower() == "yes";

            Console.Write("Include digits? (yes/no): ");
            bool includeDigits = Console.ReadLine().Trim().ToLower() == "yes";

            Console.WriteLine(); //Blank line

            string password = GeneratePassword(passwordLength, includeSpecialChars, includeUppercase, includeDigits); //Call generate password function to generate password with parameters we asked the user
            Console.WriteLine($"Generated Password: {password}"); //Output the password that is returned
            Console.ReadLine(); //Keep app open after generating
        }


        //Function that generates password
        public static string GeneratePassword(int length, bool includeSpecialChars, bool includeUppercase, bool includeDigits) //recieves parameters from main program
        {
            List<int> structure = PasswordStructure(length, includeSpecialChars, includeUppercase, includeDigits); //Call structure function to make a list for the structure of the characters with given parameters
            List<char> result = new List<char>(); //Declare a list called result: all characters of the final result will be stored in here
            char[] punctuationMarks = { '!', '#', '$', '%', '&' }; //All punctuation marks / special characters
            Random random = new Random(); //Make a new random

            foreach (var type in structure) //for every type included in our structure:
            {
                switch (type) //Check the type
                {
                    //If it's a 1: add a lowercase letter on it's place in the structure list to the result list --> same for the rest
                    case 1: // Lowercase
                        result.Add((char)random.Next(97, 123)); // 'a' to 'z'
                        break;
                    case 2: // Uppercase
                        result.Add((char)random.Next(65, 91)); // 'A' to 'Z'
                        break;
                    case 3: // Digit
                        result.Add((char)random.Next(48, 58)); // '0' to '9'
                        break;
                    case 4: // Punctuation
                        result.Add(punctuationMarks[random.Next(punctuationMarks.Length)]); // Punctuation mark from list
                        break;
                }
            }

            return string.Join("", result); // Make a string of all characters in the result list
        }


        //Function to build password structure
        public static List<int> PasswordStructure(int structureLength, bool includeSpecialChars, bool includeUppercase, bool includeDigits) //recieves parameters from generate program
        {
            List<int> structure = new List<int>(); //Make a list to store the type of each character in the password
            bool valid = false; //Set validation to False until the password structure is declared valid
            Random rand = new Random(); //Make a new random

            // Determine character types to include
            List<int> allowedTypes = new List<int> { 1 }; // Lowercase is always included
            if (includeUppercase) allowedTypes.Add(2); //If parameter is True: add number 2 (uppercase character in structure) to list allowedTypes
            if (includeDigits) allowedTypes.Add(3); //If parameter is True: add number 3 (digit in structure) to list allowedTypes
            if (includeSpecialChars) allowedTypes.Add(4); //If parameter is True: add number 4 (special characters in structure) to list allowedTypes

            while (!valid) //Keep generating structure until the structure generated is a valid structure
            {
                structure.Clear(); //Clear structure list before generating a new structure

                //Add a random character type to the structure and repeat this until the structure is long enough
                for (int i = 0; i < structureLength; i++)
                {
                    structure.Add(allowedTypes[rand.Next(allowedTypes.Count)]);
                }

                // Ensure all selected types are present
                valid = true; // Password is now valid
                foreach (int type in allowedTypes) // Check for every type if it's in the structure
                {
                    if (!structure.Contains(type)) // Type not in structure
                    {
                        valid = false; // make the password not-valid again
                        break; // Stop checking
                    }
                }
            }

            return structure; //return the valid structure
        }
    }
}

