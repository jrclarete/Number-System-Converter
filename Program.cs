using System;
using System.Collections.Generic;
using System.Linq;

namespace Number_System_Converter
{
    class Program
    {
        static string SelectOption()
        {
            string option;
            while (true)
            {
                Console.WriteLine("Number System Converter. Please select option [1-4]");
                Console.WriteLine("[1] Decimal");
                Console.WriteLine("[2] Binary");
                Console.WriteLine("[3] Octal");
                Console.WriteLine("[4] Hexadecimal");
                option = Console.ReadLine();


                if (option != "1" && option != "2" && option != "3" && option != "4")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            return option;
        }
        static bool RunProgram()
        {
            string tryAgain;
            bool choice = true;
            while (true)
            {
                Console.Write("Try again? [Y/N]");
                tryAgain = Console.ReadLine();

                    if (tryAgain != "Y" && tryAgain != "y" && tryAgain != "N" && tryAgain != "n")
                    {
                        continue;
                    }
                    else
                    {
                        if (tryAgain == "N" || tryAgain == "n")
                        {
                            choice = false;
                        }
                        break;
                    }
            }
            return choice;
        }
        static string GetBaseDigits(int baseNum)
        {
            string digits;
            int decimalPointCtr, negativeSignCtr;
            bool valid = true;

            List<char> validBinary = new List<char>()
            {
                '0', '1', '.', '-'
            };
            List<char> validOctal = new List<char>()
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '.', '-'
            };
            List<char> validHex = new List<char>()
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', '.', '-'
            };

