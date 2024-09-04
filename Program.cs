//Britton Whitaker, 8/28/24, Lab 1: Vegenere Cipher
/*
Questions:
1.
*/
using System.Diagnostics;

TestLowerCase();
TestValidInput();
TestASIIShifter();
TestStringShifter();
Console.WriteLine("This program encrypts the characters of a message using the Vigenere method.");
Console.WriteLine("Please enter a message!");
string message = Console.ReadLine() ?? string.Empty; //if value is null, set to string to empty
bool correctInput = false;
string input = string.Empty;
while (!correctInput)
{
    Console.WriteLine("Please enter the key:");
    input = Console.ReadLine() ?? string.Empty;
    if (IsValidInput(input))
    {
        correctInput = true;
    }
    else
    {
        Console.Clear();
        Console.WriteLine("The key must be all lowercase letters. Try Again.");
    }
}

//next give the message and the key to a string shifter that will move each character to the right by the value of the chars ascii value
Console.WriteLine(StringShifter(message, input));

static bool IsValidInput(string passedInput)
/*
Receives a string from the user and checks if all the characters in the string are ALL lowercase letters.
if they are not they return false, otherwise they return true.
*/
{
    bool passed = true;
    foreach (char c in passedInput)
    {
        passed = IsLowercaseLetter(c) ? true : false;
        /*
        ternary operator. it works by evaluating the condition passed to it (char.IsLower(c) in this case).
        the condition is evaluated to true or false. if true, the first value is returned, if false, the second value is returned.
        */
    }
    return passed;
}

static bool IsLowercaseLetter(char passedChar)
{
    return char.IsLower(passedChar) ? true : false;
}

static string StringShifter(string passedMessage, string passedKey)
{
    string encryptedMessage = string.Empty; //this will be the message that is returned to the user

    for (int i = 0; i < passedMessage.Length; i++)
    {
        int keyIndex = i % passedKey.Length; //this should loop through the key over and over again because of the remainder the modulo returns
        char messageChar = passedMessage[i]; //make a char of the current character in the message
        char keyChar = passedKey[keyIndex]; //make a char of the current character in the key
        int messageCharASCII = (int)messageChar; //convert the message char to its ascii value
        int keyCharASCII = (int)keyChar; //convert the key char to its ascii value
        int newCharASCII = ASCIIRangeLimiter(messageCharASCII, keyCharASCII); //add the ascii values together
        char newChar = (char)newCharASCII; //convert the added ascii values back to a char
        encryptedMessage += newChar; //add the new char to the encrypted message that will be returned to the user and printed to the console
    }
    return encryptedMessage;
}

static int ASCIIRangeLimiter(int _char, int _key)
{
    int passedValue = _char + _key;
    int minRange = 32;
    int maxRange = 126;
    //if the value is greater than the max range, modulate it by the max range
    //also add the min range to the value to keep it within the set range
    passedValue = passedValue + minRange;
    if (passedValue > maxRange)
    {
        passedValue = passedValue % maxRange;
    }
    return passedValue;
}

static void TestLowerCase()
{
    Debug.Assert(IsLowercaseLetter('a') == true);
    Debug.Assert(IsLowercaseLetter('A') == false);
    Debug.Assert(IsLowercaseLetter('z') == true);
    Debug.Assert(IsLowercaseLetter('A') == false);
    Debug.Assert(IsLowercaseLetter('"') == false);
    Debug.Assert(IsLowercaseLetter('{') == false);
}

static void TestValidInput()
{
    Debug.Assert(IsValidInput("abc") == true);
    Debug.Assert(IsValidInput("ABC123") == false);
}

static void TestASIIShifter()
{
    Debug.Assert(ASCIIRangeLimiter((int)'H', (int)'a') == 75);
    Debug.Assert(ASCIIRangeLimiter(1, 126) != 64);
}

static void TestStringShifter()
{
    Debug.Assert(StringShifter("Hello World", "a") == "Khoor#Zruog");
    Debug.Assert(StringShifter("Hello World", "abc") != "Khoor#Zruog");
}