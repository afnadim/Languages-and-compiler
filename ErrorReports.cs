/**
 * @Author: Abdullah Ferdous
 * @Date:   27/10/2018
 * @Filename: Parser - Commands.cs
 */

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {
        //reporting the error
        void ReportError(Token received,TokenKind expected)
        {
            if (received.Kind.ToString() != expected.ToString())
            {
                System.Console.WriteLine("The token We have is :   " +"["+ received +"]"+ "The token we expected is   :  "+"[" +expected+"]");
            }


           errorCount++;
        }


        //checking error while parsing
        bool HasErrors
        {
            get;
        }


        //cvounting the number of error
        int errorCount;
        int ErrorCount
        {
            get { return errorCount; }
        }


       


      



    }



}