            Console.Write("Enter a number: ");
            while (true)
            {
                decimalPointCtr = 0;
                negativeSignCtr = 0;

                digits = Console.ReadLine();

                for (int i = 0; i < digits.Length; i++)
                {
                    valid = true;
                    if (digits[i] == '.')
                        decimalPointCtr++;
                    if (digits[i] == '-')
                        negativeSignCtr++;
                    if (decimalPointCtr > 1 || negativeSignCtr > 1 || (digits[i] == '-' && i != 0))
                    {
                        valid = false;
                        break;
                    }
                    if (i > 0)
                    {
                        if ((digits[i] == '.' && digits[i-1] == '-'))
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (baseNum == 2)
                    {
                        if (!validBinary.Contains(digits[i]))
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (baseNum == 8)
                    {
                        if (!validOctal.Contains(digits[i]))
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (baseNum == 16)
                    {
                        if (!validHex.Contains(digits[i]))
                        {
                            valid = false;
                            break;
                        }
                    }
                }
                if (valid)
                {
                    break;
                }
                else
                {
                    Console.Write("Invalid input. Please try again: ");
                }
            }
            return digits;
        }
        static long ComputeBaseInteger(string num, int baseNum)
        {
            long integerPart = 0;
            long integerCtr = 0;
            for (int i = num.Length - 1; i >= 0; i--)
            {
                if (baseNum == 16)
                {
                    integerPart = integerPart + (long.Parse(GetHexChar(num[i].ToString())) * (long)Math.Pow(baseNum, integerCtr));
                }
                else
                {
                    integerPart = integerPart + (long.Parse(num[i].ToString()) * (long)Math.Pow(baseNum, integerCtr));
                }
                integerCtr++;
            }
            return integerPart;
        }
        static decimal ComputeBaseFraction(string num, int baseNum)
        {
            decimal fractionPart = 0;
            long fractionCtr = -1;
            
            for (int i = 0; i < num.Length; i++)
            {
                if (baseNum == 16)
                {
                    fractionPart = fractionPart + (decimal.Parse(GetHexChar(num[i].ToString())) * (decimal)Math.Pow(baseNum, fractionCtr));
                }
                else
                {
                    fractionPart = fractionPart + (decimal.Parse(num[i].ToString()) * (decimal)Math.Pow(baseNum, fractionCtr));
                }
                fractionCtr--;
            }
            
            return fractionPart;
        }
        static string GetHexChar(string num)
        {
            if (num == "10")
            {
                return "A";
            }
            if (num == "11")
            {
                return "B";
            }
            if (num == "12")
            {
                return "C";
            }
            if (num == "13")
            {
                return "D";
            }
            if (num == "14")
            {
                return "E";
            }
            if (num == "15")
            {
                return "F";
            }
            if (num == "A" || num == "a")
            {
                return "10";
            }
            if (num == "B" || num == "b")
            {
                return "11";
            }
            if (num == "C" || num == "c")
            {
                return "12";
            }
            if (num == "D" || num == "d")
            {
                return "13";
            }
            if (num == "E" || num == "e")
            {
                return "14";
            }
            if (num == "F" || num == "f")
            {
                return "15";
            }
            return num;
        }
        static decimal ConvertBaseToDecimal(string digits, int baseNum)
        {
            bool negativeSign;
            string integerPart = "", fractionPart = "";

            

            if (digits.Contains('.'))
            {
                integerPart = digits.Substring(0, digits.IndexOf('.'));
                fractionPart = digits.Substring(digits.IndexOf('.') + 1);
            }
            else
            {
                integerPart = digits;
            }

            negativeSign = integerPart.Contains('-');
            if (negativeSign)
            {   
                integerPart = integerPart.Remove(0, 1);
            }

            long tempIntegerRes = 0;
            decimal tempFractionRes = 0;

            
            if (integerPart != "0")
                tempIntegerRes = ComputeBaseInteger(integerPart, baseNum);
            if (fractionPart != "" && fractionPart != "0")
                tempFractionRes = ComputeBaseFraction(fractionPart, baseNum);

            decimal result = 0;

            result = negativeSign ? (tempIntegerRes + tempFractionRes) * -1 : tempIntegerRes + tempFractionRes;
            return result;
        }
        static decimal GetDecimalDigits()
        {
            decimal decimalDigits;
            Console.Write("Enter a number: ");
            while (true)
            {
                try
                {
                    decimalDigits = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (System.Exception)
                {
                   Console.Write("Invalid input. Please try again: ");
                }
            }
            return decimalDigits;
        }
        static string ConvertDecimalToBase(decimal decimalDigits, int baseNum)
        {
            long integerPart = (long)decimalDigits;
            decimal fractionPart = Math.Abs(decimalDigits) - Math.Abs(integerPart);

            string result = "";

            if (integerPart > 0)
            {
                result += $"{ComputeDecimalInteger(integerPart, baseNum)}";
            }
            if (integerPart < 0)
            {
                result += $"-{ComputeDecimalInteger(Math.Abs(integerPart), baseNum)}";
            }
            if (integerPart == 0)
            {
                result += "0";
            }
            if (fractionPart != 0)
            {
                result += $".{ComputeDecimalFraction(fractionPart, baseNum)}";
            }
            return result;
        }
        static string GetReverse(string strRes)
        {
            string tempRes = "";
            char[] arr = strRes.ToCharArray();
            var arrInteger = arr.Reverse();
            foreach (var item in arrInteger)
            {
                tempRes += item;
            }
            return tempRes;
        }
        static string ComputeDecimalInteger(long num, int baseNum)
        {
            long tempInteger = Math.Abs(num);
            string integerPart = "";

            while (tempInteger != 0)
            {
                if (baseNum == 16)
                {
                   integerPart = integerPart + GetHexChar((tempInteger % baseNum).ToString());
                }
                else
                {
                    integerPart = integerPart + (tempInteger % baseNum).ToString();
                }
                    tempInteger = (long)(tempInteger / baseNum);
            } 
            return GetReverse(integerPart);
        }
        static string ComputeDecimalFraction(decimal num, int baseNum)
        {
            decimal tempFraction = num;
            decimal tempFractionInt = 0;
            decimal tempFractionFra = 0;
            string fractionPart = "";
            int ctr = 0;
            
            while (tempFraction != 0 && ctr != 15)
            {
                tempFractionInt = (long)(tempFraction * baseNum);
                tempFractionFra = (tempFraction * baseNum) - tempFractionInt;
                if (baseNum == 16)
                {
                    fractionPart += GetHexChar(tempFractionInt.ToString());
                }
                else
                {
                    fractionPart += tempFractionInt.ToString();
                }
                tempFraction = tempFractionFra;
                ctr++;
            }

            return fractionPart;
        }
        static string Get2sCompliment(decimal decimalDigits, int baseNum)
        {
            long integerPart = (long)decimalDigits;
            decimal fractionPart = (Math.Abs(decimalDigits) - Math.Abs(integerPart));

            long number = Math.Abs((long)decimal.Parse(ConvertDecimalToBase(integerPart, 2))); 
            
            bool compliment;
            string resultInteger = "", resultFraction = "";
            long rem = 0;
            compliment = false;

            while (number != 0)
            {
                rem = number % 10;
                if (compliment == false)
                {
                    if (rem == 0)
                    {
                        resultInteger += rem.ToString();
                    }
                    else
                    {
                        resultInteger += rem.ToString();
                        compliment = true;
                    }
                }
                else
                {
                    resultInteger = resultInteger + (rem == 1 ? "0" : "1");
                }
                number /= 10;
            }

            if (fractionPart > 0)
            {
                resultFraction = $".{ComputeDecimalFraction(fractionPart, baseNum)}";
            }
            return $"1{GetReverse(resultInteger)}{resultFraction}";
        }
        static void Main(string[] args)
        {
            string option;
            bool runProgram = true;
            
            while (runProgram)
            {
                option = SelectOption();
                if (option == "1")
                {
                    decimal decimalDigits;
                    decimalDigits = GetDecimalDigits();
                    Console.WriteLine($"Binary: {ConvertDecimalToBase(decimalDigits, 2)}");
                    Console.WriteLine($"Octal: {ConvertDecimalToBase(decimalDigits, 8)}");
                    Console.WriteLine($"Hexadecimal: {ConvertDecimalToBase(decimalDigits, 16)}");
                    if (decimalDigits < 0)
                        Console.WriteLine($"2's compliment: {Get2sCompliment(decimalDigits, 2)}");
                }
                else
                {
                    string digits;
                    if (option == "2")
                    {
                        digits = GetBaseDigits(2);
                        Console.WriteLine($"Decimal: {ConvertBaseToDecimal(digits, 2)}");
                        Console.WriteLine($"Octal: {ConvertDecimalToBase(ConvertBaseToDecimal(digits, 2), 8)}");
                        Console.WriteLine($"Hexadecimal: {ConvertDecimalToBase(ConvertBaseToDecimal(digits, 2), 16)}");
                    }
                    if (option == "3")
                    {
                        digits = GetBaseDigits(8);
                        Console.WriteLine($"Decimal: {ConvertBaseToDecimal(digits, 8)}");
                        Console.WriteLine($"Binary: {ConvertDecimalToBase(ConvertBaseToDecimal(digits, 8), 2)}");
                        Console.WriteLine($"Hexadecimal: {ConvertDecimalToBase(ConvertBaseToDecimal(digits, 8), 16)}");
                    }
                    if (option == "4")
                    {
                        digits = GetBaseDigits(16);
                        Console.WriteLine($"Decimal: {ConvertBaseToDecimal(digits, 16)}");
                        Console.WriteLine($"Binary: {ConvertDecimalToBase(ConvertBaseToDecimal(digits, 16), 2)}");
                        Console.WriteLine($"Octal: {ConvertDecimalToBase(ConvertBaseToDecimal(digits, 16), 8)}");
                    }
                }
                runProgram = RunProgram();
            }
        }
    }
}
