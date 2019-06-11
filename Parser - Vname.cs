/**
 * @Author: Abdullah Ferdous
 * @Date:   27/10/2018
 * @Filename: Parser - Vnames.cs
 */

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {

        // /////////////////////////////////////////////////////////////////////////////
        //
        // VALUE-OR-VARIABLE NAMES
        //
        // /////////////////////////////////////////////////////////////////////////////


        void ParseVname()
        {
            System.Console.WriteLine("Parsing Variable Name" + "   " + "[" + tokens.Current.Spelling + "}" + "       " + tokens.Current.Position.ToString());
            ParseIdentifier();

        }

    }
}
